namespace Paytech.Models
{
    public class Funcionario
    {
        public static readonly string SELECT_ALL = "SELECT * FROM Funcionario";
        public static readonly string INSERT = @"
        INSERT INTO Funcionario (
            nome, CPF, RG, escolaridade, telefone, genero, naturalidade, num_reservista, 
            nome_mae, nome_pai, dt_nascimento, numero_titulo, num_CNH, estado_civil,
            rua, numero, CEP, bairro, cidade, uf, complemento, id_trabalhista
        )
        VALUES (
            @Nome, @Cpf, @Rg, @Escolaridade, @Telefone, @Genero, @Naturalidade, @Num_reservista,
            @nome_mae, @nome_pai, @dt_nascimento, @Numero_Titulo, @Num_cnh, @estado_civil,
            @rua, @numero, @cep, @bairro, @cidade, @uf, @complemento, @Id_trabalhista
        ); SELECT SCOPE_IDENTITY();
        ";
        public static readonly string SELECT_BY_NAME = "SELECT * FROM Funcionario WHERE nome LIKE @Nome";
        public static readonly string SELECT_BY_ID = "SELECT * FROM Funcionario WHERE ID = @Id";
        public static readonly string UPDATE = @"
    UPDATE Funcionario 
    SET nome = @Nome, 
        Cpf = @Cpf, 
        RG = @Rg, 
        escolaridade = @Escolaridade, 
        telefone = @Telefone, 
        genero = @Genero, 
        naturalidade = @Naturalidade, 
        num_reservista = @Num_reservista, 
        nome_mae = @nome_mae, 
        nome_pai = @nome_pai, 
        dt_nascimento = @dt_nascimento, 
        numero_titulo = @Numero_Titulo, 
        num_CNH = @Num_cnh, 
        estado_civil = @estado_civil, 
        rua = @rua, 
        numero = @numero, 
        CEP = @cep, 
        bairro = @bairro, 
        cidade = @cidade, 
        uf = @uf, 
        complemento = @complemento,
        id_trabalhista = @Id_trabalhista
    WHERE ID = @Id";
        public static readonly string DELETE = "DELETE FROM Funcionario WHERE ID = @Id";

        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Rg { get; set; }
        public string? Escolaridade { get; set; }
        public string? Telefone { get; set; }
        public string? Genero { get; set; }
        public string? Naturalidade { get; set; }
        public string? Num_reservista { get; set; }
        public string? Nome_mae { get; set; }
        public string? Nome_pai { get; set; }
        public DateTime? Dt_nascimento { get; set; }
        public string? Estado_civil { get; set; }
        public TituloEleitor TituloEleitor { get; set; }
        public InformacoesTrabalhistas InformacoesTrabalhistas { get; set; }
        public Cnh Cnh { get; set; }
        public Endereco Endereco { get; set; }

    }
}
