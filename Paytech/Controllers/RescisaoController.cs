using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paytech.Models;
using Paytech.Services;
using Paytech.Utils;

namespace Paytech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RescisaoController : ControllerBase
    {
        [HttpPost("Insert")]
        public async Task<Retorno> Insert(Rescisao rescisao)
        {
            return await new RescisaoService().Insert(rescisao);
        }

        [HttpPut("Update")]
        public async Task<Retorno> Update(Rescisao rescisao)
        {
            return await new RescisaoService().Update(rescisao);
        }

        [HttpDelete("Delete")]
        public async Task<Retorno> Delete(int id)
        {
            return await new RescisaoService().Delete(id);
        }

        [HttpGet("GetAll")]
        public async Task<Retorno> GetAll()
        {
            return await new RescisaoService().GetAll();
        }

        [HttpGet("GetById")]
        public async Task<Retorno> GetById(int id)
        {
            return await new RescisaoService().GetById(id);
        }

        [HttpGet("GetByIdFuncionario")]
        public async Task<Retorno> GetByIdFuncionario(int idFuncionario)
        {
            return await new RescisaoService().GetByIdFuncionario(idFuncionario);
        }
    }
}
