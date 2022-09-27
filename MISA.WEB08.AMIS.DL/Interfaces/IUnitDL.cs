using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng Unit từ tầng DL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public interface IUnitDL
    {
        /// <summary>
        /// Hàm lấy ra danh sách tất cả đơn vị
        /// </summary>
        /// <returns>Danh sách đơn vị</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetAllUnits();
    }
}
