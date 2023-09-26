using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;
using System.Collections.Generic;

namespace Paytech.Repositories
{
    public class CnhRepository : ICnhRepository
    {
        private string _conn = "Server=paytech.database.windows.net;Database=Paytech;User Id=diego;Password=Paytech2023;";
        public async Task<Cnh> Insert(Cnh cnh)
        {
            using var db = new SqlConnection(_conn);
            db.Execute(Cnh.INSERT, cnh);
            return cnh;
        }

        public List<Cnh> GetAll()
        {
            using var db = new SqlConnection(_conn);
            var cnh_s = db.Query<Cnh>(Cnh.SELECT_ALL).ToList();
            return cnh_s;
        }

        public Cnh GetByNumCnh(string num_cnh)
        {
            using var db = new SqlConnection(_conn);
            var cnh = db.QuerySingleOrDefault<Cnh>(Cnh.SELECT_BY_ID, new { Num_cnh = num_cnh });
            return cnh;
        }


        public void AlterarCnh(string num_cnh, string categoria, DateTime? dt_emissao, DateTime? dt_vencimento)
        {
            var cnh = GetByNumCnh(num_cnh);
            cnh.Categoria = categoria;
            cnh.Dt_emissao = dt_emissao;
            cnh.Dt_vencimento = dt_vencimento;
            using var db = new SqlConnection(_conn);
            db.Execute(Cnh.UPDATE, cnh);
        }

        public void Delete(string num_cnh)
        {
            var cnh = GetByNumCnh(num_cnh);
            using var db = new SqlConnection(_conn);
            db.Execute(Cnh.DELETE, cnh);
        }

    }
}
