namespace Paytech.Models
{
    public class Login
    {
        public static readonly string SELECT_ALL = "SELECT id, nome_usuario, senha, tipo, data_cadastro, id_funcionario FROM Login";
        public static readonly string INSERT = "INSERT INTO Login (nome_usuario, senha, tipo, data_cadastro, id_funcionario) VALUES (@Nome_Usuario, @Senha, @Tipo, GETDATE(), @Id_Funcionario)";
        public static readonly string DELETE = "DELETE FROM Login WHERE Id = @Id";
        public static readonly string SELECT_BY_USERNAME = "SELECT id, nome_usuario, senha, tipo, data_cadastro, id_funcionario FROM Login WHERE nome_usuario = @Nome_Usuario";

        public int Id { get; set; }
        public string Nome_Usuario { get; set; }
        public string Senha { get; set; }

        public string Tipo { get; set; }

        public DateTime Data_Cadastro { get; set; }

        public int Id_Funcionario { get; set; }

    }
}
