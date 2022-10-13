using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Resources;
using MISA.WEB08.AMIS.Common.Result;
using MISA.WEB08.AMIS.BL;
using System.IO;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// API dữ liệu với bảng employee
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm (21/09/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BasesController<Employee>
    {
        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        #region Contructor

        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion

        #region Method

        #region API GET

        /// <summary>
        /// Export data ra file excel
        /// </summary>
        /// <returns>file Excel chứa dữ liệu danh sách các nhân viên</returns>
        /// CreatedBy: Nguyễn Khắc Tiềm (6/10/2022)
        [HttpGet("export_data")]
        public IActionResult GetEmployeeExport([FromQuery] string? keyword, [FromQuery] string? sort)
        {
            try
            {
                var stream = _employeeBL.GetEmployeeExport(keyword, sort);
                stream.Position = 0;
                string excelName = $"{Resource.NameFileExcel} ({DateTime.Now.ToString("dd-MM-yyyy")}).xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
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
