using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Paytech.Models;
using Paytech.Services;
using Paytech.Utils;

namespace Paytech.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {

        private IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        public async Task<Retorno> Insert(Funcionario funcionario)
        {
            try
            {
                if (!string.IsNullOrEmpty(funcionario?.Cnh?.Num_cnh))
                {
                    var cnhService = new CnhService();
                    var cnh = cnhService.GetByNumCnh(funcionario.Cnh.Num_cnh);
                    cnh ??= await cnhService.Insert(funcionario.Cnh);
                    funcionario.Cnh = cnh;
                }

                if (!string.IsNullOrEmpty(funcionario?.TituloEleitor?.Numero_Titulo))
                {
                    var tituloService = new TituloEleitorService();
                    var titulo = tituloService.GetByTitulo(funcionario.TituloEleitor.Numero_Titulo);
                    titulo ??= await tituloService.Insert(funcionario.TituloEleitor);
                    funcionario.TituloEleitor = titulo;
                }

                if (!string.IsNullOrEmpty("" + funcionario?.InformacoesTrabalhistas?.ID))
                {
                    var infoService = new InformacoesTrabalhistasService();
                    var get = infoService.GetById((int)funcionario.InformacoesTrabalhistas.ID);
                    var infoTrab = get.Result.Dados;
                    infoTrab ??= infoService.Insert(funcionario.InformacoesTrabalhistas).Result.Dados;
                    funcionario.InformacoesTrabalhistas = (InformacoesTrabalhistas)infoTrab;
                }

                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var param = new
                {
                    funcionario.Nome,
                    funcionario.Cpf,
                    funcionario.Rg,
                    funcionario.Escolaridade,
                    funcionario.Telefone,
                    funcionario.Genero,
                    funcionario.Naturalidade,
                    funcionario.Num_reservista,
                    funcionario.Nome_mae,
                    funcionario.Nome_pai,
                    funcionario.Dt_nascimento,
                    funcionario.TituloEleitor.Numero_Titulo,
                    funcionario.Cnh.Num_cnh,
                    funcionario.Estado_civil,
                    funcionario.Endereco.Rua,
                    funcionario.Endereco.Numero,
                    funcionario.Endereco.Cep,
                    funcionario.Endereco.Bairro,
                    funcionario.Endereco.Cidade,
                    funcionario.Endereco.Uf,
                    funcionario.Endereco.Complemento,
                    Id_trabalhista = funcionario.InformacoesTrabalhistas.ID
                };
                int id = db.Query<int>(Funcionario.INSERT, param).Single();

                if (id > 0)
                {
                    var func = GetById(id).Result.Dados;
                    return new Retorno(true, func, "Dados inseridos com sucesso.");
                }
                else
                {
                    return new Retorno(false, "Ocorreu um erro ao inserir");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao inserir: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao inserir: " + ex.Message);
            }

        }

        public List<Funcionario> GetAll()
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                return db.Query<Funcionario>(Funcionario.SELECT_ALL).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public async Task<Retorno> GetById(int id)
        {
            try
            {
                Funcionario funcionario = new Funcionario();

                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var selectFunc = db.QueryFirstOrDefault(Funcionario.SELECT_BY_ID, new { Id = id });

                if (selectFunc?.ID > 0)
                {
                    funcionario = JsonConvert.DeserializeObject<Funcionario>(JsonConvert.SerializeObject(selectFunc));
                    funcionario.Cnh = !string.IsNullOrEmpty(selectFunc.num_CNH) ? new CnhService().GetByNumCnh(selectFunc.num_CNH) : new Cnh();
                    funcionario.TituloEleitor = !string.IsNullOrEmpty(selectFunc.numero_titulo) ? new TituloEleitorService().GetByTitulo(selectFunc.numero_titulo) : new TituloEleitor();
                    funcionario.InformacoesTrabalhistas = selectFunc.id_trabalhista > 0 ? new InformacoesTrabalhistasService().GetById(selectFunc.id_trabalhista).Result.Dados : new InformacoesTrabalhistas();
                    funcionario.Endereco = JsonConvert.DeserializeObject<Endereco>(JsonConvert.SerializeObject(selectFunc));
                }

                return new Retorno(true, funcionario, "Dados pesquisado com sucesso.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao pesquisar: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao pesquisar: " + ex.Message);
            }
        }

        public async Task<Retorno> GetByName(string nome)
        {
            nome = "%" + nome + "%";
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));

                List<Funcionario> funcionarios = new List<Funcionario>();

