using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Result;
using MISA.WEB08.AMIS.DL;
using Newtonsoft.Json;
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
        /// <param name="errorDetail">Lỗi chi tiết khi nhập</param>
        /// <param name="status">Trạng thái nhập khẩu</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override void CustomResultValidate(ref CommodityGroup record, string? errorDetail, string? status)
        {
            record.CommodityGroupID = Guid.NewGuid();
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

        /// <summary>
        /// Hàm xử lý lấy những dữ liệu chuẩn đưa vào list để tiến hành nhập từ tệp
        /// </summary>
        /// <param name="json">Dữ liệu sẽ được chuẩn hoá</param>
        /// <param name="listFail">Danh sách dữ liệu không hợp lệ</param>
        /// <param name="list">Danh sách dữ liệu đúng kiểu</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override void CustomListTypeImportXlsx(string json, ref List<object> listFail, ref List<CommodityGroup> list)
        {
            void setFail(ref CommodityGroupImport item, ref List<object> listFail, ref bool check, string propertyName)
            {
                item.CommodityGroupID = Guid.NewGuid().ToString();
                item.ErrorDetail = $"validate.malformed MESSAGE.VALID.SPLIT {propertyName}";
                item.StatusImportExcel = "common.illegal";
                listFail.Add(item);
                check = true;
            }
            List<CommodityGroupImport> listData = JsonConvert.DeserializeObject<List<CommodityGroupImport>>(json.ToString());
            int count = 1;
            foreach (var temp in listData)
            {
                var item = temp;
                count++;
                item.LineExcel = count;
                var properties = typeof(CommodityGroupImport).GetProperties();
                bool check = false;
                foreach (var property in properties)
                {
                    var propertyValue = property.GetValue(item)?.ToString();
                    var validateString = (ValidateString?)Attribute.GetCustomAttribute(property, typeof(ValidateString));
                    if (validateString != null)
                    {
                        if (validateString.IsDate && !string.IsNullOrEmpty(propertyValue))
                        {
                            if (!Validate<CommodityGroupImport>.IsDateFormatValid(propertyValue))
                            {
                                setFail(ref item, ref listFail, ref check, property.Name);
                                break;
                            }
                        }
                        if (validateString.IsBoolean && !string.IsNullOrEmpty(propertyValue))
                        {
                            if (!Validate<CommodityGroupImport>.IsBoolean(propertyValue))
                            {
                                setFail(ref item, ref listFail, ref check, property.Name);
                                break;
                            }
                        }
                        if (validateString.IsNumber && !string.IsNullOrEmpty(propertyValue))
                        {
                            if (!Validate<CommodityGroupImport>.IsNumeric(propertyValue))
                            {
                                setFail(ref item, ref listFail, ref check, property.Name);
                                break;
                            }
                        }
                    }
                }
                if (check)
                {
                    continue;
                }
                item.CommodityGroupID = null;
                list.Add(JsonConvert.DeserializeObject<CommodityGroup>(JsonConvert.SerializeObject(item).ToString()));
            }
        }

        #endregion
    }
}
