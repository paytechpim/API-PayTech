using Paytech.Models;

namespace Paytech.Repositories
{
    public interface ILoginRepository
    {
        bool Insert(Login login);

        Login GetByUsername(string username);

        List<Login> GetAll();

        void Delete(string username);

    }
}
