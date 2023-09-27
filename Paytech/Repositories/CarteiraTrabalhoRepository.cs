using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;

namespace Paytech.Repositories
{
    public class CarteiraTrabalhoRepository : ICarteiraTrabalhoRepository
    {
        private IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        public async Task<CarteiraTrabalho> Insert(CarteiraTrabalho carteiraTrabalho)
        {
            try
            {
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            db.Execute(CarteiraTrabalho.INSERT, carteiraTrabalho);
            return carteiraTrabalho;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public List<CarteiraTrabalho> GetAll()
        {
            try
            {
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            var carteirasTrabalho = db.Query<CarteiraTrabalho>(CarteiraTrabalho.SELECT_ALL).ToList();
            return carteirasTrabalho;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public CarteiraTrabalho GetById(string numCtps, string UF)
        {
            try
            {
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            var carteiraTrabalho = db.QuerySingleOrDefault<CarteiraTrabalho>(CarteiraTrabalho.SELECT_BY_ID, new { NumCtps = numCtps, UFCarteira = UF });
            return carteiraTrabalho;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }


        public void AlterarCarteira(string numCtps, string UF, string orgao, string serie, string cbo)
        {
            try
            {
            var carteiraTrabalho = GetById(numCtps, UF);
            carteiraTrabalho.Orgao = orgao;
            carteiraTrabalho.Serie = serie;
            carteiraTrabalho.Cbo = cbo;
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            db.Execute(CarteiraTrabalho.UPDATE, carteiraTrabalho);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void Delete(string numCtps, string UF)
        {
            try
            {
            var carteiraTrabalho = GetById(numCtps, UF);
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            db.Execute(CarteiraTrabalho.DELETE, carteiraTrabalho);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}
