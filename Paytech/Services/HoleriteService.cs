using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Paytech.Models;
using Paytech.Repositories;
using Paytech.Utils;
using System.Buffers.Text;
using System.Data.Common;
using System.Globalization;
using static Dapper.SqlMapper;

namespace Paytech.Services
{
    public class HoleriteService
    {

        private IHoleriteRepository _holeriteRepository;

        public HoleriteService()
        {
            _holeriteRepository = new HoleriteRepository();
        }

        public Task<Retorno> Insert(Holerite holerite)
        {
            return _holeriteRepository.Insert(holerite);
        }
        public Task<Retorno> Update(Holerite holerite)
        {
            return _holeriteRepository.Update(holerite);
        }
        public Task<Retorno> Delete(int id)
        {
            return _holeriteRepository.Delete(id);
        }
        public Task<Retorno> GetById(int id)
        {
            return _holeriteRepository.GetById(id);
        }
        public Task<Retorno> GetByIdFuncionario(int idFuncionario)
        {
            return _holeriteRepository.GetByIdFuncionario(idFuncionario);
        }
        public Task<Retorno> GetAll()
        {
            return _holeriteRepository.GetAll();
        }

        public async Task<Retorno> GerarDecimoTerceiro(int idFuncionario, DateTime dataCalculo)
        {
            try
            {
                var funcionario = (Funcionario)new FuncionarioService().GetById(idFuncionario).Result.Dados;
                Holerite holerite = new Holerite();
                holerite.Id_funcionario = idFuncionario;

                int anoAdmissao = funcionario.InformacoesTrabalhistas.Dt_admissao.Value.Year;
                int anoCalculo = dataCalculo.Year;

                holerite.Meses_referente = 0;

                for (int ano = anoAdmissao; ano <= anoCalculo; ano++)
                {
                    int mesInicio = (ano == anoAdmissao) ? funcionario.InformacoesTrabalhistas.Dt_admissao.Value.Month : 1;
                    int mesFim = (ano == anoCalculo) ? dataCalculo.Month : 12;

                    int mesesNoAnoAtual = mesFim - mesInicio + 1;
                    holerite.Meses_referente += mesesNoAnoAtual;

                    if (ano < anoCalculo)
                    {
                        holerite.Meses_referente = 0;
                    }
                }

                holerite.Salario_base = (funcionario.InformacoesTrabalhistas.Salario_Bruto / 12) * holerite.Meses_referente;
                holerite.Salario_Base_Inss = holerite.Salario_base;
                holerite.Desconto_inss = CalcularINSS((double)holerite.Salario_Base_Inss);
                holerite.Deducao_dependente = DeducaoDependentes(funcionario.InformacoesTrabalhistas.Qtd_Dependentes.Value);
                holerite.Salario_base_ir = holerite.Salario_base - holerite.Desconto_inss - holerite.Deducao_dependente;
                holerite.Desconto_ir = CalcularIRRF((double)holerite.Salario_base_ir);
                holerite.Salario_liquido = holerite.Salario_base - holerite.Desconto_inss - holerite.Desconto_ir;
                holerite.Fgts = CalcularFgts((double)holerite.Salario_base);
                holerite.Faixa_ir = RetornaFaixaIR((double)holerite.Salario_base_ir);

                holerite.Data_competencia_Inicio = dataCalculo;
                holerite.Data_competencia_Fim = dataCalculo;

                holerite.Tipo = "decimo";

                var retorno = Insert(holerite);

                if (retorno.Result.Sucesso)
                {
                    return new Retorno(true, retorno.Result.Dados, "Decimo terciero gerado com sucesso");
                }
                else
                {
                    return new Retorno(false, "Ocorreu um erro ao gerar o decimo terciero: " + retorno.Result.Mensagem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao gerar o decimo terceiro: " + ex.Message);
            }
        }

        public async Task<Retorno> GerarCalculoFerias(int idFuncionario, DateTime data_inicio, DateTime data_fim)
        {
            try
            {
                if ((data_fim - data_inicio).Days != 30)
                {
                    return new Retorno(false, "O Período de férias é necessário que seja de 30 dias");
                }

                var funcionario = (Funcionario)new FuncionarioService().GetById(idFuncionario).Result.Dados;
                Holerite holerite = new Holerite();

                var qtdFeriasFuncionario = new HoleriteRepository().GetQtdFerias(idFuncionario);

                var dataAdmissao = funcionario.InformacoesTrabalhistas.Dt_admissao.Value;
                
                if (data_inicio < dataAdmissao)
                {
                    return new Retorno(false, "A data de cálculo não pode ser anterior à data de admissão.");
                }

                int mesesTrabalhados = (data_inicio.Year - dataAdmissao.Year) * 12 + data_inicio.Month - dataAdmissao.Month;
                int mesesProporcionais = mesesTrabalhados - (qtdFeriasFuncionario * 12);

                if(mesesProporcionais < 12)
                    return new Retorno(false, "Férias indiponíveis");

                holerite.Salario_base = (double)funcionario.InformacoesTrabalhistas.Salario_Bruto.Value + ((double)funcionario.InformacoesTrabalhistas.Salario_Bruto.Value / 3);
                holerite.Meses_referente = 12;
                holerite.Salario_Base_Inss = holerite.Salario_base;
                holerite.Desconto_inss = CalcularINSS(holerite.Salario_Base_Inss.Value);
                holerite.Deducao_dependente = DeducaoDependentes((int)funcionario.InformacoesTrabalhistas.Qtd_Dependentes);
                holerite.Salario_base_ir = holerite.Salario_Base_Inss - holerite.Desconto_inss - holerite.Deducao_dependente;
                holerite.Desconto_ir = CalcularIRRF(holerite.Salario_base_ir.Value);
                holerite.Fgts = CalcularFgts(holerite.Salario_Base_Inss.Value);
                holerite.Faixa_ir = RetornaFaixaIR((double)holerite.Salario_base_ir);

                holerite.Salario_liquido = holerite.Salario_Base_Inss - holerite.Desconto_inss - holerite.Desconto_ir;

                holerite.Tipo = "ferias";

                holerite.Data_competencia_Inicio = data_inicio;
                holerite.Data_competencia_Fim = data_fim;
                holerite.Id_funcionario = idFuncionario;

                var retorno = Insert(holerite);

                if (retorno.Result.Sucesso)
                {
                    return new Retorno(true, retorno.Result.Dados, "Ferias gerada com sucesso");
                }
                else
                {
                    return new Retorno(false, "Ocorreu um erro ao gerar a ferias: " + retorno.Result.Mensagem);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao gerar a ferias: " + ex.Message);
            }

        }

        public async Task<Retorno> GerarHolerite(int idFuncionario, DateTime data_inicio, DateTime data_fim, double percentualHoraExtra, double valorValeTransporte, double faltas, double horaExtra)
        {
            try {
                var funcionario = (Funcionario)new FuncionarioService().GetById(idFuncionario).Result.Dados;
                Holerite resultado = new Holerite();

                resultado.Calculo_hora_extra = CalcularHoraExtra(horaExtra, (double)funcionario.InformacoesTrabalhistas.Salario_Bruto, percentualHoraExtra);
                resultado.Desconto_faltas_horas = CalcularDescontoFaltasEmHoras(faltas, (double)funcionario.InformacoesTrabalhistas.Salario_Bruto);
                resultado.Salario_base = CalcularSalarioMes((DateTime)funcionario.InformacoesTrabalhistas.Dt_admissao, data_fim, (double)funcionario.InformacoesTrabalhistas.Salario_Bruto);
                var salbaseHoraExtra = resultado.Salario_base + resultado.Calculo_hora_extra - resultado.Desconto_faltas_horas;
                resultado.Salario_Base_Inss = resultado.Salario_base + resultado.Calculo_hora_extra - resultado.Desconto_faltas_horas;
                resultado.Desconto_inss = CalcularINSS((double)resultado.Salario_base);
                resultado.Deducao_dependente = DeducaoDependentes((int)funcionario.InformacoesTrabalhistas.Qtd_Dependentes);
                var salBaseIr = resultado.Salario_base - resultado.Desconto_inss - resultado.Deducao_dependente;
                resultado.Salario_base_ir = salBaseIr < 0 ? 0 : salBaseIr;
                resultado.Desconto_ir = CalcularIRRF((double)resultado.Salario_base_ir);
                resultado.Vale_transporte = 0.0;
                resultado.Fgts = CalcularFgts((double)salbaseHoraExtra);
                resultado.Faixa_ir = RetornaFaixaIR((double)resultado.Salario_base_ir);

                if ((bool)funcionario.InformacoesTrabalhistas.Opt_Vale_Transporte)
                {
                    resultado.Vale_transporte = CalcularValeTransporte(valorValeTransporte, (double)funcionario.InformacoesTrabalhistas.Salario_Bruto);
                }

                resultado.Salario_liquido = salbaseHoraExtra - resultado.Desconto_inss - resultado.Desconto_ir - resultado.Vale_transporte;
                resultado.Id_funcionario = idFuncionario;
                resultado.Data_competencia_Inicio = data_inicio;
                resultado.Data_competencia_Fim = data_fim;

                resultado.Tipo = "holerite";

                var retorno = Insert(resultado);

                if (retorno.Result.Sucesso)
                {
                    return new Retorno(true, retorno.Result.Dados, "Holerite gerado com sucesso");
                }
                else 
                {
                    return new Retorno(false, "Ocorreu um erro ao gerar o holerite: " + retorno.Result.Mensagem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao gerar o holerite: " + ex.Message);
            }
        }
        public double CalcularHoraExtra(double horaExtra, double salarioBruto, double percentualHoraExtra)
        {
            if (horaExtra <= 0)
            {
                return 0;
            }

            double horaExtraCalculada = salarioBruto / 220 * percentualHoraExtra;
            double totalHoraExtra = horaExtraCalculada * horaExtra;
            return totalHoraExtra;
        }

        public double CalcularDescontoFaltasEmHoras(double faltasEmHoras, double salarioBruto)
        {
            const int horasMensaisPadrao = 220;
            double descontoFaltas = salarioBruto / horasMensaisPadrao * faltasEmHoras;
            return descontoFaltas;
        }

        public double CalcularSalarioMes(DateTime dataAdmissao, DateTime dataCalculo, double salarioBruto)
        {
            int diasTrabalhados;

            if (dataAdmissao.Month == dataCalculo.Month && dataAdmissao.Year == dataCalculo.Year)
            {
                diasTrabalhados = dataCalculo.Day - dataAdmissao.Day;
            }
            else
            {
                diasTrabalhados = 30;
            }
            double valorProporcional = (salarioBruto / 30) * diasTrabalhados;
            return valorProporcional;
        }

        public double CalcularINSS(double salarioBase)
        {
            double tetoFaixa4 = 7507.49;
            double aliquotaFaixa4 = 14.0;

            if (salarioBase > tetoFaixa4)
            {
                return 876.98;
            }

            double inssTotal = 0.0;

            // Faixa 1
            double tetoFaixa1 = 1320.00;
            double aliquotaFaixa1 = 7.5;
            if (salarioBase <= tetoFaixa1)
            {
                inssTotal += salarioBase * (aliquotaFaixa1 / 100);
                return inssTotal;
            }
            else
            {
                inssTotal += tetoFaixa1 * (aliquotaFaixa1 / 100);
            }
            // Faixa 2
            double tetoFaixa2 = 2571.29;
            double aliquotaFaixa2 = 9.0;
            if (salarioBase <= tetoFaixa2)
            {
                inssTotal += (salarioBase - tetoFaixa1) * (aliquotaFaixa2 / 100);
                return inssTotal;
            }
            else
            {
                inssTotal += (tetoFaixa2 - tetoFaixa1) * (aliquotaFaixa2 / 100);
            }
            // Faixa 3
            double tetoFaixa3 = 3856.94;
            double aliquotaFaixa3 = 12.0;
            if (salarioBase <= tetoFaixa3)
            {
                inssTotal += (salarioBase - tetoFaixa2) * (aliquotaFaixa3 / 100);
            }
            else
            {
                inssTotal += (tetoFaixa3 - tetoFaixa2) * (aliquotaFaixa3 / 100);
            }
            // Faixa 4
            if (salarioBase > tetoFaixa3)
            {
                double descontoFaixa4 = (salarioBase - tetoFaixa3) * (aliquotaFaixa4 / 100);
                inssTotal += descontoFaixa4;
                return inssTotal;

            }
            return inssTotal;
        }

        public double DeducaoDependentes(int dependentes)
        {
            double deducao = 189.59;
            return dependentes * deducao;
        }

        public double CalcularIRRF(double salarioBaseIR)
        {
            double irrf = 0.0;

            if (salarioBaseIR <= 2112.00)
            {
                irrf = 0.0;
            }
            else if (salarioBaseIR <= 2826.65)
            {
                irrf = salarioBaseIR * 0.075 - 158.40;
            }
            else if (salarioBaseIR <= 3751.05)
            {
                irrf = salarioBaseIR * 0.15 - 370.40;
            }
            else if (salarioBaseIR <= 4664.68)
            {
                irrf = salarioBaseIR * 0.225 - 651.73;
            }
            else
            {
                irrf = salarioBaseIR * 0.275 - 884.96;
            }

            return irrf;
        }
        
        public double RetornaFaixaIR(double salarioBaseIR)
        {
            double faixa = 0.0;

            if (salarioBaseIR <= 2112.00)
            {
                faixa = 0.0;
            }
            else if (salarioBaseIR <= 2826.65)
            {
                faixa = 7.5;
            }
            else if (salarioBaseIR <= 3751.05)
            {
                faixa = 15;
            }
            else if (salarioBaseIR <= 4664.68)
            {
                faixa = 22.5;
            }
            else
            {
                faixa = 27.5;
            }

            return faixa;
        }

        public double CalcularFgts(double salarioBruto)
        {
            double fgtsMensal = salarioBruto * 0.08;
            return fgtsMensal;
        }

        public double CalcularValeTransporte(double valorValeTransporte, double salarioBruto)
        {
            double descontoValetransporte;

            if (valorValeTransporte >= (salarioBruto * 0.06))
            {
                descontoValetransporte = salarioBruto * 0.06;
            }
            else
            {
                descontoValetransporte = valorValeTransporte;
            }

            return descontoValetransporte;
        }

        public async Task<Retorno> gerarExcelHolerite(int IdHolerite)
        {
            try
            {
                var resultHolerite = GetById(IdHolerite).Result;

                if (!resultHolerite.Sucesso) return new Retorno(false, "Ocorreu um erro ao gerar o holerite: Holerite não encontrado");

                var holerite = (Holerite)resultHolerite.Dados;
                var funcionario = (Funcionario)new FuncionarioService().GetById((int)holerite.Id_funcionario).Result.Dados;

                string caminhoArquivo = AppDomain.CurrentDomain.BaseDirectory + "../../../Templete/ArquivoRelatorio.xlsx";
                string caminhoTemplate = AppDomain.CurrentDomain.BaseDirectory + "../../../Templete/RH_Holerite.xlsx";

                System.IO.File.Delete(caminhoArquivo);
                System.IO.File.Copy(caminhoTemplate, caminhoArquivo);
                Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

                using (var workbook = new XLWorkbook(caminhoArquivo))
                {
                    double venciamento = 0;
                    double descontos = 0;

                    Stream spreadsheetStream = new MemoryStream();
                    var worksheet = workbook.Worksheets.Worksheet("Sheet");

                    worksheet.Cell("E9").Value = "304 - teste ME";
                    worksheet.Cell("AS9").Value = "teste";

                    worksheet.Cell("AO13").Value = "" + holerite.Data_competencia_Inicio?.ToString("dd/MM/yyyy") + " a " + holerite.Data_competencia_Inicio?.ToString("dd/MM/yyyy");
                    worksheet.Cell("X19").Value = "testeee ME";


                    worksheet.Cell("D18").Value = funcionario.Id + " - " + funcionario.Nome;
                    worksheet.Cell("H20").Value = funcionario.InformacoesTrabalhistas.funcao;

                    worksheet.Cell("I27").Value = "Salário bruto";
                    worksheet.Cell("AJ27").Value = holerite.Salario_base;
                    worksheet.Cell("AJ27").Style.NumberFormat.Format = "R$ #,##0.00";

                    worksheet.Cell("I30").Value = "INSS";
                    worksheet.Cell("AV30").Value = holerite.Desconto_inss;
                    worksheet.Cell("AV30").Style.NumberFormat.Format = "R$ #,##0.00";

                    worksheet.Cell("I33").Value = "IRRF";
                    worksheet.Cell("AV33").Value = holerite.Desconto_ir;
                    worksheet.Cell("AV33").Style.NumberFormat.Format = "R$ #,##0.00";

                    worksheet.Cell("I36").Value = "Vale transporte";
                    worksheet.Cell("AV36").Value = holerite.Vale_transporte;
                    worksheet.Cell("AV36").Style.NumberFormat.Format = "R$ #,##0.00";

                    worksheet.Cell("I39").Value = "Hora extra";
                    worksheet.Cell("AJ39").Value = holerite.Calculo_hora_extra;
                    worksheet.Cell("AJ39").Style.NumberFormat.Format = "R$ #,##0.00";

                    worksheet.Cell("I42").Value = "Faltas";
                    worksheet.Cell("AV42").Value = holerite.Desconto_faltas_horas;
                    worksheet.Cell("AV42").Style.NumberFormat.Format = "R$ #,##0.00";

                    ////Vencimentos
                    //worksheet.Cell("AJ67").Value = "5000";
                    //worksheet.Cell("AV42").Style.NumberFormat.Format = "R$ #,##0.00";

                    //worksheet.Cell("AV42").Value = "255";
                    //worksheet.Cell("AV42").Style.NumberFormat.Format = "R$ #,##0.00";

                    //Salario base
                    worksheet.Cell("E79").Value = holerite.Salario_base;
                    worksheet.Cell("E79").Style.NumberFormat.Format = "R$ #,##0.00";

                    //Sal.Contr.INSS
                    worksheet.Cell("P79").Value = holerite.Salario_Base_Inss;
                    worksheet.Cell("P79").Style.NumberFormat.Format = "R$ #,##0.00";

                    //Base Cálc.FGTS
                    worksheet.Cell("U79").Value = holerite.Salario_Base_Inss;
                    worksheet.Cell("U79").Style.NumberFormat.Format = "R$ #,##0.00";

                    //FGTS do Mês
                    worksheet.Cell("Z79").Value = holerite.Fgts;
                    worksheet.Cell("Z79").Style.NumberFormat.Format = "R$ #,##0.00";

                    //Base Cálc.IRRF
                    worksheet.Cell("AL79").Value = holerite.Salario_base_ir;
                    worksheet.Cell("AL79").Style.NumberFormat.Format = "R$ #,##0.00";

                    //Faixa IRRF
                    worksheet.Cell("BA79").Value = 0;
                    worksheet.Cell("BA79").Style.NumberFormat.Format = "R$ #,##0.00";
                    

                    worksheet.Cell("K74").Value = funcionario.InformacoesTrabalhistas.Dt_admissao?.ToString("dd/MM/yyyy"); ;
                    worksheet.Cell("T74").Value = funcionario.InformacoesTrabalhistas.Num_pis;

                    worksheet.Cell("AK85").Value = holerite.Data_calculo?.ToString("dd/MM/yyyy") + holerite.Data_calculo?.ToString("ddd", new CultureInfo("pt-BR"));


                    workbook.SaveAs(spreadsheetStream);
                    System.IO.File.Delete(caminhoArquivo);
                    spreadsheetStream.Position = 0;

                    var memoryStream = new MemoryStream();
                    spreadsheetStream.CopyTo(memoryStream);

                    byte[] bytes;
                    bytes = memoryStream.ToArray();
                    string base64 = Convert.ToBase64String(bytes);

                    return new Retorno(true, base64, "Holerite gerado com sucesso");
                }
            }
            catch (Exception ex)
            {
                return new Retorno(false, "Ocorreu um erro ao gerar o holerite: " + JsonConvert.SerializeObject(ex));
            }
        }
    }
}
