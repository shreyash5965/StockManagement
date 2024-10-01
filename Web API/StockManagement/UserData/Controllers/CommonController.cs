using UserData.Models;
using UserData.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UserData.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService commonService;
        public CommonController(ICommonService _commonService)
        {
            commonService = _commonService;
        }

        [HttpGet]
        [Route("GetStockList")]
        public IActionResult GetStockList()
        {
            Result res = new Result();
            res = commonService.GetStockList();

            if (res.isSuccess)
                return Ok(JsonConvert.SerializeObject(res));
            else if(res.Data == "3")
                return NotFound(JsonConvert.SerializeObject(res));
            else
                return BadRequest(JsonConvert.SerializeObject(res));
        }
    }
}