                foreach (var item in db.Query(Funcionario.SELECT_BY_NAME, new { Nome = nome }).AsList())
                {
                    var funcionario = new Funcionario();
                    if (item?.ID > 0)
                    {
                        funcionario = JsonConvert.DeserializeObject<Funcionario>(JsonConvert.SerializeObject(item));
                        funcionario.Cnh = !string.IsNullOrEmpty(item.num_CNH) ? new CnhService().GetByNumCnh(item.num_CNH) : new Cnh();
                        funcionario.TituloEleitor = !string.IsNullOrEmpty(item.numero_titulo) ? new TituloEleitorService().GetByTitulo(item.numero_titulo) : new TituloEleitor();
                        funcionario.InformacoesTrabalhistas = item.id_trabalhista > 0 ? new InformacoesTrabalhistasService().GetById(item.id_trabalhista).Result.Dados : new InformacoesTrabalhistas();
                        funcionario.Endereco = JsonConvert.DeserializeObject<Endereco>(JsonConvert.SerializeObject(item));
                    }

                    funcionarios.Add(funcionario);
                }

                return new Retorno(true, funcionarios, "Dados pesquisado com sucesso.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao pesquisar: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao pesquisar: " + ex.Message);
            }
        }
        public async Task<Retorno> AlterarFuncionario(Funcionario funcionario)
        {
            try
            {
                var informacoesTrabalhistasService = new InformacoesTrabalhistasService();
                var cnhService = new CnhService();
                var tituloEleitorService = new TituloEleitorService();
                var enderecoService = new EnderecoService();

                if (informacoesTrabalhistasService.GetById((int)funcionario.InformacoesTrabalhistas.ID).Result.Dados == null)
                {
                    informacoesTrabalhistasService.Insert(funcionario.InformacoesTrabalhistas);
                }
                else
                {
                    informacoesTrabalhistasService.Update(funcionario.InformacoesTrabalhistas);
                }

                if (cnhService.GetByNumCnh(funcionario.Cnh.Num_cnh) == null)
                {
                    cnhService.Insert(funcionario.Cnh);
                }
                else
                {
                    cnhService.AlterarCnh(funcionario.Cnh);
                }

                if (tituloEleitorService.GetByTitulo(funcionario.TituloEleitor.Numero_Titulo) == null)
                {
                    tituloEleitorService.Insert(funcionario.TituloEleitor);
                }
                else
                {
                    tituloEleitorService.AlterarTitulo(funcionario.TituloEleitor);
                }

                var param = new
                {
                    funcionario.Id,
                    funcionario.Nome,
                    funcionario.Cpf,
                    funcionario.Rg,
                    funcionario.Escolaridade,
                    funcionario.Telefone,
                    funcionario.Genero,
                    funcionario.Naturalidade,
                    funcionario.Num_reservista,
                    funcionario.Nome_mae,
                    funcionario.Nome_pai,
                    funcionario.Dt_nascimento,
                    funcionario.TituloEleitor.Numero_Titulo,
                    funcionario.Cnh.Num_cnh,
                    funcionario.Estado_civil,
                    funcionario.Endereco.Rua,
                    funcionario.Endereco.Numero,
                    funcionario.Endereco.Cep,
                    funcionario.Endereco.Bairro,
                    funcionario.Endereco.Cidade,
                    funcionario.Endereco.Uf,
                    funcionario.Endereco.Complemento,
                    funcionario.InformacoesTrabalhistas.ID
                };

                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                db.Execute(Funcionario.UPDATE, param);

                if (funcionario.Id > 0)
                {
                    var func = GetById((int)funcionario.Id).Result.Dados;
                    return new Retorno(true, func, "Dados atualizados com sucesso.");
                }
                else
                {
                    return new Retorno(false, "Ocorreu um erro ao atualizar");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao atualizar: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao atualizar: " + ex.Message);
            }
        }

        public async Task<Retorno> Delete(int id)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var funcionario = db.QueryFirstOrDefault(Funcionario.SELECT_BY_ID, new { Id = id });

                if (!string.IsNullOrEmpty(funcionario?.num_CNH))
                    new CnhService().Delete(funcionario.num_CNH);

                if (!string.IsNullOrEmpty(funcionario?.numero_titulo))
                    new TituloEleitorService().Delete(funcionario.numero_titulo);

                if (!string.IsNullOrEmpty(funcionario?.id_trabalhista))
                    new InformacoesTrabalhistasService().Delete(funcionario.id_trabalhista);

                var qtd = db.Execute(Funcionario.DELETE, new { Id = id });

                if (qtd > 0)
                {
                    return new Retorno(true, "Registro deletado com sucesso");
                }
                else
                {
                    return new Retorno(false, "Registro não encontrado");
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao atualizar: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao atualizar: " + ex.Message);
            }
        }

    }
}
