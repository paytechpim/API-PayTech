using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paytech.Models;
using Paytech.Services;
using Paytech.Utils;

namespace Paytech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformacoesTrabalhistasController : ControllerBase
    {
        [HttpPost("Insert")]
        public async Task<Retorno> Insert(InformacoesTrabalhistas info)
        {
            return await new InformacoesTrabalhistasService().Insert(info);
        }


        [HttpGet("GetAll")]
        public async Task<Retorno> GetAll()
        {
            return await new InformacoesTrabalhistasService().GetAll();
        }

        [HttpGet("GetById")]

        public async Task<Retorno> GetById(int id)
        {
            return await new InformacoesTrabalhistasService().GetById(id);
        }

        [HttpPut("Update")]
        public async Task<Retorno> Update(InformacoesTrabalhistas info)
        {
            return await new InformacoesTrabalhistasService().Update(info);
        }


        [HttpDelete("Delete")]
        public async Task<Retorno> Delete(int id)
        {
            return await new InformacoesTrabalhistasService().Delete(id);
        }
    }
}
