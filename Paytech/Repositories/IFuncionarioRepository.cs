using Paytech.Models;
using Paytech.Utils;

namespace Paytech.Repositories
{
    public interface IFuncionarioRepository
    {
            Task<Retorno> Insert(Funcionario funcionario);

            Task<Retorno> GetById(int id);

            Task<Retorno> GetByName(string name);

            List<Funcionario> GetAll();

            Task<Retorno> AlterarFuncionario(Funcionario funcionario);

            Task<Retorno> Delete(int id);
        }
    }


