using Paytech.Models;
using Paytech.Repositories;

namespace Paytech.Services
{
    public class CarteiraTrabalhoService
    {
        private readonly ICarteiraTrabalhoRepository _carteiraTrabalhoRepository;

        public CarteiraTrabalhoService()
        {
            _carteiraTrabalhoRepository = new CarteiraTrabalhoRepository();
        }

        public async Task<CarteiraTrabalho> Insert(CarteiraTrabalho carteiraTrabalho)
        {
            return await _carteiraTrabalhoRepository.Insert(carteiraTrabalho);
        }

        public List<CarteiraTrabalho> GetAll()
        {
            return _carteiraTrabalhoRepository.GetAll();
        }
        public CarteiraTrabalho GetById(string numCtps, string UF)
        {
            return _carteiraTrabalhoRepository.GetById(numCtps, UF);
        }

        public void AlterarCarteira(CarteiraTrabalho carteiraTrabalho)
        {
            _carteiraTrabalhoRepository.AlterarCarteira(carteiraTrabalho);
        }
        public void Delete(string numCtps, string UF)
        {
            _carteiraTrabalhoRepository.Delete(numCtps, UF);
        }

    }
}
