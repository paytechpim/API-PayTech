using Paytech.Models;
using Paytech.Repositories;

namespace Paytech.Services
{
    public class DescontoService
    {
        private readonly IDescontoRepository _descontoRepository;

        public DescontoService()
        {
            _descontoRepository = new DescontoRepository();
        }

        public bool Insert(Desconto desconto)
        {
            return _descontoRepository.Insert(desconto);
        }

        public List<Desconto> GetAll()
        {
            return _descontoRepository.GetAll();
        }
        public Desconto GetById(int id_desconto)
        {
            return _descontoRepository.GetById(id_desconto);
        }

        public void AlterarDesconto(int id_desconto, float fgts, float inss, float ir, float decimo_terceiro, float adiantamento_salario)
        {
            _descontoRepository.AlterarDesconto(id_desconto, fgts, inss, ir, decimo_terceiro, adiantamento_salario);
        }
        public void Delete(int id_desconto)
        {
            _descontoRepository.Delete(id_desconto);
        }

    }
}
