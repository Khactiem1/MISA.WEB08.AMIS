using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WEB08.AMIS.Common.Resource;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng employee từ tầng DL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class EmployeeDL : IEmployeeDL
    {
        #region Field

        /// <summary>
        /// chuỗi kết nối
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        /// </summary>
        private string SqlConnection { get; set; }

        #endregion

        #region Contructor

        /// <summary>
        /// lấy ra chuỗi kết nối từ file appsettings.json
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        /// </summary>
        /// <param name="config"></param>
        public EmployeeDL(IConfiguration config)
        {
            SqlConnection = config["ConnectionStrings:DefaultConnection"];
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm Lấy danh sách tất cả nhân viên
        /// </summary>
        /// <returns>Danh sách tất cả nhân viên</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetAllEmployees()
        {
            var mysqlConnection = new MySqlConnection(SqlConnection);
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_GetAll, typeof(Employee).Name);

            // thực hiện gọi vào DB
            var employeeList = mysqlConnection.Query<Employee>(
                storeProcedureName,
                commandType: System.Data.CommandType.StoredProcedure
                );
            return employeeList;
        }

        /// <summary>
        /// Hàm lấy ra nhân viên theo ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns>Thông tin chi tiết một nhân viên</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetEmployeesByID(Guid employeeID)
        {
            var mysqlConnection = new MySqlConnection(SqlConnection);
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_GetDetailOne, typeof(Employee).Name);

            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_EmployeeID", employeeID);

            // thực hiện gọi vào DB
            var employee = mysqlConnection.QueryFirstOrDefault<Employee>(
                storeProcedureName,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure
                );
            return employee;
        }

        /// <summary>
        /// Hàm lấy ra mã nhân viên tự sinh
        /// </summary>
        /// <returns>Mã nhân viên</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetEmployeeCodeNew()
        {
            var mysqlConnection = new MySqlConnection(SqlConnection);
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_GetNewCode, typeof(Employee).Name);


            // thực hiện gọi vào DB
            var employee_code = mysqlConnection.QueryFirstOrDefault<string>(
                storeProcedureName,
                commandType: System.Data.CommandType.StoredProcedure
                );
            return employee_code;
        }

        /// <summary>
        /// Hàm lấy ra danh sách nhân viên có lọc và phân trang
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="keyword"></param>
        /// <param name="sort"></param>
        /// <returns>Danh sách nhân viên và tổng số bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetFitterEmployees(int offset, int limit, string? keyword, string? sort)
        {
            var mysqlConnection = new MySqlConnection(SqlConnection);
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_GetFilterPaging, typeof(Employee).Name);

            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_Offset", offset);
            parameters.Add("v_Limit", limit);
            parameters.Add("v_Sort", sort);
            parameters.Add("v_Where", keyword);

            // thực hiện gọi vào DB
            var employees = mysqlConnection.QueryMultiple(
                storeProcedureName,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure
                );
            return new
            {
                employeeList = employees.Read<Employee>().ToList(),
                totalCount = employees.ReadSingle().totalCount,
            };
        }

        /// <summary>
        /// Hàm thêm mới một nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>ID nhân viên sau khi thêm</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Guid InsertEmployee(Employee employee)
        {
            // Khởi tạo kết nối với Mysql
            var mysqlConnection = new MySqlConnection(SqlConnection);
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_InsertOne, typeof(Employee).Name);

            // tạo employeeID
            employee.EmployeeID = Guid.NewGuid();

            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_EmployeeID", employee.EmployeeID);
            parameters.Add("v_UnitID", employee.UnitID);
            parameters.Add("v_EmployeeCode", employee.EmployeeCode);
            parameters.Add("v_EmployeeName", employee.EmployeeName);
            parameters.Add("v_DateOfBirth", employee.DateOfBirth);
            parameters.Add("v_Gender", employee.Gender);
            parameters.Add("v_IdentityCard", employee.IdentityCard);
            parameters.Add("v_BranchBank", employee.BranchBank);
            parameters.Add("v_EmployeeTitle", employee.EmployeeTitle);
            parameters.Add("v_BankAccount", employee.BankAccount);
            parameters.Add("v_NameBank", employee.NameBank);
            parameters.Add("v_DayForIdentity", employee.DayForIdentity);
            parameters.Add("v_GrantAddressIdentity", employee.GrantAddressIdentity);
            parameters.Add("v_PhoneNumber", employee.PhoneNumber);
            parameters.Add("v_LandlinePhone", employee.LandlinePhone);
            parameters.Add("v_EmployeeEmail", employee.EmployeeEmail);
            parameters.Add("v_EmployeeAddress", employee.EmployeeAddress);
            parameters.Add("v_CreatedBy", employee.CreatedBy);

            // thực hiện gọi vào DB
            mysqlConnection.Execute(
                storeProcedureName,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure
                );
            return employee.EmployeeID;
        }

        /// <summary>
        /// Hàm cập nhật thông tin một nhân viên theo ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="employee"></param>
        /// <returns>ID nhân viên sau khi cập nhật</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Guid UpdateEmployee(Guid employeeID, Employee employee)
        {
            var mysqlConnection = new MySqlConnection(SqlConnection);
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_UpdateOne, typeof(Employee).Name);

            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_EmployeeID", employeeID);
            parameters.Add("v_UnitID", employee.UnitID);
            parameters.Add("v_EmployeeCode", employee.EmployeeCode);
            parameters.Add("v_EmployeeName", employee.EmployeeName);
            parameters.Add("v_DateOfBirth", employee.DateOfBirth);
            parameters.Add("v_Gender", employee.Gender);
            parameters.Add("v_IdentityCard", employee.IdentityCard);
            parameters.Add("v_BranchBank", employee.BranchBank);
            parameters.Add("v_EmployeeTitle", employee.EmployeeTitle);
            parameters.Add("v_BankAccount", employee.BankAccount);
            parameters.Add("v_NameBank", employee.NameBank);
            parameters.Add("v_DayForIdentity", employee.DayForIdentity);
            parameters.Add("v_GrantAddressIdentity", employee.GrantAddressIdentity);
            parameters.Add("v_PhoneNumber", employee.PhoneNumber);
            parameters.Add("v_LandlinePhone", employee.LandlinePhone);
            parameters.Add("v_EmployeeEmail", employee.EmployeeEmail);
            parameters.Add("v_EmployeeAddress", employee.EmployeeAddress);
            parameters.Add("v_ModifiedBy", employee.ModifiedBy);

            // thực hiện gọi vào DB result trả về là số lượng bản ghi bị ảnh hưởng
            mysqlConnection.Execute(
                storeProcedureName,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure
                );
            return employeeID;
        }

        /// <summary>
        /// Xoá một nhân viên theo ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns>ID nhân viên sau khi xoá</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Guid DeleteEmployee(Guid employeeID)
        {
            var mysqlConnection = new MySqlConnection(SqlConnection);
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_DeleteOne, typeof(Employee).Name);

            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_EmployeeID", employeeID);

            // thực hiện gọi vào DB
            mysqlConnection.Execute(
                storeProcedureName,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure
                );
            return employeeID;
        }

        #endregion
    }
}
