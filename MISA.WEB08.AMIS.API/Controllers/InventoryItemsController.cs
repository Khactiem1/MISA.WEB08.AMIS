using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common.Entities;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// API dữ liệu với bảng InventoryItem
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm (21/09/2022)
    public class InventoryItemsController : BasesController<InventoryItem>
    {
        #region Field

        private IInventoryItemBL _inventoryItemBL;

        #endregion

        #region Contructor

        public InventoryItemsController(IInventoryItemBL inventoryItemBL) : base(inventoryItemBL)
        {
            _inventoryItemBL = inventoryItemBL;
        }

        #endregion

        #region Method

        #region API GET

        #endregion

        #endregion
    }
}
