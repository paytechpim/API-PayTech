using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;

namespace Paytech.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private string _conn = "Server=paytech.database.windows.net;Database=Paytech;User Id=diego;Password=Paytech2023;";

        public List<Funcionario> GetAll()
        {
            using var db = new SqlConnection(_conn);
            return db.Query<Funcionario>(Funcionario.SELECT_ALL).AsList();
        }

        public Funcionario GetById(int id)
        {
            using var db = new SqlConnection(_conn);
            return db.QueryFirstOrDefault<Funcionario>(Funcionario.SELECT_BY_ID);
        }
        public Funcionario GetByName(string nome)
        {
            using var db = new SqlConnection(_conn);
            return db.QueryFirstOrDefault<Funcionario>(Funcionario.SELECT_BY_NAME);
        }

        public bool Insert(Funcionario funcionario)
        {
            using var db = new SqlConnection(_conn);
            db.Execute(Funcionario.INSERT, funcionario);
            return true;
        }

        public void Delete(int id)
        {
            Funcionario funcionario = GetById(id);
            using var db = new SqlConnection(_conn);
            db.Execute(Funcionario.DELETE, new { Id = id });
        }

        List<Funcionario> IFuncionarioRepository.GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
