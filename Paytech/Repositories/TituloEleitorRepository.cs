using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;
using System.Runtime.CompilerServices;

namespace Paytech.Repositories
{
    public class TituloEleitorRepository  : ITituloEleitorRepository
    {
        private string _conn = "Server=paytech.database.windows.net;Database=Paytech;User Id=diego;Password=Paytech2023;";
        public bool Insert(TituloEleitor tituloEleitor)
        {
            using var db = new SqlConnection(_conn);
            db.Execute(TituloEleitor.INSERT, tituloEleitor);
            return true;
        }

        public List<TituloEleitor> GetAll()
        {
            using var db = new SqlConnection(_conn);
            var titulosEleitor = db.Query<TituloEleitor>(TituloEleitor.SELECT_ALL).ToList();
            return titulosEleitor;
        }

        public TituloEleitor GetByTitulo(string numeroTitulo)
        {
            using var db = new SqlConnection(_conn);
            var tituloEleitor = db.QuerySingleOrDefault<TituloEleitor>(TituloEleitor.SELECT_BY_ID, new { Numero_Titulo = numeroTitulo });
            return tituloEleitor;
        }


        public void AlterarTitulo(string numeroTitulo, string secao, string zona)
        {
            var tituloEleitor = GetByTitulo(numeroTitulo);
            tituloEleitor.Secao = secao;
            tituloEleitor.Zona = zona;
            using var db = new SqlConnection(_conn);
            db.Execute(TituloEleitor.UPDATE, tituloEleitor);
        }

        public void Delete(string numeroTitulo)
        {
            var tituloEleitor = GetByTitulo(numeroTitulo);
            using var db = new SqlConnection(_conn);
            db.Execute(TituloEleitor.DELETE, tituloEleitor);
        }

    }
}
