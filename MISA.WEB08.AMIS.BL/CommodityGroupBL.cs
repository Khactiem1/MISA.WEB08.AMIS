using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Result;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng CommodityGroup từ tầng BL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class CommodityGroupBL : BaseBL<CommodityGroup>, ICommodityGroupBL
    {
        #region Field

        private ICommodityGroupDL _commodityGroupDL;

        #endregion

        #region Contructor

        public CommodityGroupBL(ICommodityGroupDL commodityGroupDL) : base(commodityGroupDL)
        {
            _commodityGroupDL = commodityGroupDL;
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
                FileName = "Danh sách nhóm vật tư hàng hoá",
                Header = "DANH SÁCH NHÓM VẬT TƯ HÀNG HOÁ"
            };
        }

        /// <summary>
        /// Hàm xử lý custom override validate model 
        /// </summary>
        /// <param name="employee">Record cần custom validate</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override ServiceResponse? CustomValidate(CommodityGroup commodityGroup)
        {
            var validateFailures = "";
            if (commodityGroup.CommodityGroupID.ToString() == commodityGroup.ParentID && !string.IsNullOrEmpty(commodityGroup.ParentID) && !string.IsNullOrEmpty(commodityGroup.CommodityGroupID.ToString()))
            {
                validateFailures = "validate.commoditygroup_not_itself";
            }
            if (!string.IsNullOrEmpty(validateFailures))
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = validateFailures,
                    ErrorCode = MisaAmisErrorCode.InvalidInput
                };
            }
            else
            {
                return new ServiceResponse
                {
                    Success = true
                };
            }
        }

        /// <summary>
        /// Hàm xử lý custom kết quả validate cho bản ghi cần validate
        /// </summary>
        /// <param name="record">Record cần custom validate</param>
        /// <param name="line">Line nhập khẩu</param>
        /// <param name="errorDetail">Lỗi chi tiết khi nhập</param>
        /// <param name="status">Trạng thái nhập khẩu</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override void CustomResultValidate(ref CommodityGroup record, int line, string? errorDetail, string? status)
        {
            record.CommodityGroupID = Guid.NewGuid();
            record.LineExcel = line;
            record.ErrorDetail = errorDetail;
            record.StatusImportExcel = status;
        }

        /// <summary>
        /// Hàm xử lý custom validate đối với nhập từ tệp
        /// </summary>
        /// <param name="listRecord">Danh sách từ tệp</param>
        /// <param name="record">Record cần custom validate</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override ServiceResponse CustomValidateImportXlsx(CommodityGroup record, List<CommodityGroup> listRecord)
        {
            var validateFailures = "";
            int count = listRecord.Count(e => e.CommodityCode == record.CommodityCode);
            if (count >= 2)
            {
                validateFailures = $"validate.unique_import MESSAGE.VALID.SPLIT CommodityCode MESSAGE.VALID.SPLIT {record.CommodityCode}";
            }
            if (!string.IsNullOrEmpty(validateFailures))
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = validateFailures,
                    ErrorCode = MisaAmisErrorCode.InvalidInput
                };
            }
            return new ServiceResponse
            {
                Success = true
            };
        }

        #endregion
    }
}
