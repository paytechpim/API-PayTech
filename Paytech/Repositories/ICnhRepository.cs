using Paytech.Models;

namespace Paytech.Repositories
{
    public interface ICnhRepository
    {
        bool Insert(Cnh cnh);

        Cnh GetByNumCnh(string numeroTitulo);

        List<Cnh> GetAll();

        void AlterarCnh(string num_cnh, string categoria, DateTime? dt_emissao, DateTime? dt_vencimento);

        void Delete(string username);

    }
}
