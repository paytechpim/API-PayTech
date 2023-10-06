namespace Paytech.Utils
{
    public class Retorno
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }
        public static Exception Excecao { get; set; }

        #region CONSTRUTORES

        public Retorno()
        {

        }

        public Retorno(bool sucesso)
        {
            Sucesso = sucesso;
        }

        public Retorno(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        public Retorno(bool sucesso, object entity, string mensagem = "")
        {
            Sucesso = sucesso;
            Dados = entity;
            Mensagem = mensagem;
        }

        #endregion

        #region TRATAMENTO EXCEÇÃO

        public static Retorno CriarRetornoExcecao(Exception excecao)
        {
            return new Retorno(false, String.Format("Ocorreu o erro {0}, ao {1}.\r\nEntre em contato com o suporte MobSystem.", excecao.TargetSite.Name, excecao.Message));
        }

        #endregion
    }
}
