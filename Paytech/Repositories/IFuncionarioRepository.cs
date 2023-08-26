using Paytech.Models;

namespace Paytech.Repositories
{
    public class IFuncionarioRepository
    {
        bool Insert(Funcionario funcionario);

        Login GetById(string username);

        List<Funcionario> GetByName();

        List<Funcionario> GetAll();

        void Delete(string id);
    }
}
