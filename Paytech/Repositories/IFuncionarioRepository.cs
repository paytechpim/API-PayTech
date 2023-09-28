using Paytech.Models;

namespace Paytech.Repositories
{
    public interface IFuncionarioRepository
    {
            Task<Funcionario> Insert(Funcionario funcionario);

            Funcionario GetById(int id);

            List<Funcionario> GetByName(string name);

            List<Funcionario> GetAll();
            void AlterarFuncionario(Funcionario funcionario);

            void Delete(int id);
        }
    }


