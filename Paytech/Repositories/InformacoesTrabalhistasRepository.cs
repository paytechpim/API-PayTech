using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;
using Paytech.Utils;

namespace Paytech.Repositories
{
    public class InformacoesTrabalhistasRepository : IInformacoesTrabalhistasRepository
    {
        private IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

        public async Task<Retorno> Insert(InformacoesTrabalhistas info)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                int id = db.Query<int>(InformacoesTrabalhistas.INSERT, info).Single();

                if (id > 0)
                {
                    var infoTrab = GetById(id).Result.Dados;
                    return new Retorno(true, infoTrab, "Dados inseridos com sucesso.");
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

        public async Task<Retorno> GetAll()
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var infoTrab = db.Query<InformacoesTrabalhistas>(InformacoesTrabalhistas.SELECT_ALL).ToList();
                return new Retorno(true, infoTrab, "Dados pesquisado com sucesso.");
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
                var infoTrab = db.QuerySingleOrDefault<InformacoesTrabalhistas>(InformacoesTrabalhistas.SELECT_BY_ID, new { Id = id });
                return new Retorno(true, infoTrab, "Dados pesquisado com sucesso.");
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


        public async Task<Retorno> Update(InformacoesTrabalhistas info)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var rows = db.Execute(InformacoesTrabalhistas.UPDATE, info);
                if (rows > 0)
                {
                    return new Retorno(true, info, "Dados atualizados com sucesso.");
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
                var rows = db.Execute(InformacoesTrabalhistas.DELETE, new { Id = id});
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
