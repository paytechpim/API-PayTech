using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paytech.Models;
using Paytech.Services;
using System.Net;

namespace Paytech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {

        [HttpPost("Insert")]
        public ActionResult Insert(Funcionario funcionario)
        {
            if (new FuncionarioService().Insert(funcionario) != null)
                return StatusCode(200, funcionario);
            else
                return BadRequest();
        }


        [HttpGet("Get")]

        public List<Funcionario> Get()
        {
            return new FuncionarioService().GetAll();
        }

        [HttpGet("GetByName")]

        public List<Funcionario> GetByName(string nome)
        {
            return new FuncionarioService().GetByName(nome);
        }

        [HttpGet("GetById")]

        public Funcionario GetById(int id)
        {
            return new FuncionarioService().GetById(id);
        }


        [HttpDelete("Delete")]
        public ActionResult Delete(int id)
        {
            new FuncionarioService().Delete(id);
            return NoContent();
        }

    }
}
