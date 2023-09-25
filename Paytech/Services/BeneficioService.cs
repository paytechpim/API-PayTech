using Paytech.Models;
using Paytech.Repositories;

namespace Paytech.Services
{
    public class BeneficioService
    {
        private readonly IBeneficioRepository _beneficioRepository;

        public BeneficioService(IBeneficioRepository beneficioRepository)
        {
            _beneficioRepository = beneficioRepository;
        }

        public bool Insert(Beneficio beneficio)
        {
            return _beneficioRepository.Insert(beneficio);
        }

        public List<Beneficio> GetAll()
        {
            return _beneficioRepository.GetAll();
        }
        public Beneficio GetById(int id_beneficio)
        {
            return _beneficioRepository.GetById(id_beneficio);
        }

        public void AlterarBeneficio(int id_beneficio, int salario_familia, float plr, float vale_alimentacao, float vale_transporte, float vale_refeicao)
        {
            _beneficioRepository.AlterarBeneficio(id_beneficio, salario_familia,plr, vale_alimentacao, vale_transporte, vale_refeicao);
        }
        public void Delete(int id_beneficio)
        {
            _beneficioRepository.Delete(id_beneficio);
        }

    }
}
