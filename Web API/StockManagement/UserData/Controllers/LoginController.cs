using UserData.Models;
using UserData.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UserData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IJWTAuthentication jWTAuthentication;
        public LoginController(IJWTAuthentication _jWTAuthentication)
        {
            jWTAuthentication = _jWTAuthentication;
        }

        [HttpPost]
        [Route("ValidateUser")]
        public IActionResult ValidateUser([FromBody]Login login)
        {
            Result res = new Result();
            res = jWTAuthentication.validateUser(login);

            if (res.isSuccess)
                return Ok(JsonConvert.SerializeObject(res));
            else if(res.Data == "3")
                return NotFound(JsonConvert.SerializeObject(res));
            else
                return BadRequest(JsonConvert.SerializeObject(res));
        }
    }
}
