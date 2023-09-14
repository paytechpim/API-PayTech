using Paytech.Models;
using System.Reflection.Metadata;

namespace Paytech.Repositories
{
    public interface ITituloEleitorRepository
    {
        bool Insert(TituloEleitor tituloEleitor);

        TituloEleitor GetByTitulo(string numeroTitulo);

        List<TituloEleitor> GetAll();

        void AlterarTitulo(string numeroTitulo, string secao, string zona);

        void Delete(string username);

    }
}
