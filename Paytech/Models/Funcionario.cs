namespace Paytech.Models
{
    public class Funcionario
    {
        public static readonly string SELECT_ALL = "SELECT * FROM Funcionario";
        //public static readonly string INSERT = @"
        //INSERT INTO Funcionario (
        //    nome, CPF, RG, escolaridade, forma_pagamento, salario, telefone, genero, naturalidade, num_reservista, 
        //    nome_mae, nome_pai, dt_admissao, dt_nascimento, dt_FGTS, numero_titulo, num_CNH, NumCtps, UFCarteira, 
        //    funcao, estado_civil, ID_endereco
        //)
        //VALUES (
        //    @Nome, @Cpf, @Rg, @Escolaridade, @FormaPagamento, @Salario, @Telefone, @Genero, @Naturalidade, @NumReservista,
        //    @NomeMae, @NomePai, @DataAdmissao, @DataNasc, @DataFgts, @TituloEleitor, @Cnh, @CarteiraTrabalho, 
        //    @Funcao, @EstadoCivil, @Endereco
        //)"; 
        public static readonly string INSERT = @"
        INSERT INTO Funcionario (
            ID, nome, CPF, RG, escolaridade, forma_pagamento, salario, telefone, genero, naturalidade, num_reservista, 
            nome_mae, nome_pai, dt_admissao, dt_nascimento, dt_FGTS, num_CNH, funcao, estado_civil 
        )
        VALUES (
            @Id, @Nome, @Cpf, @Rg, @Escolaridade, @Forma_pagamento, @Salario, @Telefone, @Genero, @Naturalidade, @Num_reservista,
            @nome_mae, @nome_pai, @dt_admissao, @dt_nascimento, @dt_FGTS, @Num_cnh, @Funcao, @estado_civil
        )";

        public static readonly string DELETE = "DELETE FROM Funcionario WHERE ID = @Id";
        public static readonly string SELECT_BY_NAME = "SELECT * FROM Funcionarios WHERE nome = @Nome";
        public static readonly string SELECT_BY_ID = "SELECT * FROM Funcionarios WHERE ID = @Id";

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Escolaridade { get; set; }
        public string Forma_pagamento { get; set; }
        public float Salario { get; set; }
        public string Telefone { get; set; }
        public string Genero { get; set; }
        public string Naturalidade { get; set; }
        public string Num_reservista { get; set; }
        public string? Nome_mae { get; set; }
        public string? Nome_pai { get; set; }
        public DateTime? Dt_admissao { get; set; }
        public DateTime? Dt_nascimento { get; set; }
        public DateTime? Dt_FGTS { get; set;}
        //public TituloEleitor TituloEleitor { get; set; }
        //public CarteiraTrabalho? CarteiraTrabalho { get; set; }
        public Cnh Cnh { get; set; }
        public string Funcao { get; set; }
        public string Estado_civil { get; set; }
        //public Endereco Endereco { get; set; }
        public int HorasTrabalhadas { get; set; }

    }
}
