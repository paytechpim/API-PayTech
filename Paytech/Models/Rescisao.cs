namespace Paytech.Models
{
    public class Rescisao
    {
        public static readonly string INSERT = @"
        INSERT INTO RESCISAO 
        (saldo_salario_rescisao, decimo_terceiro_rescisao, ferias_rescisao, meses_ferias, aviso_previo_rescisao, hora_extra_rescisao, desconto_faltas, fgts, 
        desconto_inss_rescisao, salario_base_inss_rescisão, salario_base_irrf_rescisao, desconto_irrf_rescisao, data_calculo, data_competencia, ID_funcionario) 
        VALUES 
        (@Saldo_salario_rescisao, @Decimo_terceiro_rescisao, @Ferias_rescisao, @Meses_ferias, @Aviso_previo_rescisao, @Hora_extra_rescisao, @Desconto_faltas, @Fgts, 
        @Desconto_inss_rescisao, @salario_base_inss_rescisão, @Salario_base_irrf_rescisao, @Desconto_irrf_rescisao, GETDATE(), @Data_competencia, @Id_funcionario
        ); SELECT SCOPE_IDENTITY();
        ";
        public static readonly string SELECT_ALL = "SELECT * FROM RESCISAO";
        public static readonly string SELECT_BY_ID = "SELECT * FROM RESCISAO WHERE id = @Id";
        public static readonly string SELECT_BY_ID_FUNCIONARIO = "SELECT * FROM RESCISAO WHERE id_funcionario = @Id_funcionario";
        public static readonly string UPDATE = @"UPDATE RESCISAO 
        SET saldo_salario_rescisao = @Saldo_salario_rescisao, 
        Decimo_terceiro_rescisao = @Decimo_terceiro_rescisao, 
        Ferias_rescisao = @Ferias_rescisao, 
        Meses_ferias = @Meses_ferias,
        Aviso_previo_rescisao = @Aviso_previo_rescisao,
        Hora_extra_rescisao = @Hora_extra_rescisao,
        Desconto_faltas = @Desconto_faltas,
        Fgts = @Fgts,
        Desconto_inss_rescisao = @Desconto_inss_rescisao,
        salario_base_inss_rescisão = @salario_base_inss_rescisão,
        Salario_base_irrf_rescisao = @Salario_base_irrf_rescisao,
        Desconto_irrf_rescisao = @Desconto_irrf_rescisao,
        data_calculo = GETDATE(),
        data_competencia = @Data_competencia,
        id_funcionario = @Id_funcionario
        WHERE id = @Id";
        public static readonly string DELETE = "DELETE FROM RESCISAO WHERE id = @Id";
        public int ID { get; set; }
        public double Saldo_salario_rescisao { get; set; }
		public double Decimo_terceiro_rescisao { get; set; }
		public double Ferias_rescisao { get; set; }
		public double Meses_ferias { get; set; }
		public double Aviso_previo_rescisao { get; set; }
		public double Hora_extra_rescisao { get; set; }
		public double Desconto_faltas { get; set; }
		public double Fgts { get; set; }
		public double Desconto_inss_rescisao { get; set; }
		public double Salario_base_inss_rescisão { get; set; }
		public double Salario_base_irrf_rescisao { get; set; }
		public double Desconto_irrf_rescisao { get; set; }
		public DateTime Data_calculo { get; set; }
        public DateTime Data_competencia { get; set; }
        public int ID_funcionario { get; set; }
    }
}
