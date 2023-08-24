using Paytech.Models;
using Paytech.Repositories;

namespace Paytech.Services
{
    public class LoginService
    {
        private ILoginRepository _loginRepository;

        public LoginService()
        {
            _loginRepository = new LoginRepository();
        }

        public bool Insert(Login login)
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

        public async Task<bool> AuthenticateAsync(string username, string senha)
        {
            Login login = _loginRepository.GetByUsername(username);

            if (login != null && IsValidPassword(login, senha))
            {
                return true;
            }
            return false;
        }

        private bool IsValidPassword(Login login, string password)
        {
            return login.Senha == password;
        }
    }
}
