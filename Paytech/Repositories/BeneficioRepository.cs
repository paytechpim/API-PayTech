using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;

namespace Paytech.Repositories
{
    public class BeneficioRepository : IBeneficioRepository
    {
        private string _conn = "Server=paytech.database.windows.net;Database=Paytech;User Id=diego;Password=Paytech2023;";
        public bool Insert(Beneficio Beneficio)
        {
            using var db = new SqlConnection(_conn);
            db.Execute(Beneficio.INSERT, Beneficio);
            return true;
        }

        public List<Beneficio> GetAll()
        {
            using var db = new SqlConnection(_conn);
            var beneficios = db.Query<Beneficio>(Beneficio.SELECT_ALL).ToList();
            return beneficios;
        }

        public Beneficio GetById(int id_beneficio)
        {
            using var db = new SqlConnection(_conn);
            var beneficio = db.QuerySingleOrDefault<Beneficio>(Beneficio.SELECT_BY_ID, new { ID_beneficio = id_beneficio });
            return beneficio;
        }


        public void AlterarBeneficio(int id_beneficio, int salario_familia, float plr, float vale_alimentacao, float vale_transporte, float vale_refeicao)
        {
            var Beneficio = GetById(id_beneficio);
            Beneficio.Salario_familia = salario_familia;
            Beneficio.PLR = plr;
            Beneficio.Vale_alimentacao = vale_alimentacao;
            Beneficio.Vale_transporte = vale_transporte;
            Beneficio.Vale_refeicao = vale_refeicao;
            using var db = new SqlConnection(_conn);
            db.Execute(Beneficio.UPDATE, Beneficio);
        }

        public void Delete(int id_beneficio)
        {
            var beneficio = GetById(id_beneficio);
            using var db = new SqlConnection(_conn);
            db.Execute(Beneficio.DELETE, beneficio);
        }

    }
}
