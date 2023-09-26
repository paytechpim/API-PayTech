using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;
using Paytech.Services;

namespace Paytech.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private string _conn = "Server=paytech.database.windows.net;Database=Paytech;User Id=diego;Password=Paytech2023;";

        public async Task<Funcionario> Insert(Funcionario funcionario)
        {
            var cnhService = new CnhService();
            var cnh = cnhService.GetByNumCnh(funcionario.Cnh.Num_cnh);
            if(cnh == null)
            {
            cnh = await cnhService.Insert(funcionario.Cnh);
            }
            funcionario.Cnh = cnh;
            using var db = new SqlConnection(_conn);
            db.Execute(Funcionario.INSERT, funcionario);
            return funcionario;
        }

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
        public List<Funcionario> GetByName(string nome)
        {
            using var db = new SqlConnection(_conn);
            return db.Query<Funcionario>(Funcionario.SELECT_BY_NAME).AsList();
        }


        public void Delete(int id)
        {
            Funcionario funcionario = GetById(id);
            using var db = new SqlConnection(_conn);
            db.Execute(Funcionario.DELETE, funcionario);
        }

    }
}
