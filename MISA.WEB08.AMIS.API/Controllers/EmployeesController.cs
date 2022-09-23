using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MISA.WEB08.AMIS.API.Enums;
using System.Linq;
using System.Threading.Tasks;
using MISA.WEB08.AMIS.API.Entities;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Dapper;

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
        public string StrConnection { get; set; } // chuỗi kết nối
        /// <summary>
        /// lấy ra chuỗi kết nối từ file appsettings.json
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        /// </summary>
        /// <param name="config"></param>
        public EmployeesController(IConfiguration configuration)
        {
            StrConnection = configuration["ConnectionStrings:DefaultConnection"];
        }

        #region API GET
        /// <summary> API lấy ra danh sách tất cả nhân viên
        /// <return> Danh sách tất cả nhân viên, có tổng số lượg nhân viên
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                StrConnection = "Server = localhost; Port=3306; Database=misa.web08.amis; UID=root; Pwd=Huekute5a; ConvertZeroDateTime=True";
                var mysqlConnection = new MySqlConnection(StrConnection);
                // chuẩn bị câu lệnh MySQL
                string storeProcedureName = "Proc_employee_GetAllEmployee";


                // thực hiện gọi vào DB
                var employeeList = mysqlConnection.Query<Employee>(
                    storeProcedureName
                    , commandType: System.Data.CommandType.StoredProcedure
                    );

                return StatusCode(StatusCodes.Status200OK, employeeList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.Exception,
                    ex.Message,
                    StrConnection,
                    "https://openapi.google.com/api/errorcode"
                    ));
            }
        }

        /// <summary> API lấy ra thông tin nhân viên chi tiết theo id
        /// <return> thông tin chi tiết một nhân viên
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpGet("{employeeID}")]
        public IActionResult GetEmployeesByID([FromRoute] Guid employeeID)
        {
            try
            {
                StrConnection = "Server = localhost; Port=3306; Database=misa.web08.amis; UID=root; Pwd=Huekute5a; ConvertZeroDateTime=True";
                var mysqlConnection = new MySqlConnection(StrConnection);
                // chuẩn bị câu lệnh MySQL
                string storeProcedureName = "Proc_employee_GetDetailOneEmployee";

                // Khởi tạo các parameter để chèn vào trong Proc
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("id", employeeID);

                // thực hiện gọi vào DB
                var employee = mysqlConnection.QueryFirstOrDefault<Employee>(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                return StatusCode(StatusCodes.Status200OK, employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.Exception,
                    ex.Message,
                    StrConnection,
                    "https://openapi.google.com/api/errorcode"
                    ));
            }
        }

        /// <summary> API trả về một mã nhân viên mới tự động tăng
        /// <return> Mã nhân viên mới lớn nhất tăng tự động
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpGet("next_value")]
        public IActionResult GetEmployeeCodeNew()
        {
            return StatusCode(StatusCodes.Status200OK, "NV00010");
        }

        /// <summary> API trả về danh sách đã lọc và phân trang
        /// <return> Danh sách nhân viên sau khi phân trang, chỉ lấy ra số bản ghi và số trang yêu cầu, và tổng số lượg bản ghi có điều kiện
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpGet("fitter")]
        public IActionResult GetFitterEmployees([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] string? search)
        {
            return StatusCode(StatusCodes.Status200OK, pageSize + " Bản ghi " + "Trang số " + pageNumber + " từ khoá tìm kiếm " + search);
        }
        #endregion

        #region API POST
        /// <summary> API thêm mới một nhân viên
        /// <return> Mã nhân viên sau khi thêm
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            employee.employee_id = Guid.NewGuid();
            return StatusCode(StatusCodes.Status201Created, employee.employee_id);
        }

        /// <summary> API xoá hàng loạt nhân viên
        /// <return> danh sách Mã nhân viên sau khi xoá
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpPost("bulk_delete")]
        public IActionResult BulkDeleteEmployee([FromBody] Guid[] EmployeesID)
        {
            return StatusCode(StatusCodes.Status200OK, EmployeesID);
        }
        #endregion

        #region API PUT
        /// <summary> API sửa một nhân viên
        /// <return> Mã nhân viên sau khi sửa
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpPut("{employeeID}")]
        public IActionResult UpdateEmployee([FromRoute] Guid employeeID, [FromBody] Employee employee)
        {
            return StatusCode(StatusCodes.Status200OK, employeeID);
        }
        #endregion
        #region API DELETE
        /// <summary> API xoá một nhân viên
        /// <return> Mã nhân viên sau khi xoá
        /// <summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpDelete("{employeeID}")]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeID)
        {
            return StatusCode(StatusCodes.Status200OK, employeeID);
        }
        #endregion
    }
}
