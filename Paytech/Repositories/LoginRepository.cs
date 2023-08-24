using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;

namespace Paytech.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private string _conn = "Server=paytech.database.windows.net;Database=Paytech;User Id=diego;Password=Paytech2023;";

        public List<Login> GetAll()
        {
            List<Login> list = new();

            using var db = new SqlConnection(_conn);
            var logins = db.Query<Login>(Login.SELECT_ALL);
            return (List<Login>)logins;
        }

        public Login GetByUsername(string username)
        {
            using var db = new SqlConnection(_conn);
            var login = db.QuerySingleOrDefault<Login>(Login.SELECT_BY_USERNAME);
            return login;
        }

        public bool Insert(Login login)
        {
            using var db = new SqlConnection(_conn);
            db.Execute(Login.INSERT, login);
            return true;
        }


        public void Delete(string username)
        {
            Login login = GetByUsername(username);
            using var db = new SqlConnection(_conn);
            db.Execute(Login.DELETE, login);
        }
    }
}
