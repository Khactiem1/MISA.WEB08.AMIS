using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UnitController : Controller
    {
        /// <summary>
        /// API lấy ra danh sách tất cả đơn vị
        /// </summary>
        /// Created by : Nguyễn Khắc Tiềm 21.09.2022
        [HttpGet]
        public IActionResult GetAllUnits()
        {
            List<Unit> UnitList = new List<Unit>();
            UnitList.Add(new Unit
            {
                unit_id = Guid.NewGuid(),
                unit_code = "U001",
                unit_name = "Ngoại giao đoàn",
            });
            UnitList.Add(new Unit
            {
                unit_id = Guid.NewGuid(),
                unit_code = "U002",
                unit_name = "Duy tân",
            });
            return StatusCode(StatusCodes.Status200OK, UnitList);
        }
    }
}
