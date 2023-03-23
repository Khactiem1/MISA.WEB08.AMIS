using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng employee từ tầng BL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        /// <summary>
        /// Hàm Lấy danh sách bản ghi nhân viên theo từ khoá tìm kiếm không phân trang
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public string GetEmployeeExport(Dictionary<string, object> formData);
    }
}
