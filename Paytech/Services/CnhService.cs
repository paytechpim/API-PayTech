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

        public void AlterarCnh(string num_cnh, string cateforia, DateTime? dt_emissao, DateTime? dt_vencimento)
        {
            _cnhRepository.AlterarCnh(num_cnh, cateforia, dt_emissao, dt_vencimento);
        }
        public void Delete(string username)
        {
            _cnhRepository.Delete(username);
        }

    }
}
