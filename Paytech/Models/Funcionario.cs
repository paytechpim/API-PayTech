namespace Paytech.Models
{
    public class Funcionario
    {
        public static readonly string SELECT_ALL = "SELECT * FROM Funcionario";
        public static readonly string INSERT = @"
        INSERT INTO Funcionario (
            nome, ID_folha_pagamento, dt_FGTS, dt_nascimento, dt_admissao, naturalidade, funcao,
            PIS, forma_pagamento, CPF, num_CNH, numero_titulo, nome_mae, nome_pai, num_reservista,
            salario, genero, RG, escolaridade, estado_civil, telefone, enderecoRua, enderecoNumero, 
            enderecoBairro, enderecoCEP, enderecoCidade
        )
        VALUES (
            @Nome, @CarteiraTrabalho, @DataFgts, @DataNasc, @DataAdmissao, @Naturalidade, @Funcao,
            @NumPis, @FormaPagamento, @Cpf, @Cnh, @TituloEleitor, @NomeMae, @NomePai, @NumReservista,
            @Salario, @Genero, @Rg, @Escolaridade, @EstadoCivil, @Telefone, @Endereco, @enderecoRua, 
            @enderecoNumero, @enderecoBairro, @enderecoCEP, @enderecoCidade
        )"; 
        public static readonly string DELETE = "DELETE FROM Funcionario WHERE ID = @Id";
        public static readonly string SELECT_BY_NAME = "SELECT * FROM Funcionarios WHERE nome = @Nome";
        public static readonly string SELECT_BY_ID = "SELECT * FROM Funcionarios WHERE ID = @Id";

        public string Id { get; set; }
        public string Nome { get; set; }
        public CarteiraTrabalho CarteiraTrabalho { get; set; }
        public DateOnly DataFgts { get; set;}
        public DateOnly DataNasc { get; set; }
        public DateOnly DataAdmissao { get; set; }
        public string Naturalidade { get; set; }
        public string Funcao { get; set; }
        public string NumPis { get; set; }
        public string FormaPagamento { get; set; }
        public string Cpf { get; set; }
        public Cnh Cnh { get; set; }
        public TituloEleitor TituloEleitor { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public string NumReservista { get; set; }
        public float Salario { get; set; }
        public string Genero { get; set; }
        public string Rg { get; set; }
        public string Escolaridade { get; set; }
        public string EstadoCivil { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
        //public int HorasTrabalhadas { get; set; }
        public string Username { get; set; }





    }
}
