using Paytech.Models;
using Paytech.Repositories;
using Paytech.Utils;

namespace Paytech.Services
{
    public class InformacoesTrabalhistasService
    {
        private IInformacoesTrabalhistasRepository _informacoesTrabalhistasRepository;

        public InformacoesTrabalhistasService()
        {
            _informacoesTrabalhistasRepository = new InformacoesTrabalhistasRepository();
        }

        public Task<Retorno> Insert(InformacoesTrabalhistas info)
        {
            return _informacoesTrabalhistasRepository.Insert(info);
        }
        public Task<Retorno> GetById(int id)
        {
            return _informacoesTrabalhistasRepository.GetById(id);
        }
        public Task<Retorno> GetAll()
        {
            return _informacoesTrabalhistasRepository.GetAll();
        }
        public Task<Retorno> Update(InformacoesTrabalhistas info)
        {
            return _informacoesTrabalhistasRepository.Update(info);
        }
        public Task<Retorno> Delete(int id)
        {
            return _informacoesTrabalhistasRepository.Delete(id);
        }
    }
}
