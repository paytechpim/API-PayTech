using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paytech.Models;
using Paytech.Services;
using Paytech.Utils;

namespace Paytech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoleriteController : ControllerBase
    {
        [HttpPost("Insert")]
        public async Task<Retorno> Insert(Holerite holerite)
        {
            return await new HoleriteService().Insert(holerite);
        }

        [HttpPut("Update")]
        public async Task<Retorno> Update(Holerite holerite)
        {
            return await new HoleriteService().Update(holerite);
        }

        [HttpDelete("Delete")]
        public async Task<Retorno> Delete(int id)
        {
            return await new HoleriteService().Delete(id);
        }

        [HttpGet("GetAll")]
        public async Task<Retorno> GetAll()
        {
            return await new HoleriteService().GetAll();
        }

        [HttpGet("GetById")]
        public async Task<Retorno> GetById(int id)
        {
            return await new HoleriteService().GetById(id);
        }

        [HttpGet("GetByIdFuncionario")]
        public async Task<Retorno> GetByIdFuncionario(int idFuncionario)
        {
            return await new HoleriteService().GetByIdFuncionario(idFuncionario);
        }

        [HttpPost("GerarHolerite")]
        public async Task<Retorno> GerarHolerite(int idFuncionario, DateTime data_inicio, DateTime data_fim, double percentualHoraExtra, double valorValeTransporte, double faltas, double horaExtra)
        {
            return await new HoleriteService().GerarHolerite(idFuncionario, data_inicio, data_fim, percentualHoraExtra, valorValeTransporte, faltas, horaExtra);
        }

        [HttpPost]
        [Route("GerarExcel")]
        public async Task<Retorno> GerarExcel(int IdHolerite)
        {
            return await new HoleriteService().gerarExcelHolerite(IdHolerite);
        }
    }
}
