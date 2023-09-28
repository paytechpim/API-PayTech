using Paytech.Models;

namespace Paytech.Repositories
{
    public interface ICnhRepository
    {
        Task<Cnh> Insert(Cnh cnh);

        Cnh GetByNumCnh(string num_cnh);

        List<Cnh> GetAll();

        void AlterarCnh(Cnh cnh);

        void Delete(string num_cnh);

    }
}
