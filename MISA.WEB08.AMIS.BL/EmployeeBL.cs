using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng employee từ tầng BL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class EmployeeBL : IEmployeeBL
    {
        #region Field

        private IEmployeeDL _EmployeeDL;

        #endregion

        #region Contructor

        public EmployeeBL(IEmployeeDL employeeDL)
        {
            _EmployeeDL = employeeDL;
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
            return _EmployeeDL.GetAllEmployees();
        }

        /// <summary>
        /// Hàm lấy ra nhân viên theo ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns>Thông tin chi tiết một nhân viên</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetEmployeesByID(Guid employeeID)
        {
            return _EmployeeDL.GetEmployeesByID(employeeID);
        }

        /// <summary>
        /// Hàm lấy ra mã nhân viên tự sinh
        /// </summary>
        /// <returns>Mã nhân viên</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetEmployeeCodeNew()
        {
            return _EmployeeDL.GetEmployeeCodeNew();
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
            return _EmployeeDL.GetFitterEmployees(offset, limit, keyword, sort);
        }

        /// <summary>
        /// Hàm thêm mới một nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>ID nhân viên sau khi thêm</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Guid InsertEmployee(Employee employee)
        {
            return _EmployeeDL.InsertEmployee(employee);
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
            return _EmployeeDL.UpdateEmployee(employeeID, employee);
        }

        /// <summary>
        /// Xoá một nhân viên theo ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns>ID nhân viên sau khi xoá</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Guid DeleteEmployee(Guid employeeID)
        {
            return _EmployeeDL.DeleteEmployee(employeeID);
        }

        #endregion
    }
}
