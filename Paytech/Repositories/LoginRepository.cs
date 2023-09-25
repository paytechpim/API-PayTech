using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;

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
            List<Login> list = new();

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

        public bool Insert(Login login)
        {
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            db.Execute(Login.INSERT, login);
            return true;
        }


        public void Delete(string username)
        {
            Login login = GetByUsername(username);
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            db.Execute(Login.DELETE, login);
        }
    }
}
