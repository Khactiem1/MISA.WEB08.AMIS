using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Result;
using MISA.WEB08.AMIS.DL;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng Unit từ tầng BL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class BranchBL : BaseBL<Branch>, IBranchBL
    {
        #region Field

        private IBranchDL _branchDL;

        #endregion

        #region Contructor

        public BranchBL(IBranchDL branchDL) : base(branchDL)
        {
            _branchDL = branchDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm custom dữ liệu tên file, header, ... khi xuất file
        /// </summary>
        /// <returns></returns>
        ///  NK Tiềm 05/10/2022
        public override OptionExport CustomOptionExport()
        {
            return new OptionExport
            {
                FileName = "Danh sách đơn vị",
                Header = "DANH SÁCH ĐƠN VỊ"
            };
        }

        #endregion
    }
}
