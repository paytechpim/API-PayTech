using ClosedXML.Excel;
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

        public async Task<Retorno> GerarHolerite(int idFuncionario, DateTime dataCalculoSalarioMensal, double percentualHoraExtra, double valorValeTransporte, double faltas, double horaExtra)
        {
            try {
                var funcionario = (Funcionario)new FuncionarioService().GetById(idFuncionario).Result.Dados;
                Holerite resultado = new Holerite();

                resultado.Calculo_hora_extra = CalcularHoraExtra(horaExtra, (double)funcionario.InformacoesTrabalhistas.Salario_Bruto, percentualHoraExtra);
                resultado.Desconto_faltas_horas = CalcularDescontoFaltasEmHoras(faltas, (double)funcionario.InformacoesTrabalhistas.Salario_Bruto);
                resultado.Salario_base = CalcularSalarioMes((DateTime)funcionario.InformacoesTrabalhistas.Dt_admissao, dataCalculoSalarioMensal, (double)funcionario.InformacoesTrabalhistas.Salario_Bruto);
                resultado.Salario_Base_Inss = resultado.Salario_base + resultado.Calculo_hora_extra - resultado.Desconto_faltas_horas;
                resultado.Desconto_inss = CalcularINSS((double)resultado.Salario_base);
                resultado.Deducao_dependente = DeducaoDependentes((int)funcionario.InformacoesTrabalhistas.Qtd_Dependentes);
                resultado.Salario_base_ir = resultado.Salario_base - resultado.Desconto_inss - resultado.Deducao_dependente;
                resultado.Desconto_ir = CalcularIRRF((double)resultado.Salario_base_ir);
                resultado.Vale_transporte = 0.0;
                resultado.Fgts = CalcularFgts((double)resultado.Salario_base);

                if ((bool)funcionario.InformacoesTrabalhistas.Opt_Vale_Transporte)
                {
                    resultado.Vale_transporte = CalcularValeTransporte(valorValeTransporte, (double)funcionario.InformacoesTrabalhistas.Salario_Bruto);
                }

                resultado.Salario_liquido = resultado.Salario_base - resultado.Desconto_inss - resultado.Desconto_ir - resultado.Vale_transporte;
                resultado.Id_funcionario = idFuncionario;
                resultado.Data_competencia = dataCalculoSalarioMensal;

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


                string caminhoArquivo = AppDomain.CurrentDomain.BaseDirectory + "../../../Templete/ArquivoRelatorio.xlsx";
                string caminhoTemplate = AppDomain.CurrentDomain.BaseDirectory + "../../../Templete/RH_Holerite.xlsx";

                System.IO.File.Delete(caminhoArquivo);
                System.IO.File.Copy(caminhoTemplate, caminhoArquivo);
                Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

                using (var workbook = new XLWorkbook(caminhoArquivo))
                {
                    Stream spreadsheetStream = new MemoryStream();
                    var worksheet = workbook.Worksheets.Worksheet("Sheet");
                    var count = 0;
                    var linhaInicioDados = 5;

                    worksheet.Cell("D18").Value = "16 - Diego Cesar Domingos";

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
