using Paytech.Models;
using Paytech.Repositories;
using Paytech.Utils;

namespace Paytech.Services
{
    public class RescisaoService
    {

        private IRescisaoRepository _rescisaoRepository;

        public RescisaoService()
        {
            _rescisaoRepository = new RescisaoRepository();
        }

        public Task<Retorno> Insert(Rescisao rescisao)
        {
            return _rescisaoRepository.Insert(rescisao);
        }
        public Task<Retorno> Update(Rescisao rescisao)
        {
            return _rescisaoRepository.Update(rescisao);
        }
        public Task<Retorno> Delete(int id)
        {
            return _rescisaoRepository.Delete(id);
        }
        public Task<Retorno> GetById(int id)
        {
            return _rescisaoRepository.GetById(id);
        }
        public Task<Retorno> GetByIdFuncionario(int idFuncionario)
        {
            return _rescisaoRepository.GetByIdFuncionario(idFuncionario);
        }
        public Task<Retorno> GetAll()
        {
            return _rescisaoRepository.GetAll();
        }
    }
}
