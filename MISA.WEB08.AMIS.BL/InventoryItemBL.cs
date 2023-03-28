using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.DL;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Dữ liệu thao tác với DatACase và trả về với bảng CommodityGroup từ tầng BL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class InventoryItemBL : BaseBL<InventoryItem>, IInventoryItemBL
    {
        #region Field

        private IInventoryItemDL _inventoryItemBL;

        #endregion

        #region Contructor

        public InventoryItemBL(IInventoryItemDL inventoryItemBL) : base(inventoryItemBL)
        {
            _inventoryItemBL = inventoryItemBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm lấy ra tổng số lượng hàng sắp hết và hết hàng
        /// </summary>
        /// <returns></returns>
        //// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetInventoryStatus()
        {
            return _inventoryItemBL.GetInventoryStatus();
        }

        #endregion
    }
}
