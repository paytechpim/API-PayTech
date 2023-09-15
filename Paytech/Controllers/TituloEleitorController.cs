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
        [HttpPost("Insert")] 
        public ActionResult Insert([FromBody] TituloEleitor tituloEleitor)
        {
            if (Utilidades.ValidacaoTituloEleitor(tituloEleitor.Numero_Titulo) == false)
                return StatusCode(413, "Este título não é válido, digite-o corretamente!");

            if (new TituloEleitorService().Insert(tituloEleitor))
                return StatusCode(200, tituloEleitor);
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
            if (!Utilidades.ValidacaoTituloEleitor(numeroTitulo)) 
                return StatusCode(413, "Este título não é válido, digite-o corretamente!");

            var titulo = new TituloEleitorService().GetByTitulo(numeroTitulo);
            if (Utilidades.ValidacaoTituloEleitor(numeroTitulo) == false) return BadRequest("Este título não é válido, digite-o corretamente!");
            if (titulo == null) return NotFound("Titulo informado não consta nos registros...");
            return StatusCode(200, titulo);
        }


        [HttpPut("Update")]
        public ActionResult<TituloEleitor> AlterarDados(string numeroTitulo, string secao, string zona)
        {
            if (!Utilidades.ValidacaoTituloEleitor(numeroTitulo)) 
                return StatusCode(413, "Este título não é válido, digite-o corretamente!");

            var titulo = new TituloEleitorService().GetByTitulo(numeroTitulo);
            if (titulo == null) return NotFound("Título de eleitor não encontrado");
            new TituloEleitorService().AlterarDados(numeroTitulo, secao, zona);
            var tituloAtualizado = new TituloEleitorService().GetByTitulo(numeroTitulo);
            return StatusCode(201, tituloAtualizado);
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(string numeroTitulo)
        {
            if (!Utilidades.ValidacaoTituloEleitor(numeroTitulo)) 
                return StatusCode(413, "Este título não é válido, digite-o corretamente!");

            var titulo = new TituloEleitorService().GetByTitulo(numeroTitulo);
            if (titulo == null)
            {
                return NotFound("Título não encontrado!");
            }
            new TituloEleitorService().Delete(numeroTitulo);
            return StatusCode(200, titulo);
        }


    }
    public static class Utilidades
    {
        public static bool ValidacaoTituloEleitor(string numeroTitulo)
        {
            if (string.IsNullOrEmpty(numeroTitulo) || numeroTitulo.Length != 12)
            {
                return false;
            }
            foreach (char c in numeroTitulo)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
