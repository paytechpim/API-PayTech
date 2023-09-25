using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;

namespace Paytech.Repositories
{
    public class DescontoRepository : IDescontoRepository
    {
        private string _conn = "Server=paytech.database.windows.net;Database=Paytech;User Id=diego;Password=Paytech2023;";
        public bool Insert(Desconto desconto)
        {
            using var db = new SqlConnection(_conn);
            db.Execute(Desconto.INSERT, desconto);
            return true;
        }

        public List<Desconto> GetAll()
        {
            using var db = new SqlConnection(_conn);
            var descontos = db.Query<Desconto>(Desconto.SELECT_ALL).ToList();
            return descontos;
        }

        public Desconto GetById(int id_desconto)
        {
            using var db = new SqlConnection(_conn);
            var desconto = db.QuerySingleOrDefault<Desconto>(Desconto.SELECT_BY_ID, new { ID_desconto = id_desconto });
            return desconto;
        }


        public void AlterarDesconto(int id_desconto, float fgts, float inss, float ir, float decimo_terceiro, float adiantamento_salario)
        {
            var Desconto = GetById(id_desconto);
            Desconto.FGTS = fgts;
            Desconto.INSS = inss;
            Desconto.IR = ir;
            Desconto.Decimo_terceiro = decimo_terceiro;
            Desconto.Adiantamento_salario = adiantamento_salario;
            using var db = new SqlConnection(_conn);
            db.Execute(Desconto.UPDATE, Desconto);
        }

        public void Delete(int id_desconto)
        {
            var desconto = GetById(id_desconto);
            using var db = new SqlConnection(_conn);
            db.Execute(Desconto.DELETE, desconto);
        }

    }
}
