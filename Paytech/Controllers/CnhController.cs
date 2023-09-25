using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paytech.Models;
using Paytech.Services;
using System.Text.RegularExpressions;

namespace Paytech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CnhController : ControllerBase
    {
        [HttpPost("Insert")]
        public ActionResult Insert([FromBody] Cnh cnh)
        {
            if (UtilidadesCnh.ValidarCNH(cnh.Num_cnh) == false)
                return StatusCode(413, "Número de cnh inválido, digite-o corretamente!");

            if (new CnhService().Insert(cnh)) 
                return StatusCode(200, cnh);
            else
                return BadRequest();
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Cnh>> GetAll()
        {
            return new CnhService().GetAll();
        }

        [HttpGet("GetByNumCnh")]
        public ActionResult<Cnh> GetByNumCnh(string num_cnh)
        {
            if (!UtilidadesCnh.ValidarCNH(num_cnh))
                return StatusCode(413, "Este número de CNH não é válido, digite-o corretamente!");

            var cnh = new CnhService().GetByNumCnh(num_cnh);
            if (UtilidadesCnh.ValidarCNH(num_cnh) == false) return BadRequest("Este número de CNH não é válido, digite-o corretamente!");
            if (cnh == null) return NotFound("Titulo informado não consta nos registros...");
            return StatusCode(200, cnh);
        }


        [HttpPut("Update")]
        public ActionResult<Cnh> AlterarDados(string num_cnh, string categoria, DateTime? dt_emissao, DateTime? dt_vencimento)
        {
            if (!UtilidadesCnh.ValidarCNH(num_cnh))
                return StatusCode(413, "Este número de CNH não é válido, digite-o corretamente!");

            var cnh = new CnhService().GetByNumCnh(num_cnh);
            if (cnh == null) return NotFound("CNH não encontrada");
            new CnhService().AlterarCnh(num_cnh, categoria, dt_emissao, dt_vencimento);
            var cnhAtualizado = new CnhService().GetByNumCnh(num_cnh);
            return StatusCode(201, cnhAtualizado);
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(string num_cnh)
        {
            if (!UtilidadesCnh.ValidarCNH(num_cnh))
                return StatusCode(413, "Este número de CNH não é válido, digite-o corretamente!");

            var cnh = new CnhService().GetByNumCnh(num_cnh);
            if (cnh == null)
            {
                return NotFound("CNH não encontrada!");
            }
            new CnhService().Delete(num_cnh);
            return StatusCode(200, cnh);
        }


    }
    public static class UtilidadesCnh
    {
        public static bool ValidarCNH(string numeroCNH)
        {
            return !string.IsNullOrEmpty(numeroCNH) &&
                       numeroCNH.Length == 11 &&
                       numeroCNH.All(char.IsDigit) &&
                       !numeroCNH.Any(char.IsLetter);
        }
    }

}
