using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;

namespace Paytech.Repositories
{
    public class CarteiraTrabalhoRepository : ICarteiraTrabalhoRepository
    {
        private string _conn = "Server=paytech.database.windows.net;Database=Paytech;User Id=diego;Password=Paytech2023;";
        public bool Insert(CarteiraTrabalho carteiraTrabalho)
        {
            using var db = new SqlConnection(_conn);
            db.Execute(CarteiraTrabalho.INSERT, carteiraTrabalho);
            return true;
        }

        public List<CarteiraTrabalho> GetAll()
        {
            using var db = new SqlConnection(_conn);
            var carteirasTrabalho = db.Query<CarteiraTrabalho>(CarteiraTrabalho.SELECT_ALL).ToList();
            return carteirasTrabalho;
        }

        public CarteiraTrabalho GetById(string numCtps, string UF)
        {
            using var db = new SqlConnection(_conn);
            var carteiraTrabalho = db.QuerySingleOrDefault<CarteiraTrabalho>(CarteiraTrabalho.SELECT_BY_ID, new { NumCtps = numCtps, UFCarteira = UF });
            return carteiraTrabalho;
        }


        public void AlterarCarteira(string numCtps, string UF, string orgao, string serie, string cbo)
        {
            var carteiraTrabalho = GetById(numCtps, UF);
            carteiraTrabalho.Orgao = orgao;
            carteiraTrabalho.Serie = serie;
            carteiraTrabalho.Cbo = cbo;
            using var db = new SqlConnection(_conn);
            db.Execute(CarteiraTrabalho.UPDATE, carteiraTrabalho);
        }

        public void Delete(string numCtps, string UF)
        {
            var carteiraTrabalho = GetById(numCtps, UF);
            using var db = new SqlConnection(_conn);
            db.Execute(CarteiraTrabalho.DELETE, carteiraTrabalho);
        }
    }
}
