namespace Paytech.Models
{
    public class Beneficio
    {
        public static readonly string INSERT = "INSERT INTO Beneficio (ID_beneficio, salario_familia, PLR, Vale_alimentacao, Vale_transporte, Vale_refeicao) " +
            "VALUES (@ID_beneficio, @Salario_familia, @PLR, @Vale_alimentacao, @Vale_transporte, @Vale_refeicao)";
        public static readonly string SELECT_ALL = "SELECT * FROM Beneficio";
        public static readonly string SELECT_BY_ID = "SELECT * FROM Beneficio WHERE ID_beneficio = @ID_beneficio";
        public static readonly string UPDATE = "UPDATE Beneficio SET Salario_familia = @Salario_familia, PLR = @PLR, Vale_alimentacao = @Vale_alimentacao, " +
            "Vale_transporte = @Vale_transporte, Vale_refeicao = @Vale_refeicao  WHERE ID_beneficio = @ID_beneficio";
        public static readonly string DELETE = "DELETE FROM Beneficio WHERE ID_beneficio = @ID_beneficio";
        public int ID_beneficio { get; set; }
        public int Salario_familia { get; set; }
        public float PLR { get; set; }
        public float Vale_alimentacao { get; set; }
        public float Vale_transporte { get; set; }
        public float Vale_refeicao { get; set; }

    }
}
