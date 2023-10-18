using Paytech.Models;
using Paytech.Repositories;
using Paytech.Utils;

namespace Paytech.Services
{
    public class HoleriteService
    {

        private IHoleriteRepository _holeriteRepository;

        public HoleriteService()
        {
            _holeriteRepository = new HoleriteRepository();
        }

        public Task<Retorno> Insert(Holerite holerite)
        {
            return _holeriteRepository.Insert(holerite);
        }
        public Task<Retorno> Update(Holerite holerite)
        {
            return _holeriteRepository.Update(holerite);
        }
        public Task<Retorno> Delete(int id)
        {
            return _holeriteRepository.Delete(id);
        }
        public Task<Retorno> GetById(int id)
        {
            return _holeriteRepository.GetById(id);
        }
        public Task<Retorno> GetByIdFuncionario(int idFuncionario)
        {
            return _holeriteRepository.GetByIdFuncionario(idFuncionario);
        }
        public Task<Retorno> GetAll() 
        {
            return _holeriteRepository.GetAll();
        }
    }
}
