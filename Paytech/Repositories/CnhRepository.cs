using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Paytech.Models;
using System.Collections.Generic;

namespace Paytech.Repositories
{
    public class CnhRepository : ICnhRepository
    {
        private IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        public async Task<Cnh> Insert(Cnh cnh)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                db.Execute(Cnh.INSERT, cnh);
                return cnh;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public List<Cnh> GetAll()
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var cnh_s = db.Query<Cnh>(Cnh.SELECT_ALL).ToList();
                return cnh_s;
            }

            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public Cnh GetByNumCnh(string num_cnh)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                return db.QuerySingleOrDefault<Cnh>(Cnh.SELECT_BY_ID, new { Num_cnh = num_cnh });
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }


        public void AlterarCnh(Cnh cnh)
        {
            try
            {           
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            db.Execute(Cnh.UPDATE, cnh);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void Delete(string num_cnh)
        {
            try
            {
            var cnh = GetByNumCnh(num_cnh);
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            db.Execute(Cnh.DELETE, cnh);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}
