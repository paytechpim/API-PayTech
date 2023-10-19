namespace Paytech.Models
{
    public class InformacoesTrabalhistas
    {
        public static readonly string INSERT = @"
        INSERT INTO INFORMACOES_TRABALHISTAS 
        (NumCtps, UFCarteira, Orgao, Serie, num_pis, dt_admissao, salario_bruto, qtd_dependentes, 
        opt_vale_transporte, funcao) 
        VALUES 
        (@NumCtps, @UFCarteira, @Orgao, @Serie, @Num_pis, @Dt_admissao, @Salario_Bruto, @Qtd_Dependentes, 
        @Opt_Vale_Transporte, @funcao
        ); SELECT SCOPE_IDENTITY();
        ";
        public static readonly string SELECT_ALL = "SELECT * FROM INFORMACOES_TRABALHISTAS";
        public static readonly string SELECT_BY_ID = "SELECT * FROM INFORMACOES_TRABALHISTAS WHERE id = @Id";
        public static readonly string UPDATE = @"UPDATE INFORMACOES_TRABALHISTAS 
        SET NumCtps = @NumCtps, 
        UFCarteira = @UFCarteira, 
        Orgao = @Orgao, 
        Serie = @Serie,
        num_pis = @Num_pis,
        dt_admissao = @Dt_admissao,
        salario_bruto = @Salario_Bruto,
        qtd_dependentes = @Qtd_Dependentes,
        opt_vale_transporte = @Opt_Vale_Transporte,
        funcao = @funcao
        WHERE id = @Id";
        public static readonly string DELETE = "DELETE FROM INFORMACOES_TRABALHISTAS WHERE id = @Id";
        public int? ID { get; set; }
        public string? NumCtps { get; set; }
        public string? UFCarteira { get; set; }
        public string? Orgao { get; set; }
        public string? Serie { get; set; }
        public string? Num_pis { get; set; }
        public DateTime? Dt_admissao { get; set; }
        public double? Salario_Bruto { get; set; }
        public int? Qtd_Dependentes { get; set; }
        public bool? Opt_Vale_Transporte { get; set; }
        public string? funcao { get; set; }
    }
}
