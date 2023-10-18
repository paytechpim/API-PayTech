using Microsoft.AspNetCore.Mvc;
using Paytech.Models;
using Paytech.Services;
using Paytech.Utils;

namespace Paytech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {

        [HttpPost("Insert")]
        public async Task<Retorno> Insert(Funcionario funcionario)
        {
            return await new FuncionarioService().Insert(funcionario);
        }


        [HttpGet("GetAll")]
        public List<Funcionario> Get()
        {
            return new FuncionarioService().GetAll();
        }

        [HttpGet("GetByName")]

        public async Task<Retorno> GetByName(string nome)
        {
            return await new FuncionarioService().GetByName(nome);
        }

        [HttpGet("GetById")]

        public async Task<Retorno> GetById(int id)
        {
            return await new FuncionarioService().GetById(id);
        }

        [HttpPut("Update")]
        public async Task<Retorno> AlterarFuncionario(Funcionario funcionario)
        {
            return await new FuncionarioService().AlterarFuncionario(funcionario);
        }


        [HttpDelete("Delete")]
        public async Task<Retorno> Delete(int id)
        {
            return await new FuncionarioService().Delete(id);
        }

    }
}
