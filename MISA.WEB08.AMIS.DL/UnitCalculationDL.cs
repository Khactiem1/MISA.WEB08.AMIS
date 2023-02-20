﻿using Dapper;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng UnitCalculation từ tầng DL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class UnitCalculationDL : BaseDL<UnitCalculation>, IUnitCalculationDL
    {
        #region Field

        private IDatabaseHelper<UnitCalculation> _dbHelper;

        #endregion

        #region Contructor

        public UnitCalculationDL(IDatabaseHelper<UnitCalculation> dbHelper) : base(dbHelper)
        {
            _dbHelper = dbHelper;
        }

        #endregion

        #region Method

        #endregion
    }
}
