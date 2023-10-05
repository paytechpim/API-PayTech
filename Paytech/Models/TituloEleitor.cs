namespace Paytech.Models
{
    public class TituloEleitor
    {
        public static readonly string INSERT = "INSERT INTO TituloEleitor (numero_titulo, secao, zona) VALUES (@Numero_Titulo, @Secao, @Zona)";
        public static readonly string SELECT_ALL = "SELECT numero_titulo, secao, zona FROM TituloEleitor";
        public static readonly string SELECT_BY_ID = "SELECT * FROM TituloEleitor WHERE numero_titulo = @Numero_Titulo";
        public static readonly string UPDATE = "UPDATE TituloEleitor SET secao = @Secao, Zona = @Zona WHERE numero_titulo = @Numero_Titulo";
        public static readonly string DELETE = "DELETE FROM TituloEleitor WHERE numero_titulo = @Numero_Titulo";
        public string? Numero_Titulo { get; set; }
        public string? Secao { get; set;}
        public string? Zona { get; set;}

    }
}
