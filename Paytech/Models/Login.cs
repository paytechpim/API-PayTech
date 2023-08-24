namespace Paytech.Models
{
    public class Login
    {
        public static readonly string SELECT_ALL = "SELECT username, senha FROM Login";
        public static readonly string INSERT = "INSERT INTO Login (username, senha) VALUES (@Username, @Senha)";
        public static readonly string DELETE = "DELETE FROM Login WHERE User = @User";
        public static readonly string SELECT_BY_USERNAME = "SELECT username, senha FROM Login WHERE username = @Username";

        public string Username { get; set; }
        public string Senha { get; set; }
    }
}
