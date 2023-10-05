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


        [HttpGet("GetAll")]

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

        [HttpPut("Update")]
        public ActionResult<Funcionario> AlterarFuncionario(Funcionario funcionario)
        {

            var funcionarioAdquirido = new FuncionarioService().GetById((int)funcionario.Id);
            if (funcionarioAdquirido == null) return NotFound("Funcionário não encontrado");
            new FuncionarioService().AlterarFuncionario(funcionario);
            var tituloAtualizado = new FuncionarioService().GetById((int)funcionario.Id);
            return StatusCode(201, tituloAtualizado);
        }


        [HttpDelete("Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
            var funcionario = new FuncionarioService().GetById(id);
            if (funcionario == null) return NotFound("Funcionário não encontrado!");
            new FuncionarioService().Delete(id);
            return StatusCode(200, funcionario);
            }
            catch (BadHttpRequestException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
