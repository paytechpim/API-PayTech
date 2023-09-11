using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paytech.Models;
using Paytech.Services;

namespace Paytech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        [HttpGet(Name = "Get")]

        public List<Funcionario> Get()
        {
            return new FuncionarioService().GetAll();
        }

        [HttpGet(Name = "GetByName")]

        public List<Funcionario> GetByName(string nome)
        {
            return new FuncionarioService().GetByName(nome);
        }

        [HttpGet(Name = "GetById")]

        public Funcionario GetById(int id)
        {
            return new FuncionarioService().GetById(id);
        }


        [HttpPost(Name = "Insert")]
        public ActionResult Insert(Funcionario funcionario)
        {
            if (new FuncionarioService().Insert(funcionario))
                return StatusCode(200);
            else
                return BadRequest();
        }

        [HttpDelete(Name = "Delete")]
        public ActionResult Delete(int id)
        {
            new FuncionarioService().Delete(id);
            return NoContent();
        }

    }
}
