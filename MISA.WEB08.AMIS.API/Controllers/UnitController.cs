using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resource;
using MISA.WEB08.AMIS.Common.Result;
using MISA.WEB08.AMIS.BL;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// API dữ liệu với bảng unit
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm (21/09/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UnitController : Controller
    {
        #region Field

        private IUnitBL _UnitBL;

        #endregion

        #region Contructor

        public UnitController(IUnitBL unitBL)
        {
            _UnitBL = unitBL;
        }

        #endregion

        #region Method

        #region API GET
        /// <summary>
        /// API lấy ra danh sách tất cả đơn vị
        /// </summary>
        /// Created by : Nguyễn Khắc Tiềm 21.09.2022
        [HttpGet]
        public IActionResult GetAllUnits()
        {
            try
            {
                var unitList = _UnitBL.GetAllUnits();
                return StatusCode(StatusCodes.Status200OK, unitList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier
                    ));
            }
        }
        #endregion

        #endregion
    }
}
