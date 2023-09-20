namespace Paytech.Models
{
    public class Cnh
    {
            
        public static readonly string INSERT = "INSERT INTO Cnh (num_CNH, categoria, dt_emissao, dt_vencimento) VALUES (@num_CNH, @categoria, @dt_emissao, @dt_vencimento)";
        public static readonly string SELECT_ALL = "SELECT * FROM Cnh";
        public static readonly string SELECT_BY_ID = "SELECT * FROM Cnh WHERE num_CNH = @num_CNH";
        public static readonly string UPDATE = "UPDATE Cnh SET categoria = @categoria, dt_emissao = @dt_emissao, dt_vencimento = @dt_vencimento WHERE num_CNH = @num_CNH";
        public static readonly string DELETE = "DELETE FROM Cnh WHERE num_CNH = @num_CNH";
        public string Num_cnh { get; set; }
        public string Categoria { get; set; }
        public DateTime? Dt_emissao { get; set; }
        public DateTime? Dt_vencimento { get; set; }



    }
}
