using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] Login user)
        {
            var login = new LoginService().AuthenticateReturnLogin(user.Nome_Usuario, user.Senha);

            if (login == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(login);
            var refreshToken = TokenService.GenerateRefreshToken();
            TokenService.SaveRefreshToken(user.Nome_Usuario, refreshToken);

            login.Senha = "";
            return new
            {
                user = login,
                token = token,
                refreshToken = refreshToken,
            };
        }

        [HttpPost]
        [Route("refresh")]
        [AllowAnonymous]
        public IActionResult Refresh(string token, string refreshToken)
        {
            var principal = TokenService.GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name;
            var savedRefreshToken = TokenService.GetRefreshToken(username);
            if (savedRefreshToken != refreshToken)
            {
                throw new SecurityTokenException("Refresh token inválido");
            }

            var newJwtToken = TokenService.GenerateToken(principal.Claims);
            var newRefreshToken = TokenService.GenerateRefreshToken();
            TokenService.DeleteRefreshToken(username, refreshToken);
            TokenService.SaveRefreshToken(username, newRefreshToken);

            return new ObjectResult(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken
            });
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
