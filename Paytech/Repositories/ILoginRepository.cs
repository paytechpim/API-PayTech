using Paytech.Models;
using Paytech.Utils;

namespace Paytech.Repositories
{
    public interface ILoginRepository
    {
        Task<Retorno> Insert(Login login);

        Login GetByUsername(string username);

        List<Login> GetAll();

        void Delete(string username);

        Task<Retorno> AlterarLogin(Login login);

        Task<Retorno> GetByFuncionario(int Id_Funcionario);

        Task<Retorno> IsUserNameExist(string Nome_Usuario);

        Task<Retorno> GetById(int Id);
    }
}
