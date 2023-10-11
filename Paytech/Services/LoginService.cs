using Paytech.Models;
using Paytech.Repositories;
using Paytech.Utils;

namespace Paytech.Services
{
    public class LoginService
    {
        private ILoginRepository _loginRepository;

        public LoginService()
        {
            _loginRepository = new LoginRepository();
        }

        public Task<Retorno> Insert(Login login)
        {
            return _loginRepository.Insert(login);
        }

        public void Delete(string username)
        {
            _loginRepository.Delete(username);
        }

        public List<Login> GetAll()
        {
            return _loginRepository.GetAll();
        }

        public Login GetByUsername(string username)
        {
            return _loginRepository.GetByUsername(username);
        }

        public Task<Retorno> GetById(int Id)
        {
            return _loginRepository.GetById(Id);
        }

        public Task<Retorno> AlterarLogin(Login login)
        {
            return _loginRepository.AlterarLogin(login);
        }

        public Task<Retorno> GetByFuncionario(int Id_Funcionario)
        {
            return _loginRepository.GetByFuncionario(Id_Funcionario);
        }

        public Task<Retorno> IsUserNameExist(string Nome_Usuario)
        {
            return _loginRepository.IsUserNameExist(Nome_Usuario);
        }

        public async Task<bool> AuthenticateAsync(string username, string senha)
        {
            Login login = _loginRepository.GetByUsername(username);

            if (login != null && IsValidPassword(login, senha) && login?.Ativo == true)
            {
                return true;
            }
            return false;
        }

        public Login? AuthenticateReturnLogin(string username, string senha)
        {
            Login login = _loginRepository.GetByUsername(username);

            if (login != null && IsValidPassword(login, senha) && login?.Ativo == true)
            {
                return login;
            }
            return null;
        }

        private bool IsValidPassword(Login? login, string? password)
        {
            return login.Senha == password;
        }
    }
}
