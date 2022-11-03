using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>
        /// Export data ra file excel
        /// </summary>
        /// <returns>file Excel chứa dữ liệu danh sách </returns>
        /// CreatedBy: Nguyễn Khắc Tiềm (6/10/2022)
        [HttpGet("export_data")]
        public IActionResult GetExport([FromQuery] string? keyword, [FromQuery] string? sort)
        {
            var stream = _commodityGroupBL.GetExport(keyword, sort);
            stream.Position = 0;
            string excelName = $"{"Danh_Sach_Nhom_Vat_Tu_Hang_Hoa_Dich_Vu"} ({DateTime.Now.ToString("dd-MM-yyyy")}).xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        #endregion

        #endregion
    }
}
