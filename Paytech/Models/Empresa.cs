namespace Paytech.Models
{
    public class Empresa
    {
        public static readonly string INSERT = @"
        INSERT INTO EMPRESA 
        (razao, Cnpj, Tipo_atividade, Endereco, Cep, Regime) 
        VALUES 
        (@Razao, @Cnpj, @Tipo_atividade, @Endereco, @Cep, @Regime
        ); SELECT SCOPE_IDENTITY();
        ";
        public static readonly string SELECT_ALL = "SELECT * FROM EMPRESA";
        public static readonly string SELECT_BY_ID = "SELECT * FROM EMPRESA WHERE id = @Id";
        public static readonly string SELECT_BY_CNPJ = "SELECT * FROM EMPRESA WHERE cnpj = @Cnpj";
        public static readonly string UPDATE = @"UPDATE EMPRESA 
        SET razao = @Razao, 
        Cnpj = @Cnpj, 
        Tipo_atividade = @Tipo_atividade, 
        Endereco = @Endereco,
        Cep = @Cep,
        Regime = @Regime
        WHERE id = @Id";
        public static readonly string DELETE = "DELETE FROM EMPRESA WHERE id = @Id";
        public int ID { get; set; }
        public string Razao { get; set; }
        public string Cnpj { get; set; }
        public string Tipo_atividade { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public string Regime { get; set; }
    }
}
