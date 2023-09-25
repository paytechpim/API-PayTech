using Paytech.Models;

namespace Paytech.Repositories
{
    public interface ICarteiraTrabalhoRepository
    {
        bool Insert(CarteiraTrabalho carteiraTrabalho);

        CarteiraTrabalho GetById(string numCtps, string UF);

        List<CarteiraTrabalho> GetAll();

        void AlterarCarteira(string numCtps, string UF, string orgao, string serie, string cbo);

        void Delete(string numCtps, string UF);
    }
}
