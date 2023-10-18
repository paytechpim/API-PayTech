using Paytech.Models;

namespace Paytech.Repositories
{
    public interface ITituloEleitorRepository
    {
        Task<TituloEleitor> Insert(TituloEleitor tituloEleitor);

        TituloEleitor GetByTitulo(string numeroTitulo);

        List<TituloEleitor> GetAll();

        void AlterarTitulo(TituloEleitor tituloEleitor);

        void Delete(string username);

    }
}
