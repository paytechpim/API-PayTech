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

        public bool Insert(CarteiraTrabalho carteiraTrabalho)
        {
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            db.Execute(CarteiraTrabalho.INSERT, carteiraTrabalho);
            return true;
        }

        public List<CarteiraTrabalho> GetAll()
        {
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            var carteirasTrabalho = db.Query<CarteiraTrabalho>(CarteiraTrabalho.SELECT_ALL).ToList();
            return carteirasTrabalho;
        }

        public CarteiraTrabalho GetById(string numCtps, string UF)
        {
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            var carteiraTrabalho = db.QuerySingleOrDefault<CarteiraTrabalho>(CarteiraTrabalho.SELECT_BY_ID, new { NumCtps = numCtps, UFCarteira = UF });
            return carteiraTrabalho;
        }


        public void AlterarCarteira(string numCtps, string UF, string orgao, string serie, string cbo)
        {
            var carteiraTrabalho = GetById(numCtps, UF);
            carteiraTrabalho.Orgao = orgao;
            carteiraTrabalho.Serie = serie;
            carteiraTrabalho.Cbo = cbo;
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            db.Execute(CarteiraTrabalho.UPDATE, carteiraTrabalho);
        }

        public void Delete(string numCtps, string UF)
        {
            var carteiraTrabalho = GetById(numCtps, UF);
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            db.Execute(CarteiraTrabalho.DELETE, carteiraTrabalho);
        }
    }
}
