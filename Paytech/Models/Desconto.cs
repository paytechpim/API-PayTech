namespace Paytech.Models
{
    public class Desconto
    {
        public static readonly string INSERT = "INSERT INTO Desconto (ID_desconto, FGTS, INSS, IR, Decimo_terceiro, Falta, Adiantamento_salario) " +
    "VALUES (@ID_desconto, @FGTS, @INSS, @IR, @Decimo_terceiro, @Falta, @Adiantamento_salario)";
        public static readonly string SELECT_ALL = "SELECT * FROM Desconto";
        public static readonly string SELECT_BY_ID = "SELECT * FROM Desconto WHERE ID_desconto = @ID_desconto";
        public static readonly string UPDATE = "UPDATE Desconto SET FGTS = @FGTS, INSS = @INSS, Decimo_terceiro = @Decimo_terceiro, " +
            "Falta = @Falta, Adiantamento_salario = @Adiantamento_salario  WHERE ID_desconto = @ID_desconto";
        public static readonly string DELETE = "DELETE FROM Desconto WHERE ID_desconto = @ID_desconto";
        public int ID_desconto { get; set; }
        public float FGTS { get; set; }
        public float INSS { get; set; }
        public float IR { get; set; }
        public float Decimo_terceiro { get; set; }
        public float Falta { get; set; }
        public float Adiantamento_salario { get; set; }

    }
}
