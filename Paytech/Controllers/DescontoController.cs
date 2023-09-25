using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paytech.Models;
using Paytech.Services;

namespace Paytech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DescontoController : ControllerBase
    {
        [HttpPost("Insert")]
        public ActionResult Insert([FromBody] Desconto Desconto)
        {
            if (new DescontoService().Insert(Desconto))
                return StatusCode(200, Desconto);
            else
                return BadRequest();
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Desconto>> GetAll()
        {
            return new DescontoService().GetAll();
        }

        [HttpGet("GetById")]
        public ActionResult<Desconto> GetById(int id_desconto)
        {
            var desconto = new DescontoService().GetById(id_desconto);
            if (desconto == null) return NotFound("Desconto informado não consta nos registros...");
            return StatusCode(200, desconto);
        }


        [HttpPut("Update")]
        public ActionResult<Desconto> AlterarDesconto(int id_desconto, float fgts, float inss, float ir, float decimo_terceiro, float adiantamento_salario)
        {

            var desconto = new DescontoService().GetById(id_desconto);
            if (desconto == null) return NotFound("Desconto não encontrado!");
            new DescontoService().AlterarDesconto(id_desconto,  fgts,  inss,  ir,  decimo_terceiro,  adiantamento_salario);
            var descontoAtualizado = new DescontoService().GetById(id_desconto);
            return StatusCode(201, descontoAtualizado);
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(int id_desconto)
        {

            var titulo = new DescontoService().GetById(id_desconto);
            if (titulo == null)
            {
                return NotFound("Desconto não encontrado!");
            }
            new DescontoService().Delete(id_desconto);
            return StatusCode(200, titulo);
        }

    }
}
