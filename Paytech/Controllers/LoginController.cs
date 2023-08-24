using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paytech.Models;
using Paytech.Services;

namespace Paytech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
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
