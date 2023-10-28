using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;
using Paytech.Utils;

namespace Paytech.Repositories
{
    public class HoleriteRepository : IHoleriteRepository
    {
        private IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

        public async Task<Retorno> Insert(Holerite holerite)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                int id = db.Query<int>(Holerite.INSERT, holerite).Single();

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

        public async Task<Retorno> Update(Holerite holerite)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var rows = db.Execute(Holerite.UPDATE, holerite);
                if (rows > 0)
                {
                    return new Retorno(true, GetById((int)holerite.ID).Result.Dados, "Dados atualizados com sucesso.");
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
                var rows = db.Execute(Holerite.DELETE, new { Id = id });
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
                var entity = db.Query<Holerite>(Holerite.SELECT_ALL).ToList();
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
                var entity = db.QuerySingleOrDefault<Holerite>(Holerite.SELECT_BY_ID, new { Id = id });
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
                var entity = db.Query<Holerite>(Holerite.SELECT_BY_ID_FUNCIONARIO, new { Id_funcionario = idFuncionario }).ToList();
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

        public int GetQtdFerias(int idFuncionario)
        {
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            return db.QuerySingle<int>(Holerite.SELECT_QTD_FERIAS, new { Id_funcionario = idFuncionario });
        }
    }
}
