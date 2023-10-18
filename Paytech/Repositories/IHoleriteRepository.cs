using Paytech.Models;
using Paytech.Utils;

namespace Paytech.Repositories
{
    public interface IHoleriteRepository
    {
        Task<Retorno> Insert(Holerite holerite);
        Task<Retorno> Update(Holerite holerite);
        Task<Retorno> Delete(int id);
        Task<Retorno> GetById(int id);
        Task<Retorno> GetByIdFuncionario(int idFuncionario);
        Task<Retorno> GetAll();
    }
}
