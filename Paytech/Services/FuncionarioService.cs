using Paytech.Models;
using Paytech.Repositories;

namespace Paytech.Services
{
    public class FuncionarioService
    {
        private IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService()
        {
            _funcionarioRepository = new FuncionarioRepository();
        }

        public bool Insert(Funcionario funcionario)
        {
            return _funcionarioRepository.Insert(funcionario);
        }

        public void Delete(string username)
        {
            _funcionarioRepository.Delete(username);
        }

        public List<Funcionario> GetAll()
        {
            return _funcionarioRepository.GetAll();
        }

        public List<Funcionario> GetByName(string name)
        {
            return _funcionarioRepository.GetByName(name);
        }

        public Funcionario GetById(string id)
        {
            return _funcionarioRepository.GetById(id);
        }

    }
}
