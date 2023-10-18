using Microsoft.AspNetCore.Mvc;
using Paytech.Models;
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
            if (UtilidadesTituloEleitor.ValidacaoTituloEleitor(tituloEleitor.Numero_Titulo) == false)
                return StatusCode(413, "Título inválido, digite-o corretamente!");

            if (UtilidadesTituloEleitor.ValidacaoSecaoEZona(tituloEleitor.Secao, tituloEleitor.Zona) == false)
                return StatusCode(413, "Seção e/ou zona inválidas, digite corretamente!");

            if (new TituloEleitorService().Insert(tituloEleitor) != null)
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
        public ActionResult<TituloEleitor> GetByTitulo(string numeroTitulo)
        {
            if (!UtilidadesTituloEleitor.ValidacaoTituloEleitor(numeroTitulo))
                return StatusCode(413, "Este título não é válido, digite-o corretamente!");

            var titulo = new TituloEleitorService().GetByTitulo(numeroTitulo);
            if (UtilidadesTituloEleitor.ValidacaoTituloEleitor(numeroTitulo) == false) return BadRequest("Este título não é válido, digite-o corretamente!");
            if (titulo == null) return NotFound("Titulo informado não consta nos registros...");
            return StatusCode(200, titulo);
        }


        [HttpPut("Update")]
        public ActionResult<TituloEleitor> AlterarDados(TituloEleitor tituloEleitor)
        {
            if (!UtilidadesTituloEleitor.ValidacaoTituloEleitor(tituloEleitor.Numero_Titulo))
                return StatusCode(413, "Este título não é válido, digite-o corretamente!");

            if (UtilidadesTituloEleitor.ValidacaoSecaoEZona(tituloEleitor.Secao, tituloEleitor.Zona) == false)
                return StatusCode(413, "Seção e/ou zona inválidas, digite corretamente!");

            var tituloAdquirido = new TituloEleitorService().GetByTitulo(tituloEleitor.Numero_Titulo);
            if (tituloAdquirido == null) return NotFound("Título de eleitor não encontrado");
            new TituloEleitorService().AlterarTitulo(tituloEleitor);
            var tituloAtualizado = new TituloEleitorService().GetByTitulo(tituloEleitor.Numero_Titulo);
            return StatusCode(201, tituloAtualizado);
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(string numeroTitulo)
        {
            if (!UtilidadesTituloEleitor.ValidacaoTituloEleitor(numeroTitulo))
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
    public static class UtilidadesTituloEleitor
    {
        public static bool ValidacaoTituloEleitor(string numeroTitulo)
        {
            return !string.IsNullOrEmpty(numeroTitulo) &&
                       numeroTitulo.Length == 12 &&
                       numeroTitulo.All(char.IsDigit) &&
                       !numeroTitulo.Any(char.IsLetter);
        }

        public static bool ValidacaoSecaoEZona(string secao, string zona)
        {
            return secao.Length <= 5 && zona.Length <= 5 &&
                   secao.All(char.IsDigit) && zona.All(char.IsDigit);
        }
    }
}
