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
    /// API dữ liệu với bảng depot
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm (21/09/2022)
    public class DepotsController : BasesController<Depot>
    {
        #region Field

        private IDepotBL _depotBL;

        #endregion

        #region Contructor

        public DepotsController(IDepotBL depotBL) : base(depotBL)
        {
            _depotBL = depotBL;
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
            var stream = _depotBL.GetExport(keyword, sort);
            stream.Position = 0;
            string excelName = $"{"Danh_sach_nha_kho"} ({DateTime.Now.ToString("dd-MM-yyyy")}).xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        #endregion

        #endregion
    }
}
