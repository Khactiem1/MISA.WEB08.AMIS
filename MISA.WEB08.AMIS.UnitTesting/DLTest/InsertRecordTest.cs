using Microsoft.VisualStudio.TestTools.UnitTesting;
using MISA.WEB08.AMIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.UnitTesting
{
    [TestClass]
    public class InsertRecordTest<T>
    {
        #region Field

        private IDatabaseHelper<T> _dbHelper;

        #endregion

        #region Contructor

        [TestInitialize]
        public void Initialize(IDatabaseHelper<T> dbHelper)
        {
            _dbHelper = dbHelper;
        }

        #endregion

        #region MyRegion

        public void InsertRecordReturnSuccess()
        {

        }

        #endregion
    }
}
