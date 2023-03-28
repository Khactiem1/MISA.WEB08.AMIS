using MISA.WEB08.AMIS.Common.Entities;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng CommodityGroup từ tầng DL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class CommodityGroupDL : BaseDL<CommodityGroup>, ICommodityGroupDL
    {
        #region Field

        private IDatabaseHelper<CommodityGroup> _dbHelper;

        #endregion

        #region Contructor

        public CommodityGroupDL(IDatabaseHelper<CommodityGroup> dbHelper) : base(dbHelper)
        {
            _dbHelper = dbHelper;
        }

        #endregion

        #region Method

        #endregion
    }
}
