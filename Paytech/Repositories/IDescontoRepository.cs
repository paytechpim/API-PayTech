using Paytech.Models;

namespace Paytech.Repositories
{
    public interface IDescontoRepository
    {
        bool Insert(Desconto desconto);

        Desconto GetById(int id_desconto);

        List<Desconto> GetAll();

        void AlterarDesconto(int id_desconto, float fgts, float inss, float ir, float decimo_terceiro, float adiantamento_salario);

        void Delete(int id_desconto);


    }
}
