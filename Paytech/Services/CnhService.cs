using Paytech.Models;
using Paytech.Repositories;

namespace Paytech.Services
{
    public class CnhService
    {
        private readonly ICnhRepository _cnhRepository;

        public CnhService()
        {
            _cnhRepository = new CnhRepository();
        }

        public async Task<Cnh> Insert(Cnh cnh)
        {
            return await _cnhRepository.Insert(cnh);
        }

        public List<Cnh> GetAll()
        {
            return _cnhRepository.GetAll();
        }
        public Cnh GetByNumCnh(string num_cnh)
        {
            return _cnhRepository.GetByNumCnh(num_cnh);
        }

        public void AlterarCnh(Cnh cnh)
        {
            _cnhRepository.AlterarCnh(cnh);
        }
        public void Delete(string num_cnh)
        {
            _cnhRepository.Delete(num_cnh);
        }

    }
}
