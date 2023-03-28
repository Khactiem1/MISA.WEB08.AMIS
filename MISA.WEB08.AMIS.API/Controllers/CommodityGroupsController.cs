using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common.Entities;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// API dữ liệu với bảng CommodityGroup
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm (21/09/2022)
    public class CommodityGroupsController : BasesController<CommodityGroup>
    {
        #region Field

        private ICommodityGroupBL _commodityGroupBL;

        #endregion

        #region Contructor

        public CommodityGroupsController(ICommodityGroupBL commodityGroupBL) : base(commodityGroupBL)
        {
            _commodityGroupBL = commodityGroupBL;
        }

        #endregion

        #region Method

        #region API GET

        #endregion

        #endregion
    }
}
