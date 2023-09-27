using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;
using System.Runtime.CompilerServices;

namespace Paytech.Repositories
{
    public class TituloEleitorRepository : ITituloEleitorRepository
    {
        private IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        public async Task<TituloEleitor> Insert(TituloEleitor tituloEleitor)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                db.Execute(TituloEleitor.INSERT, tituloEleitor);
                return tituloEleitor;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public List<TituloEleitor> GetAll()
        {
            try
            {
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            var titulosEleitor = db.Query<TituloEleitor>(TituloEleitor.SELECT_ALL).ToList();
            return titulosEleitor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public TituloEleitor GetByTitulo(string numeroTitulo)
        {
            try
            {
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            var tituloEleitor = db.QuerySingleOrDefault<TituloEleitor>(TituloEleitor.SELECT_BY_ID, new { Numero_Titulo = numeroTitulo });
            return tituloEleitor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }


        public void AlterarTitulo(string numeroTitulo, string secao, string zona)
        {
            try
            {
            var tituloEleitor = GetByTitulo(numeroTitulo);
            tituloEleitor.Secao = secao;
            tituloEleitor.Zona = zona;
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            db.Execute(TituloEleitor.UPDATE, tituloEleitor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void Delete(string numeroTitulo)
        {
            try
            {
            var tituloEleitor = GetByTitulo(numeroTitulo);
            using var db = new SqlConnection(configuration.GetConnectionString("sql"));
            db.Execute(TituloEleitor.DELETE, tituloEleitor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

    }
}
