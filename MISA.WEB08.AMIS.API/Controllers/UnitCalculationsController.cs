using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common.Entities;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// API dữ liệu với bảng  UnitCalculation
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm (21/09/2022)
    public class UnitCalculationsController : BasesController<UnitCalculation>
    {
        #region Field

        private IUnitCalculationBL _unitCalculationBL;

        #endregion

        #region Contructor

        public UnitCalculationsController(IUnitCalculationBL unitCalculationBL) : base(unitCalculationBL)
        {
            _unitCalculationBL = unitCalculationBL;
        }

        #endregion

        #region Method

        #region API GET

        #endregion

        #endregion
    }
}
