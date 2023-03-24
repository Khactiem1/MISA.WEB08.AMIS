using Microsoft.AspNetCore.Mvc;
using System;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resources;
using MISA.WEB08.AMIS.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MISA.WEB08.AMIS.Common.Result;
using System.Collections.Generic;
using MISA.WEB08.AMIS.Common.Enums;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// API dữ liệu với bảng employee
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm (21/09/2022)
    //[Authorize]
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
        /// <returns>file Excel chứa dữ liệu danh sách </returns>
        /// CreatedBy: Nguyễn Khắc Tiềm (6/10/2022)
        [HttpPost("export_data")]
        public IActionResult GetEmployeeExport([FromBody] Dictionary<string, object> formData)
        {
            var excelName = _employeeBL.GetEmployeeExport(formData);
            if (excelName != null)
            {
                return StatusCode(StatusCodes.Status200OK, new ServiceResponse
                {
                    Success = true,
                    Data = excelName
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new ServiceResponse
                {
                    Success = false,
                    ErrorCode = MisaAmisErrorCode.InvalidInput,
                    Data = new MisaAmisErrorResult(
                            MisaAmisErrorCode.InvalidInput,
                            Resource.DevMsg_ValidateFailed,
                            Resource.Message_export_null,
                            Resource.MoreInfo_Exception,
                            HttpContext.TraceIdentifier
                        )
                });
            }
        }

        #endregion

        #endregion
    }
}
