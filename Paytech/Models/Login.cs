namespace Paytech.Models
{
    public class Login
    {
        public static readonly string SELECT_ALL = "SELECT id, username, senha, tipo FROM Login";
        public static readonly string INSERT = "INSERT INTO Login (username, senha, tipo) VALUES (@Username, @Senha, @Tipo)";
        public static readonly string DELETE = "DELETE FROM Login WHERE Id = @Id";
        public static readonly string SELECT_BY_USERNAME = "SELECT id, username, senha, tipo FROM Login WHERE username = @Username";

        public int Id { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }

        public string Tipo { get; set; }
    }
}
