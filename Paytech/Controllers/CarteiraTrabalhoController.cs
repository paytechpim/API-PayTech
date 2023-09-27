using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paytech.Models;
using Paytech.Services;

namespace Paytech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarteiraTrabalhoController : ControllerBase
    {
        [HttpPost("Insert")]
        public ActionResult Insert([FromBody] CarteiraTrabalho carteiraTrabalho)
        {
            if (UtilidadesCarteiraDeTrabalho.ValidarNumCTPS(carteiraTrabalho.NumCtps) == false)
                return StatusCode(413, "Número CTPS inválido, digite-o corretamente!");

            if (UtilidadesCarteiraDeTrabalho.ValidarUf(carteiraTrabalho.UFCarteira) == false)
                return StatusCode(413, "UF inválido, digite corretamente!");

            if (UtilidadesCarteiraDeTrabalho.ValidarNumeroSerieCTPS(carteiraTrabalho.Serie) == false)
                return StatusCode(413, "Número de série inválido, digite corretamente!");

            if (new CarteiraTrabalhoService().Insert(carteiraTrabalho) != null)
                return StatusCode(200, carteiraTrabalho);
            else
                return BadRequest();
        }

        [HttpGet("GetAll")]
        public ActionResult<List<CarteiraTrabalho>> GetAll()
        {
            return new CarteiraTrabalhoService().GetAll();
        }

        [HttpGet("GetById")]
        public ActionResult<CarteiraTrabalho> GetById(string numCtps, string uf)
        {
            if (UtilidadesCarteiraDeTrabalho.ValidarNumCTPS(numCtps) == false)
                return StatusCode(413, "Número CTPS inválido, digite-o corretamente!");

            if (UtilidadesCarteiraDeTrabalho.ValidarUf(uf) == false)
                return StatusCode(413, "UF inválido, digite corretamente!");

            var carteiraTrabalho = new CarteiraTrabalhoService().GetById(numCtps, uf);
            if (carteiraTrabalho == null) return NotFound("Carteira de Trabalho informada não consta nos registros...");
            return StatusCode(200, carteiraTrabalho);
        }


        [HttpPut("Update")]
        public ActionResult<CarteiraTrabalho> AlterarCarteira(string numCtps, string uf, string orgao, string serie, string cbo)
        {
            if (UtilidadesCarteiraDeTrabalho.ValidarNumCTPS(numCtps) == false)
                return StatusCode(413, "Número CTPS inválido, digite-o corretamente!");

            if (UtilidadesCarteiraDeTrabalho.ValidarUf(uf) == false)
                return StatusCode(413, "UF inválido, digite corretamente!");

            if (UtilidadesCarteiraDeTrabalho.ValidarNumeroSerieCTPS(serie) == false)
                return StatusCode(413, "Número de série inválido, digite corretamente!");

            var carteiraTrabalho = new CarteiraTrabalhoService().GetById(numCtps, uf);
            if (carteiraTrabalho == null) return NotFound("Título de eleitor não encontrado");
            new CarteiraTrabalhoService().AlterarCarteira(numCtps, uf, orgao, serie, cbo);
            var carteiraAtualizada = new CarteiraTrabalhoService().GetById(numCtps, uf);
            return StatusCode(201, carteiraAtualizada);
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(string numCtps, string uf)
        {
            if (UtilidadesCarteiraDeTrabalho.ValidarNumCTPS(numCtps) == false)
                return StatusCode(413, "Número CTPS inválido, digite-o corretamente!");

            if (UtilidadesCarteiraDeTrabalho.ValidarUf(uf) == false)
                return StatusCode(413, "UF inválido, digite corretamente!");

            var carteiraTrabalho = new CarteiraTrabalhoService().GetById(numCtps, uf);
            if (carteiraTrabalho == null)
            {
                return NotFound("Carteira não encontrada!");
            }
            new CarteiraTrabalhoService().Delete(numCtps, uf);
            return StatusCode(200, carteiraTrabalho);
        }
    }

    public static class UtilidadesCarteiraDeTrabalho
    {
        public static bool ValidarNumCTPS(string numCTPS)
        {
            if (string.IsNullOrEmpty(numCTPS) || numCTPS.Length != 7)
            {
                return false;
            }
            foreach (char c in numCTPS)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ValidarNumeroSerieCTPS(string numeroSerieCTPS)
        {
            if (string.IsNullOrEmpty(numeroSerieCTPS) || numeroSerieCTPS.Length < 1)
            {
                return false;
            }

            foreach (char c in numeroSerieCTPS)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ValidarUf(string uf)
        {
            List<string> ufsValidas = new List<string>
    {
        "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA",
        "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO"
    };
            return ufsValidas.Contains(uf.ToUpper()); 
        }
    }
}

