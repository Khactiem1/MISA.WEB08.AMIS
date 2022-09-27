using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WEB08.AMIS.DL;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng Unit từ tầng BL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class UnitBL : IUnitBL
    {
        #region Field

        private IUnitDL _UnitDL;

        #endregion

        #region Contructor

        public UnitBL(IUnitDL unitDL)
        {
            _UnitDL = unitDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm lấy ra danh sách tất cả đơn vị
        /// </summary>
        /// <returns>Danh sách đơn vị</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetAllUnits()
        {
            return _UnitDL.GetAllUnits();
        }

        #endregion
    }
}
