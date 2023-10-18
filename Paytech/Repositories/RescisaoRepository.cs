using Microsoft.Data.SqlClient;
using Paytech.Models;
using Paytech.Utils;
using Dapper;

namespace Paytech.Repositories
{
    public class RescisaoRepository : IRescisaoRepository
    {
        private IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

        public async Task<Retorno> Insert(Rescisao rescisao)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                int id = db.Query<int>(Rescisao.INSERT, rescisao).Single();

                if (id > 0)
                {
                    var entity = GetById(id).Result.Dados;
                    return new Retorno(true, entity, "Dados inseridos com sucesso.");
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

        public async Task<Retorno> Update(Rescisao rescisao)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var rows = db.Execute(Rescisao.UPDATE, rescisao);
                if (rows > 0)
                {
                    return new Retorno(true, GetById((int)rescisao.ID).Result.Dados, "Dados atualizados com sucesso.");
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
                var rows = db.Execute(Rescisao.DELETE, new { Id = id });
                if (rows > 0)
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
                return new Retorno(false, "Ocorreu um erro ao excluir: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao excluir: " + ex.Message);
            }
        }

        public async Task<Retorno> GetAll()
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var entity = db.Query<Rescisao>(Rescisao.SELECT_ALL).ToList();
                return new Retorno(true, entity, "Dados pesquisado com sucesso.");
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

        public async Task<Retorno> GetById(int id)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var entity = db.QuerySingleOrDefault<Rescisao>(Rescisao.SELECT_BY_ID, new { Id = id });
                return new Retorno(true, entity, "Dados pesquisado com sucesso.");
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

        public async Task<Retorno> GetByIdFuncionario(int idFuncionario)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var entity = db.Query<Rescisao>(Rescisao.SELECT_BY_ID_FUNCIONARIO, new { Id_funcionario = idFuncionario }).ToList();
                return new Retorno(true, entity, "Dados pesquisado com sucesso.");
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
    }
}
