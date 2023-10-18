using Paytech.Models;
using Paytech.Utils;

namespace Paytech.Repositories
{
    public interface IInformacoesTrabalhistasRepository
    {
        Task<Retorno> Insert(InformacoesTrabalhistas info);
        Task<Retorno> GetById(int id);
        Task<Retorno> GetAll();
        Task<Retorno> Update(InformacoesTrabalhistas info);
        Task<Retorno> Delete(int id);
    }
}
