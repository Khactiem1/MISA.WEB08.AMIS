using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Result;
using MISA.WEB08.AMIS.DL;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Dữ liệu thao tác với DatACase và trả về với bảng CommodityGroup từ tầng BL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class InventoryItemBL : BaseBL<InventoryItem>, IInventoryItemBL
    {
        #region Field

        private IInventoryItemDL _inventoryItemBL;

        #endregion

        #region Contructor

        public InventoryItemBL(IInventoryItemDL inventoryItemBL) : base(inventoryItemBL)
        {
            _inventoryItemBL = inventoryItemBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm lấy ra tổng số lượng hàng sắp hết và hết hàng
        /// </summary>
        /// <returns></returns>
        //// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetInventoryStatus()
        {
            return _inventoryItemBL.GetInventoryStatus();
        }

        /// <summary>
        /// Hàm custom dữ liệu xuất file
        /// </summary>
        /// <param name="property">Cột dữ liệu cần custom</param>
        /// <returns>Trả ra false khi không custom gì</returns>
        ///  NK Tiềm 05/10/2022
        public override bool CustomValuePropertieExport(PropertyInfo property, ref ExcelWorksheet sheet, int indexRow, int indexBody, InventoryItem inventoryItem)
        {
            if (property.Name == "Nature")
            {
                sheet.Cells[indexRow + 4, indexBody].Value = inventoryItem.Nature == Nature.Goods ? "Hàng hoá" : inventoryItem.Nature == Nature.Service ? "Dịch vụ" : inventoryItem.Nature == Nature.Materials ? "Nguyên vật liệu" : inventoryItem.Nature == Nature.FinishedProduct ? "Thành phẩm" : "Dụng cụ công cụ";
                return true;
            }
            else if (property.Name == "DepreciatedTax")
            {
                sheet.Cells[indexRow + 4, indexBody].Value = inventoryItem.DepreciatedTax == DepreciatedTax.undefined ? "Không xác định" : inventoryItem.DepreciatedTax == DepreciatedTax.no_tax_reduction ? "Không giảm thuế" : inventoryItem.DepreciatedTax == DepreciatedTax.tax_reduction ? "Giảm thuế" : "";
                return true;
            }
            return false;
        }

        /// <summary>
        /// Hàm custom dữ liệu tên file, header, ... khi xuất file
        /// </summary>
        /// <returns></returns>
        ///  NK Tiềm 05/10/2022
        public override OptionExport CustomOptionExport()
        {
            return new OptionExport
            {
                FileName = "Danh sách hàng hoá, dịch vụ",
                Header = "DANH SÁCH HÀNG HOÁ, DỊCH VỤ"
            };
        }

        /// <summary>
        /// Hàm xử lý custom tham số cho bản ghi cần validate
        /// </summary>
        /// <param name="record">Record cần custom validate</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override void CustomParameterValidate(ref InventoryItem record)
        {
            record.DepotID = Guid.NewGuid();
            record.UnitCalculationID = Guid.NewGuid();
        }

        /// <summary>
        /// Hàm xử lý custom kết quả validate cho bản ghi cần validate
        /// </summary>
        /// <param name="record">Record cần custom validate</param>
        /// <param name="errorDetail">Lỗi chi tiết khi nhập</param>
        /// <param name="status">Trạng thái nhập khẩu</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override void CustomResultValidate(ref InventoryItem record, string? errorDetail, string? status)
        {
            record.InventoryItemID = Guid.NewGuid();
            record.ErrorDetail = errorDetail;
            record.StatusImportExcel = status;
        }

        /// <summary>
        /// Hàm xử lý custom validate đối với nhập từ tệp
        /// </summary>
        /// <param name="listRecord">Danh sách từ tệp</param>
        /// <param name="record">Record cần custom validate</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override ServiceResponse CustomValidateImportXlsx(InventoryItem record, List<InventoryItem> listRecord)
        {
            var validateFailures = "";
            int count = listRecord.Count(e => e.InventoryItemCode == record.InventoryItemCode);
            if (count >= 2)
            {
                validateFailures = $"validate.unique_import MESSAGE.VALID.SPLIT InventoryItemCode MESSAGE.VALID.SPLIT {record.InventoryItemCode}";
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
        /// Hàm xử lý đưa những bản ghi không hợp lệ vào list Fail, xoá bản ghi không hợp lệ ở list pass sau khi nhận kết quả từ proc
        /// </summary>
        /// <param name="listFail">Danh sách bản ghi không hợp lệ</param>
        /// <param name="listPass">Danh sách bản ghi  hợp lệ</param>
        /// <param name="listFailResultProc">Danh sách bản ghi không hơp lệ trả về từ Proc</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override void CustomListFailResultImportXlsx(ref List<object> listFail, ref List<InventoryItem> listPass, List<InventoryItem> listFailResultProc)
        {
            foreach (var item in listFailResultProc)
            {
                item.InventoryItemID = Guid.NewGuid();
                listFail.Add(item);
                listPass.RemoveAll(x => x.InventoryItemCode == item.InventoryItemCode);
            }
        }

        /// <summary>
        /// Hàm xử lý lấy những dữ liệu chuẩn đưa vào list để tiến hành nhập từ tệp
        /// </summary>
        /// <param name="json">Dữ liệu sẽ được chuẩn hoá</param>
        /// <param name="listFail">Danh sách dữ liệu không hợp lệ</param>
        /// <param name="list">Danh sách dữ liệu đúng kiểu</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override void CustomListTypeImportXlsx(string json, ref List<object> listFail, ref List<InventoryItem> list)
        {
            void setFail(ref InventoryItemImport item, ref List<object> listFail, ref bool check, string propertyName)
            {
                item.InventoryItemID = Guid.NewGuid().ToString();
                item.ErrorDetail = $"validate.malformed MESSAGE.VALID.SPLIT {propertyName}";
                item.StatusImportExcel = "common.illegal";
                listFail.Add(item);
                check = true;
            }
            List<InventoryItemImport> listData = JsonConvert.DeserializeObject<List<InventoryItemImport>>(json.ToString());
            int count = 1;
            foreach (var temp in listData)
            {
                var item = temp;
                count++;
                item.LineExcel = count;
                var properties = typeof(InventoryItemImport).GetProperties();
                bool check = false;
                foreach (var property in properties)
                {
                    var propertyValue = property.GetValue(item)?.ToString();
                    var validateString = (ValidateString?)Attribute.GetCustomAttribute(property, typeof(ValidateString));
                    if (validateString != null)
                    {
                        if (validateString.IsDate && !string.IsNullOrEmpty(propertyValue))
                        {
                            if (!Validate<InventoryItemImport>.IsDateFormatValid(propertyValue))
                            {
                                setFail(ref item, ref listFail, ref check, property.Name);
                                break;
                            }
                        }
                        if (validateString.IsBoolean && !string.IsNullOrEmpty(propertyValue))
                        {
                            if (!Validate<InventoryItemImport>.IsBoolean(propertyValue))
                            {
                                setFail(ref item, ref listFail, ref check, property.Name);
                                break;
                            }
                        }
                        if (validateString.IsNumber && !string.IsNullOrEmpty(propertyValue))
                        {
                            if (!Validate<InventoryItemImport>.IsNumeric(propertyValue))
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
                if (!string.IsNullOrEmpty(item.Nature))
                {
                    if (!Validate<InventoryItemImport>.IsNature(item.Nature))
                    {
                        setFail(ref item, ref listFail, ref check, "Nature");
                        continue;
                    }
                }
                if (!string.IsNullOrEmpty(item.DepreciatedTax))
                {
                    if (!Validate<InventoryItemImport>.IsDepreciatedTax(item.DepreciatedTax))
                    {
                        setFail(ref item, ref listFail, ref check, "DepreciatedTax");
                        continue;
                    }
                }
                item.InventoryItemID = null;
                item.DepotID = null;
                item.UnitCalculationID = null;
                list.Add(JsonConvert.DeserializeObject<InventoryItem>(JsonConvert.SerializeObject(item).ToString()));
            }
        }

        #endregion
    }
}
