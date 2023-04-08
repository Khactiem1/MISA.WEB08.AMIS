using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Resources;
using MISA.WEB08.AMIS.Common.Result;
using System.Threading.Tasks;

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

        /// <summary>
        /// API lấy ra tổng số lượng hàng sắp hết và hết hàng
        /// </summary>
        /// <returns></returns>
        //// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        [HttpGet("GetInventoryStatus")]
        public async Task<IActionResult> GetInventoryStatus()
        {
            var result = await Task.FromResult(_inventoryItemBL.GetInventoryStatus());
            if(result != null)
            {
                return StatusCode(StatusCodes.Status201Created, new ServiceResponse
                {
                    Success = true,
                    Data = result
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ServiceResponse
            {
                Success = false,
                ErrorCode = MisaAmisErrorCode.NotFoundData,
                Data = new MisaAmisErrorResult(
                            MisaAmisErrorCode.NotFoundData,
                            Resource.DevMsg_ValidateFailed,
                            Resource.Message_notFoundData,
                            Resource.MoreInfo_Exception,
                            HttpContext.TraceIdentifier
                        )
            });
        }

        #endregion

        #endregion
    }
}
