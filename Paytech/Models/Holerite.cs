namespace Paytech.Models
{
    public class Holerite
    {
        public static readonly string INSERT = @"
        INSERT INTO HOLERITE 
        (salario_base, Salario_Base_Inss, desconto_inss, salario_base_ir, desconto_ir, vale_transporte, calculo_hora_extra, desconto_faltas_horas, salario_liquido, 
        deducao_dependente, fgts, faixa_ir, data_calculo, data_competencia_inicio, data_competencia_fim, ID_funcionario) 
        VALUES 
        (@Salario_base, @Salario_Base_Inss, @Desconto_inss, @Salario_base_ir, @Desconto_ir, @Vale_transporte, @Calculo_hora_extra, @Desconto_faltas_horas, @Salario_liquido, 
        @Deducao_dependente, @Fgts, @Faixa_ir, GETDATE(), @Data_competencia_inicio, @Data_competencia_fim, @Id_funcionario
        ); SELECT SCOPE_IDENTITY();
        ";
        public static readonly string SELECT_ALL = "SELECT * FROM HOLERITE";
        public static readonly string SELECT_BY_ID = "SELECT * FROM HOLERITE WHERE id = @Id";
        public static readonly string SELECT_BY_ID_FUNCIONARIO = "SELECT * FROM HOLERITE WHERE id_funcionario = @Id_funcionario";
        public static readonly string SELECT_BY_PERIODO = "SELECT * FROM HOLERITE WHERE data_competencia_inicio >= @Data_Inicio AND data_competencia_fim <= @Data_Fim";
        public static readonly string SELECT_BY_PERIODO_FUNCIONARIO = "SELECT * FROM HOLERITE WHERE id_funcionario = @Id_funcionario AND data_competencia_inicio >= @Data_Inicio AND data_competencia_fim <= @Data_Fim";
        public static readonly string UPDATE = @"UPDATE HOLERITE 
        SET salario_base = @Salario_base, 
        Salario_Base_Inss = @Salario_Base_Inss, 
        desconto_inss = @Desconto_inss, 
        salario_base_ir = @Salario_base_ir, 
        desconto_ir = @Desconto_ir,
        vale_transporte = @Vale_transporte,
        calculo_hora_extra = @Calculo_hora_extra,
        desconto_faltas_horas = @Desconto_faltas_horas,
        salario_liquido = @Salario_liquido,
        deducao_dependente = @Deducao_dependente,
        fgts = @Fgts,
        faixa_ir = @Faixa_ir,
        data_calculo = GETDATE(),
        data_competencia_inicio = @Data_competencia_inicio,
        data_competencia_fim = @Data_competencia_fim,
        id_funcionario = @Id_funcionario
        WHERE id = @Id";
        public static readonly string DELETE = "DELETE FROM HOLERITE WHERE id = @Id";
        public int? ID { get; set; }
        public double? Salario_base { get; set; }
        public double? Salario_Base_Inss { get; set; }
	    public double? Desconto_inss { get; set; }
	    public double? Salario_base_ir { get; set; }
	    public double? Desconto_ir { get; set; }
	    public double? Vale_transporte { get; set; }
	    public double? Calculo_hora_extra { get; set; }
	    public double? Desconto_faltas_horas { get; set; }
	    public double? Salario_liquido { get; set; }
	    public double? Deducao_dependente { get; set; }
	    public double? Fgts  { get; set; }
	    public double? Faixa_ir { get; set; }
        public DateTime? Data_calculo { get; set; }
        public DateTime? Data_competencia_Inicio { get; set; }
        public DateTime? Data_competencia_Fim { get; set; }
        public int? Id_funcionario { get; set; }
    }
}
