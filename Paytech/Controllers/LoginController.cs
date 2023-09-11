using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paytech.Models;
using Paytech.Services;
using System;

namespace Paytech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] Login model)
        {
            var login = new LoginService().AuthenticateReturnLogin(model.Username, model.Senha);

            if (login == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(login);
            login.Senha = "";
            return new
            {
                user = login,
                token = token,
            };
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet(Name = "Get")]

        public List<Login> Get()
        {
            return new LoginService().GetAll();
        }

        [HttpPost(Name = "Insert")]
        public ActionResult Insert(Login login)
        {
            if (new LoginService().Insert(login))
                return StatusCode(200);
            else
                return BadRequest();
        }

        [HttpDelete(Name = "Delete")]
        public ActionResult Delete(string username)
        {
            new LoginService().Delete(username);
            return NoContent();
        }
    }
}
