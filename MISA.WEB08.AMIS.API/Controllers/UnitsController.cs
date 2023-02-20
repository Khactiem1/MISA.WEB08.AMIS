using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.BL;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// API dữ liệu với bảng unit
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm (21/09/2022)
    public class UnitsController : BasesController<Unit>
    {
        #region Contructor

        public UnitsController(IUnitBL unitBL) : base(unitBL)
        {
        }

        #endregion
    }
}