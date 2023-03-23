﻿using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng Unit từ tầng BL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class BranchBL : BaseBL<Branch>, IBranchBL
    {
        #region Contructor

        public BranchBL(IBranchDL unitDL) : base(unitDL)
        {
        }

        #endregion
    }
}