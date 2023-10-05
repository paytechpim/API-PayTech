namespace Paytech.Models
{
    public class CarteiraTrabalho
    {
        public static readonly string INSERT = "INSERT INTO CarteiraTrabalho (NumCtps, UFCarteira, Orgao, Serie, Cbo) VALUES (@NumCtps, @UFCarteira, @Orgao, @Serie, @Cbo)";
        public static readonly string SELECT_ALL = "SELECT NumCtps, UFCarteira, Orgao, Serie, Cbo FROM CarteiraTrabalho";
        public static readonly string SELECT_BY_ID = "SELECT * FROM CarteiraTrabalho WHERE NumCtps = @NumCtps AND UFCarteira = @UFCarteira ";
        public static readonly string UPDATE = "UPDATE CarteiraTrabalho SET Orgao = @Orgao, Serie = @Serie, Cbo = @Cbo WHERE NumCtps = @NumCtps AND UFCarteira = @UFCarteira";
        public static readonly string DELETE = "DELETE FROM CarteiraTrabalho WHERE NumCtps = @NumCtps AND UFCarteira = @UFCarteira";
        public string? NumCtps { get; set; }
        public string? UFCarteira { get; set; }
        public string? Orgao { get; set; }
        public string? Serie { get; set; }
        public string? Cbo { get; set; }
    }
}
