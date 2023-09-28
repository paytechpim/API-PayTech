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

        public async Task<TituloEleitor> Insert(TituloEleitor tituloEleitor)
        {
            return await _tituloEleitorRepository.Insert(tituloEleitor);
        }

        public List<TituloEleitor> GetAll()
        {
            return _tituloEleitorRepository.GetAll();
        }
        public TituloEleitor GetByTitulo(string numeroTitulo)
        {
            return _tituloEleitorRepository.GetByTitulo(numeroTitulo);
        }

        public void AlterarTitulo(TituloEleitor tituloEleitor)
        {
            _tituloEleitorRepository.AlterarTitulo(tituloEleitor);
        }
        public void Delete(string numeroTitulo)
        {
            _tituloEleitorRepository.Delete(numeroTitulo);
        }

    }
}
