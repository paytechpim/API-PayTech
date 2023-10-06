using Paytech.Models;
using Paytech.Repositories;
using Paytech.Utils;

namespace Paytech.Services
{
    public class FuncionarioService
    {
        //private IFuncionarioRepository _funcionarioRepository;

        //public FuncionarioService(IFuncionarioRepository funcionarioRepository)
        //{
        //    _funcionarioRepository = funcionarioRepository;
        //}

        private IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService()
        {
            _funcionarioRepository = new FuncionarioRepository();
        }

        public Task<Retorno> Insert(Funcionario funcionario)
        {
            return _funcionarioRepository.Insert(funcionario);
        }

        public void Delete(int id)
        {
            _funcionarioRepository.Delete(id);
        }

        public List<Funcionario> GetAll()
        {
            return _funcionarioRepository.GetAll();
        }

        public List<Funcionario> GetByName(string name)
        {
            return _funcionarioRepository.GetByName(name);
        }

        public Task<Retorno> AlterarFuncionario(Funcionario funcionario)
        {
            return _funcionarioRepository.AlterarFuncionario(funcionario);
        }

        public Funcionario GetById(int id)
        {
            return _funcionarioRepository.GetById(id);
        }

    }
}
