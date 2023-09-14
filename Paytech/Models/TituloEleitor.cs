namespace Paytech.Models
{
    public class TituloEleitor
    {
        public static readonly string INSERT = "INSERT INTO TituloEleitor (numero_titulo, secao, zona) VALUES (@NumeroTitulo, @Secao, @Zona)";
        public static readonly string SELECT_ALL = "SELECT numero_titulo, secao, zona FROM TituloEleitor";
        public static readonly string SELECT_BY_ID = "SELECT * FROM TituloEleitor WHERE numero_titulo = @NumeroTitulo";
        public static readonly string UPDATE = "UPDATE TituloEleitor SET secao = @Secao, Zona = @Zona WHERE numero_titulo = @NumeroTitulo";
        public static readonly string DELETE = "DELETE FROM TituloEleitor WHERE numero_titulo = @NumeroTitulo";
        public string NumeroTitulo { get; set; }
        public string Secao { get; set;}
        public string Zona { get; set;}

    }
}
