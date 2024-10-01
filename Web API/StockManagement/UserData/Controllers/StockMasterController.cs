using UserData.Models;
using UserData.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UserData.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class StockMasterController : ControllerBase
    {
        public IStockMaster _StockMaster;
        public StockMasterController(IStockMaster StockMaster)
        {
            _StockMaster = StockMaster;
        }

        [HttpGet]
        [Route("GetData")]
        public IActionResult GetData()
        {
            Result res = _StockMaster.GetData(null);
            return Ok(JsonConvert.SerializeObject(res));
        }

        //[HttpGet]
        //[Route("GetData")]
        //public IActionResult GetData(int intUserID)
        //{
        //    Result res = _StockMaster.GetData(intUserID);
        //    return Ok(JsonConvert.SerializeObject(res));
        //}

        [HttpPost]
        [Route("Add_Update_Data")]
        public IActionResult Add_Update_Data(StockMasterModel obj)
        {
            Result res = _StockMaster.Insert_Update_Data(obj);
            if (res.isSuccess)
                return Created("", JsonConvert.SerializeObject(res));
            else
                return BadRequest(JsonConvert.SerializeObject(res));
        }

        [HttpPost]
        [Route("DeleteData")]
        public IActionResult DeleteData(StockMasterModel obj)
        {
            Result res = _StockMaster.Delete_Data(obj);
            if (res.isSuccess)
                return Ok(JsonConvert.SerializeObject(res));
            else
                return BadRequest(JsonConvert.SerializeObject(res));
        }
    }
}
