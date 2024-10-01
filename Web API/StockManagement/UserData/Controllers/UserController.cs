using UserData.Models;
using UserData.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UserData.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public IUserMaster _UserMaster { get; set; }
        public UserController(IUserMaster UserMaster)
        {
            _UserMaster = UserMaster;
        }

        [HttpGet]
        [Route("GetData")]
        public IActionResult GetData()
        {
            Result res = _UserMaster.GetData(null);
            return Ok(JsonConvert.SerializeObject(res));
        }

        //[HttpGet]
        //[Route("GetData")]
        //public IActionResult GetData(int intUserID)
        //{
        //    Result res = _UserMaster.GetData(intUserID);
        //    return Ok(JsonConvert.SerializeObject(res));
        //}

        [HttpPost]
        [Route("Add_Update_Data")]
        public IActionResult Add_Update_Data(UserModel obj)
        {
            Result res = _UserMaster.Insert_Update_Data(obj);
            if (res.isSuccess)
                return Created("", JsonConvert.SerializeObject(res));
            else
                return BadRequest(JsonConvert.SerializeObject(res));
        }

        [HttpPost]
        [Route("DeleteData")]
        public IActionResult DeleteData(UserModel obj)
        {
            Result res = _UserMaster.Delete_Data(obj);
            if (res.isSuccess)
                return Ok(JsonConvert.SerializeObject(res));
            else
                return BadRequest(JsonConvert.SerializeObject(res));
        }
    }
}
