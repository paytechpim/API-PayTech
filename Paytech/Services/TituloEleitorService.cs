using Paytech.Models;
using Paytech.Repositories;

namespace Paytech.Services
{
    public class TituloEleitorService
    {
        private readonly ITituloEleitorRepository _tituloEleitorRepository;

        public TituloEleitorService()
        {
            _tituloEleitorRepository = new TituloEleitorRepository();
        }

        public bool Insert(TituloEleitor tituloEleitor)
        {
            return _tituloEleitorRepository.Insert(tituloEleitor);
        }

        public List<TituloEleitor> GetAll()
        {
            return _tituloEleitorRepository.GetAll();
        }
        public TituloEleitor GetByTitulo(string numeroTitulo)
        {
            return _tituloEleitorRepository.GetByTitulo(numeroTitulo);
        }

        public void AlterarDados(string numeroTitulo, string secao, string zona)
        {
            _tituloEleitorRepository.AlterarTitulo(numeroTitulo, secao, zona);
        }
        public void Delete(string username)
        {
            _tituloEleitorRepository.Delete(username);
        }
    }
}
