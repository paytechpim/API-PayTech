using Paytech.Models;

namespace Paytech.Repositories
{
    public interface ICarteiraTrabalhoRepository
    {
        Task<CarteiraTrabalho> Insert(CarteiraTrabalho carteiraTrabalho);

        CarteiraTrabalho GetById(string numCtps, string UF);

        List<CarteiraTrabalho> GetAll();

        void AlterarCarteira(CarteiraTrabalho carteiraTrabalho);

        void Delete(string numCtps, string UF);
    }
}
