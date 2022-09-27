using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Resource;
using MISA.WEB08.AMIS.Common.Result;
using MISA.WEB08.AMIS.BL;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// API dữ liệu với bảng employee
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm (21/09/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        #region Field

        private IEmployeeBL _EmployeeBL;

        #endregion

        #region Contructor

        public EmployeesController(IEmployeeBL employeeBL)
        {
            _EmployeeBL = employeeBL;
        }

        #endregion

        #region Method

        #region API GET

        /// <summary>
        /// API lấy ra danh sách tất cả nhân viên
        /// <return> Danh sách tất cả nhân viên
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                var employeeList = _EmployeeBL.GetAllEmployees();
                return StatusCode(StatusCodes.Status200OK, employeeList);
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

        /// <summary> 
        /// API lấy ra thông tin nhân viên chi tiết theo id
        /// <return> thông tin chi tiết một nhân viên
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpGet("{employeeID}")]
        public IActionResult GetEmployeesByID([FromRoute] Guid employeeID)
        {
            try
            {
                var employee = _EmployeeBL.GetEmployeesByID(employeeID);
                return StatusCode(StatusCodes.Status200OK, employee);
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

        /// <summary> 
        /// API trả về một mã nhân viên mới tự động tăng
        /// <return> Mã nhân viên mới lớn nhất tăng tự động
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpGet("next_value")]
        public IActionResult GetEmployeeCodeNew()
        {
            try
            {
                var employee_code = _EmployeeBL.GetEmployeeCodeNew();
                return StatusCode(StatusCodes.Status200OK, employee_code);
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

        /// <summary> 
        /// API trả về danh sách đã lọc và phân trang
        /// <return> Danh sách nhân viên sau khi phân trang, chỉ lấy ra số bản ghi và số trang yêu cầu, và tổng số lượg bản ghi có điều kiện
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpGet("fitter")]
        public IActionResult GetFitterEmployees([FromQuery] int offset, [FromQuery] int limit, [FromQuery] string? keyword, [FromQuery] string? sort)
        {
            try
            {
                var employees = _EmployeeBL.GetFitterEmployees(offset, limit, keyword, sort);
                return StatusCode(StatusCodes.Status200OK, employees);
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

        #region API POST

        /// <summary> 
        /// API thêm mới một nhân viên
        /// <return> ID nhân viên sau khi thêm
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            try
            {
                // Validate dữ liệu đầu vào 
                var properties = typeof(Employee).GetProperties();
                var validateFailures = new List<string>();
                foreach (var property in properties)
                {
                    string propertyName = property.Name;
                    var propertyValue = property.GetValue(employee);
                    var isNotNullOrEmptyAttribute = (IsNotNullOrEmptyAttribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAttribute));
                    if (isNotNullOrEmptyAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                    {
                        validateFailures.Add(isNotNullOrEmptyAttribute.ErrorMessage);
                    }
                }

                if (validateFailures.Count > 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.InvalidInput,
                    Resource.DevMsg_ValidateFailed,
                    Resource.UserMsg_ValidateFailed,
                    validateFailures,
                    HttpContext.TraceIdentifier
                    ));
                }
                Guid result = _EmployeeBL.InsertEmployee(employee);
                return StatusCode(StatusCodes.Status200OK, employee.EmployeeID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.Exception,
                    Resource.DevMsg_InsertFailed,
                    Resource.UserMsg_InsertFailed,
                    Resource.MoreInfo_InsertFailed,
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        /// <summary> 
        /// API xoá hàng loạt nhân viên
        /// <return> danh sách ID nhân viên sau khi xoá
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpPost("bulk_delete")]
        public IActionResult BulkDeleteEmployee([FromBody] Guid[] EmployeesID)
        {
            return StatusCode(StatusCodes.Status200OK, EmployeesID);
        }

        #endregion

        #region API PUT

        /// <summary> 
        /// API sửa một nhân viên
        /// <return> ID nhân viên sau khi sửa
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpPut("{employeeID}")]
        public IActionResult UpdateEmployee([FromRoute] Guid employeeID, [FromBody] Employee employee)
        {
            try
            {
                var result = _EmployeeBL.UpdateEmployee(employeeID, employee);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.Exception,
                    Resource.DevMsg_UpdateFailed,
                    Resource.UserMsg_UpdateFailed,
                    Resource.MoreInfo_Request,
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        #endregion

        #region API DELETE

        /// <summary>
        /// API xoá một nhân viên
        /// <return> ID nhân viên sau khi xoá
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpDelete("{employeeID}")]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeID)
        {
            try
            {
                var result = _EmployeeBL.DeleteEmployee(employeeID);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.Exception,
                    Resource.DevMsg_DeleteFailed,
                    Resource.UserMsg_DeleteFailed,
                    Resource.MoreInfo_Request,
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        #endregion

        #endregion
    }
}
