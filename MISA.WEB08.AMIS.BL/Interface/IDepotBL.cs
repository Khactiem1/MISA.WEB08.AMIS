using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MISA.WEB08.AMIS.Common.Entities;
using System.Threading.Tasks;
using System.IO;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng Depot từ tầng BL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public interface IDepotBL : IBaseBL<Depot>
    {
        /// <summary>
        /// Hàm Lấy danh sách bản ghi nhân viên theo từ khoá tìm kiếm không phân trang
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Stream GetExport(string? keyword, string? sort);
    }
}
