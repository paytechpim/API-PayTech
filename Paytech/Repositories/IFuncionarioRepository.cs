using Paytech.Models;
using Paytech.Utils;

namespace Paytech.Repositories
{
    public interface IFuncionarioRepository
    {
            Task<Retorno> Insert(Funcionario funcionario);

            Funcionario GetById(int id);

            List<Funcionario> GetByName(string name);

            List<Funcionario> GetAll();
            void AlterarFuncionario(Funcionario funcionario);

            void Delete(int id);
        }
    }


