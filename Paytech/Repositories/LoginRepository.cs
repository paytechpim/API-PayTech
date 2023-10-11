using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Paytech.Models;
using Paytech.Utils;
using System;

namespace Paytech.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        public List<Login> GetAll()
        {
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            var logins = db.Query<Login>(Login.SELECT_ALL);
            return (List<Login>)logins;
        }

        public Login GetByUsername(string Username)
        {
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            var login = db.QuerySingleOrDefault<Login>(Login.SELECT_BY_USERNAME, new { Nome_Usuario = Username });
            return login;
        }

        public async Task<Retorno> GetByFuncionario(int Id_Funcionario)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var login = db.QuerySingleOrDefault<Login>(Login.SELECT_BY_FUNCIONARIO, new { Id_Funcionario = Id_Funcionario });

                return new Retorno(true, login, "Dados consultados com sucesso!");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao consultar: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao consultar: " + ex.Message);
            }
        }

        public async Task<Retorno> IsUserNameExist(string Username)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var rows = db.Query(Login.SELECT_USERNAME_EXIST, new { Nome_Usuario = Username });

                return new Retorno(true, rows.IsNullOrEmpty() ? false : true, "Dados consultados com sucesso!");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao consultar: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao consultar: " + ex.Message);
            }
        }

        public async Task<Retorno> GetById(int Id)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var login = db.QuerySingleOrDefault<Login>(Login.SELECT_BY_ID, new { Id = Id });

                return new Retorno(true, login, "Dados consultados com sucesso!");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao consultar: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao consultar: " + ex.Message);
            }
        }

        public async Task<Retorno> Insert(Login login)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                int id = db.Query<int>(Login.INSERT, login).Single();

                if (id > 0)
                {
                    var lo = GetById(id).Result.Dados;
                    return new Retorno(true, lo, "Dados inseridos com sucesso.");
                }
                else
                {
                    return new Retorno(false, "Ocorreu um erro ao inserir");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao consultar: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao consultar: " + ex.Message);
            }
        }

        public void Delete(string username)
        {
            Login login = GetByUsername(username);
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            db.Execute(Login.DELETE, login);
        }

        public async Task<Retorno> AlterarLogin(Login login)
        {
            try
            {
                var rows = 0;
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));

                if(login.Senha == "")
                {
                    rows = db.Execute(Login.UPDATE, login);
                }
                else
                {
                    rows = db.Execute(Login.UPDATE_SENHA, login);
                }
                

                if (rows > 0)
                {
                    var result = GetByFuncionario((int)login.Id_Funcionario).Result.Dados;
                    return new Retorno(true, result, "Dados atualizados com sucesso.");
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
    }
}
