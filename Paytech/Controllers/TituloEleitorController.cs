using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paytech.Models;
using Paytech.Repositories;
using Paytech.Services;

namespace Paytech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TituloEleitorController : ControllerBase
    {
        private readonly TituloEleitorRepository _tituloEleitorRepository;

        [HttpPost("Insert")] 
        public ActionResult Insert([FromBody] TituloEleitor tituloEleitor)
        {
            if (new TituloEleitorService().Insert(tituloEleitor))
                return StatusCode(200);
            else
                return BadRequest();
        }

        [HttpGet("GetAll")] 
        public ActionResult<List<TituloEleitor>> GetAll()
        {
            return new TituloEleitorService().GetAll();
        }

        [HttpGet("GetByTitulo")] 
        public ActionResult<TituloEleitor>  GetByTitulo(string numeroTitulo)
        {
            var titulo = new TituloEleitorService().GetByTitulo(numeroTitulo);

            if (_tituloEleitorRepository.ValidacaoTituloEleitor(numeroTitulo) == false) return BadRequest("Este título não é válido, digite-o corretamente!");
            if (titulo == null) return NotFound("Titulo informado não consta nos registros...");
            return StatusCode(200, titulo);
        }


        [HttpPut("Update")]
        public ActionResult<TituloEleitor> AlterarDados(string numeroTitulo, string secao, string zona)
        {
            var titulo = new TituloEleitorService().GetByTitulo(numeroTitulo);
            if (titulo == null) return NotFound("Título de eleitor não encontrado");
            new TituloEleitorService().AlterarDados(numeroTitulo, secao, zona);
            return StatusCode(201, titulo);
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(string numeroTitulo)
        {
            var titulo = new TituloEleitorService().GetByTitulo(numeroTitulo);
            if (titulo == null)
            {
                return NotFound("Título não encontrado!");
            }
            new TituloEleitorService().Delete(numeroTitulo);
            return StatusCode(200, titulo);
        }
    }
}
