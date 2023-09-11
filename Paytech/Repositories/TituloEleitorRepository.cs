using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;
using System.Runtime.CompilerServices;

namespace Paytech.Repositories
{
    public class TituloEleitorRepository  : ITituloEleitorRepository
    {
        private string _conn = "Server=paytech.database.windows.net;Database=Paytech;User Id=diego;Password=Paytech2023;";

        public List<TituloEleitor> GetAll()
        {
            List<TituloEleitor> list = new();

            using var db = new SqlConnection(_conn);
            var TituloEleitors = db.Query<TituloEleitor>(TituloEleitor.SELECT_ALL);
            return (List<TituloEleitor>)TituloEleitors;
        }

        public TituloEleitor GetByTitulo(string numeroTitulo)
        {
            using var db = new SqlConnection(_conn);
            var tituloEleitor = db.QuerySingleOrDefault<TituloEleitor>(TituloEleitor.SELECT_BY_ID);
            return tituloEleitor;
        }

        public bool Insert(TituloEleitor TituloEleitor)
        {
            using var db = new SqlConnection(_conn);
            db.Execute(TituloEleitor.INSERT, TituloEleitor);
            return true;
        }

        public void AlterarTitulo(string numeroTitulo)
        {
            TituloEleitor TituloEleitor = GetByTitulo(numeroTitulo);
            using var db = new SqlConnection(_conn);
            db.Execute(TituloEleitor.UPDATE, TituloEleitor);
        }

        public void Delete(string numeroTitulo)
        {
            TituloEleitor TituloEleitor = GetByTitulo(numeroTitulo);
            using var db = new SqlConnection(_conn);
            db.Execute(TituloEleitor.DELETE, TituloEleitor);
        }

    }
}
