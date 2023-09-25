using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paytech.Models;
using Paytech.Services;

namespace Paytech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficioController : ControllerBase
    {
        //private readonly BeneficioService _beneficioService;

        //public BeneficioController(BeneficioService beneficioService)
        //{
        //    _beneficioService = beneficioService;
        //}

        [HttpPost("Insert")]
        public ActionResult Insert([FromBody] Beneficio Beneficio)
        {
            if (new BeneficioService().Insert(Beneficio))
                return StatusCode(200, Beneficio);
            else
                return BadRequest();
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Beneficio>> GetAll()
        {
            return new BeneficioService().GetAll();
        }

        [HttpGet("GetById")]
        public ActionResult<Beneficio> GetById(int id_beneficio)
        {
            var beneficio = new BeneficioService().GetById(id_beneficio);
            if (beneficio == null) return NotFound("Benefício informado não consta nos registros...");
            return StatusCode(200, beneficio);
        }


        [HttpPut("Update")]
        public ActionResult<Beneficio> AlterarBeneficio(int id_beneficio, int salario_familia, float plr, float vale_alimentacao, float vale_transporte, float vale_refeicao)
        {

            var beneficio = new BeneficioService().GetById(id_beneficio);
            if (beneficio == null) return NotFound("Benefício não encontrado!");
            new BeneficioService().AlterarBeneficio(id_beneficio, salario_familia, plr, vale_alimentacao, vale_transporte, vale_refeicao);
            var beneficioAtualizado = new BeneficioService().GetById(id_beneficio);
            return StatusCode(201, beneficioAtualizado);
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(int id_beneficio)
        {

            var titulo = new BeneficioService().GetById(id_beneficio);
            if (titulo == null)
            {
                return NotFound("Benefício não encontrado!");
            }
            new BeneficioService().Delete(id_beneficio);
            return StatusCode(200, titulo);
        }

    }
}
