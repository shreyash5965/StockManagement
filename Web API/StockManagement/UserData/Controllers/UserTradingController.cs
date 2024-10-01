using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserData.Models;
using UserData.Services;

namespace UserData.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class UserTradingController : ControllerBase
    {
        public IUserTrading userTrading;

        public UserTradingController(IUserTrading _userTrading)
        {
            userTrading = _userTrading;
        }

        [HttpGet]
        [Route("GetTradingHistory")]
        public IActionResult GetTradingHistory()
        {
            Result res = userTrading.GetUserTraingDetails();

            if (res.isSuccess)
                return Ok(res);
            else
                return BadRequest(res);
        }

        [HttpGet]
        [Route("GetDashboardData")]
        public IActionResult GetDashboardData()
        {
            Result res = userTrading.GetDashboardData();

            if (res.isSuccess)
                return Ok(res);
            else
                return BadRequest(res);
        }

        [HttpPost]
        [Route("Add_Update_Data")]
        public IActionResult Add_Update_Data(UserStockDetails obj)
        {
            Result res = userTrading.Insert_Update_Data(obj);

            if (res.isSuccess)
                return Ok(res);
            else
                return BadRequest(res);
        }

        [HttpPost]
        [Route("GetHistoryData")]
        public IActionResult GetHistoryData(TradingHistoryFilter obj)
        {
            Result res = userTrading.GetTradingHistory(obj);

            if (res.isSuccess)
                return Ok(res);
            else
                return BadRequest(res);
        }
    }
}
