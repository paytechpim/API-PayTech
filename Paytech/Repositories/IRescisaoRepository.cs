using Paytech.Models;
using Paytech.Utils;

namespace Paytech.Repositories
{
    public interface IRescisaoRepository
    {
        Task<Retorno> Insert(Rescisao rescisao);
        Task<Retorno> Update(Rescisao rescisao);
        Task<Retorno> Delete(int id);
        Task<Retorno> GetById(int id);
        Task<Retorno> GetByIdFuncionario(int idFuncionario);
        Task<Retorno> GetAll();
    }
}
