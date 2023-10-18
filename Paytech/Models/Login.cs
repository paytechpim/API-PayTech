namespace Paytech.Models
{
    public class Login
    {
        public static readonly string SELECT_ALL = "SELECT id, nome_usuario, tipo, data_cadastro, id_funcionario, ativo FROM Login";
        public static readonly string INSERT = @"INSERT INTO Login (nome_usuario, senha, tipo, data_cadastro, id_funcionario, ativo) 
        VALUES 
        (@Nome_Usuario, @Senha, @Tipo, GETDATE(), @Id_Funcionario, @Ativo); 
        SELECT SCOPE_IDENTITY();";
        public static readonly string DELETE = "DELETE FROM Login WHERE Id = @Id";
        public static readonly string UPDATE = "UPDATE Login SET nome_usuario = @Nome_Usuario, tipo = @Tipo, ativo = @Ativo WHERE id_funcionario = @Id_Funcionario";
        public static readonly string UPDATE_SENHA = "UPDATE Login SET nome_usuario = @Nome_Usuario, senha = @Senha, tipo = @Tipo, ativo = @Ativo WHERE id_funcionario = @Id_Funcionario";
        public static readonly string UPDATE_SENHA_PRIMEIRO_ACESSO = "UPDATE Login SET senha = @Senha, data_ultima_alteracao_senha = GETDATE() WHERE id_funcionario = @Id_Funcionario";
        public static readonly string SELECT_BY_USERNAME = "SELECT id, nome_usuario, senha, tipo, data_cadastro, id_funcionario, ativo FROM Login WHERE nome_usuario = @Nome_Usuario";
        public static readonly string SELECT_BY_ID = "SELECT id, nome_usuario, senha, tipo, data_cadastro, id_funcionario, ativo FROM Login WHERE id = @Id";
        public static readonly string SELECT_BY_FUNCIONARIO = "SELECT id, nome_usuario, tipo, data_cadastro, id_funcionario, ativo FROM Login WHERE id_funcionario = @Id_Funcionario";
        public static readonly string SELECT_USERNAME_EXIST = "SELECT TOP 1 id FROM Login WHERE nome_usuario = @Nome_Usuario";
        public static readonly string SELECT_IS_PRIMEIRO_ACESSO = "SELECT ISNULL(data_ultima_alteracao_senha, '') FROM Login WHERE id_funcionario = @Id_Funcionario";

        public int? Id { get; set; }
        public string? Nome_Usuario { get; set; }
        public string? Senha { get; set; }

        public string? Tipo { get; set; }

        public DateTime? Data_Cadastro { get; set; }
        public DateTime? Data_ultima_alteracao_senha { get; set; }

        public int? Id_Funcionario { get; set; }

        public Boolean? Ativo { get; set; }

    }
}
