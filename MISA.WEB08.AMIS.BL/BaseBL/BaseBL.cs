using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Resources;
using MISA.WEB08.AMIS.Common.Result;
using MISA.WEB08.AMIS.DL;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng trong Database từ tầng BL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class BaseBL<T> : CustomVirtual<T>, IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Contructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm Lấy danh sách tất cả bản ghi của 1 bảng
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual object GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        /// <summary>
        /// Hàm Lấy danh sách dropdown
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetDropdown()
        {
            return _baseDL.GetDropdown();
        }

        /// <summary>
        /// Hàm lấy ra bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>Thông tin chi tiết một bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual object GetRecordByID(string recordID)
        {
            return _baseDL.GetRecordByID(recordID);
        }

        /// <summary>
        /// Hàm lấy ra mã record tự sinh
        /// </summary>
        /// <returns>Mã bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual object GetRecordCodeNew()
        {
            return _baseDL.GetRecordCodeNew();
        }

        /// <summary>
        /// Hàm lấy ra danh sách record có lọc và phân trang
        /// </summary>
        /// <param name="formData">Từ khoá tìm kiếm</param>
        /// <returns>Danh sách record và tổng số bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual object GetFitterRecords(Dictionary<string, object> formData)
        {
            var v_Offset = int.Parse(formData["v_Offset"].ToString());
            var v_Limit = int.Parse(formData["v_Limit"].ToString());
            var v_Where = formData.Keys.Contains("v_Where") ? Convert.ToString(formData["v_Where"]).Trim() : null;
            var v_Sort = formData.Keys.Contains("v_Sort") ? Convert.ToString(formData["v_Sort"]) : null;
            var v_Select = formData.Keys.Contains("v_Select") ? JsonConvert.DeserializeObject<List<string>>(Convert.ToString(formData["v_Select"])) : new List<string>();

            List<ComparisonTypeSearch> listQuery = formData.Keys.Contains("CustomSearch") ? JsonConvert.DeserializeObject <List<ComparisonTypeSearch>>(Convert.ToString(formData["CustomSearch"])) : new List<ComparisonTypeSearch>();
            string v_Query = "";
            foreach (ComparisonTypeSearch item in listQuery)
            {
                if (!string.IsNullOrEmpty(item.ValueSearch) || item.ComparisonType == "!=Null" || item.ComparisonType == "=Null")
                {
                    if (item.Join == null || string.IsNullOrEmpty(item.Join.TableJoin))
                    {
                        v_Query += Validate<T>.FormatQuery(item.ColumnSearch, item.ValueSearch.Trim(), item.TypeSearch, item.ComparisonType, typeof(T).Name);
                    }
                    else
                    {
                        v_Query += Validate<T>.FormatQuery(item.ColumnSearch, item.ValueSearch.Trim(), item.TypeSearch, item.ComparisonType, item.Join.TableJoin);
                    }
                }
            }
            return _baseDL.GetFitterRecords(v_Offset, v_Limit, v_Where, v_Sort, v_Query, string.Join(", ", v_Select));
        }

        /// <summary>
        /// Hàm thêm mới một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>ID bản ghi sau khi thêm</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual ServiceResponse InsertRecord(T record)
        {
            var validateResult = Validate<T>.ValidateData(record);
            if (!validateResult.Success)
            {
                return validateResult;
            }
            var validateCustom = CustomValidate(record);
            if (!validateCustom.Success)
            {
                return validateCustom;
            }
            var result = _baseDL.InsertRecord(record);
            if (!result.Success)
            {
                return new ServiceResponse
                {
                    Success = false,
                    ErrorCode = MisaAmisErrorCode.InsertFailed,
                    Data = result.Data == Guid.Empty.ToString() ? Resource.Message_data_change : result.Data,
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Data = result.Data,
            };
        }

        /// <summary>
        /// Hàm cập nhật thông tin một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <param name="record"></param>
        /// <returns>ID record sau khi cập nhật</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual ServiceResponse UpdateRecord(Guid recordID, T record)
        {
            var validateResult = Validate<T>.ValidateData(record);
            if (!validateResult.Success)
            {
                return validateResult;
            }
            var validateCustom = CustomValidate(record);
            if (!validateCustom.Success)
            {
                return validateCustom;
            }
            var result = _baseDL.UpdateRecord(recordID, record);
            if (!result.Success)
            {
                return new ServiceResponse
                {
                    Success = false,
                    ErrorCode = MisaAmisErrorCode.UpdateFailed,
                    Data = result.Data == Guid.Empty.ToString() ? Resource.Message_data_change : result.Data,
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Data = result.Data,
            };
        }

        /// <summary>
        /// Xoá một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>ID record sau khi xoá</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual ServiceResponse DeleteRecord(Guid recordID)
        {
            var result = _baseDL.DeleteRecord(recordID);
            if (!result.Success)
            {
                return new ServiceResponse
                {
                    Success = false,
                    ErrorCode = MisaAmisErrorCode.DeleteFailed,
                    Data = result.Data == Guid.Empty.ToString() ? Resource.Message_data_change : result.Data,
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Data = result.Data,
            };
        }

        /// <summary>
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="listRecordID">danh sách bản ghi cần xoá</param>
        /// <param name="count">Số lượng bản ghi bị xoá</param>
        /// <returns>Số kết quả bản ghi đã xoá</returns>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public virtual ServiceResponse DeleteMultiple(string listRecordID, int count)
        {
            int rowAffects = _baseDL.DeleteMultiple(listRecordID, count).Data;
            if (rowAffects == 0)
            {
                return new ServiceResponse
                {
                    Success = false,
                    ErrorCode = MisaAmisErrorCode.DeleteMultiple,
                    Data = Resource.Message_data_change
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Data = rowAffects
            };
        }

        /// <summary>
        /// Hàm cập nhật toggle active bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>ID record sau khi cập nhật</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual ServiceResponse ToggleActive(Guid recordID)
        {
            Guid result = _baseDL.ToggleActive(recordID).Data;
            if (result != Guid.Empty)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Data = result,
                };
            }
            return new ServiceResponse
            {
                Success = false,
                ErrorCode = MisaAmisErrorCode.UpdateFailed,
                Data = Resource.Message_data_change,
            };
        }

        /// <summary>
        /// Hhập khẩu dữ liệu từ tệp
        /// </summary>
        /// <param name="data">Json danh sách</param>
        /// <param name="count">Số lượng record</param>
        /// <returns></returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public bool ImportXLSX(string data, int count)
        {
            return _baseDL.ImportXLSX(data, count).Data;
        }

        /// <summary>
        /// Export data ra file excel
        /// </summary>
        /// <param name="formData">Trường muốn filter và sắp xếp</param>
        /// <returns>file Excel chứa dữ liệu danh sách </returns>
        /// CreatedBy: Nguyễn Khắc Tiềm (6/10/2022)
        public string ExportData(Dictionary<string, object> formData)
        {
            // lấy dữ liệu nhân viên
            var v_Offset = 0;
            var v_Limit = 0;
            var v_Where = formData.Keys.Contains("v_Where") ? Convert.ToString(formData["v_Where"]).Trim() : null;
            var v_Sort = formData.Keys.Contains("v_Sort") ? Convert.ToString(formData["v_Sort"]) : null;
            var v_Select = formData.Keys.Contains("v_Select") ? JsonConvert.DeserializeObject<List<string>>(Convert.ToString(formData["v_Select"])) : new List<string>();

            List<ComparisonTypeSearch> listQuery = formData.Keys.Contains("CustomSearch") ? JsonConvert.DeserializeObject<List<ComparisonTypeSearch>>(Convert.ToString(formData["CustomSearch"])) : new List<ComparisonTypeSearch>();
            string v_Query = "";
            foreach (ComparisonTypeSearch item in listQuery)
            {
                if (!string.IsNullOrEmpty(item.ValueSearch) || item.ComparisonType == "!=Null" || item.ComparisonType == "=Null")
                {
                    if (item.Join == null || string.IsNullOrEmpty(item.Join.TableJoin))
                    {
                        v_Query += Validate<T>.FormatQuery(item.ColumnSearch, item.ValueSearch.Trim(), item.TypeSearch, item.ComparisonType, typeof(T).Name);
                    }
                    else
                    {
                        v_Query += Validate<T>.FormatQuery(item.ColumnSearch, item.ValueSearch.Trim(), item.TypeSearch, item.ComparisonType, item.Join.TableJoin);
                    }
                }
            }
            List<T> records = (List<T>)_baseDL.GetFitterRecords(v_Offset, v_Limit, v_Where, v_Sort, v_Query, string.Join(", ", v_Select)).recordList;
            if (records == null || records.Count == 0 || v_Select.Count == 0) return null;
            string column = SaveFileImage.GetColumnName(v_Select.Count);
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream ?? new MemoryStream()))
            {
                // thêm 1 sheet vào file excel
                var sheet = package.Workbook.Worksheets.Add(CustomOptionExport().Header);
                // style header
                sheet.Cells[$"A1:{column}1"].Merge = true;
                sheet.Cells[$"A1:{column}1"].Value = CustomOptionExport().Header;
                sheet.Cells[$"A1:{column}1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[$"A1:{column}1"].Style.Font.Bold = true;
                sheet.Cells[$"A1:{column}1"].Style.Font.Size = 16;
                sheet.Cells[$"A1:{column}1"].Style.Font.Name = Resource.ExcelFontHeader;
                sheet.Cells[$"A2:{column}2"].Merge = true;
                sheet.Row(3).Style.Font.Name = Resource.ExcelFontHeader;
                sheet.Row(3).Style.Font.Bold = true;
                sheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Row(3).Style.Font.Size = 10;
                sheet.Cells[$"A3:{column}3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells[$"A3:{column}3"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(Resource.BackGroundColorHeaderExport));
                sheet.Cells[3, 1].Value = "STT";
                sheet.Column(1).Width = 10;
                sheet.Cells[$"A3:{column}{records.Count() + 3}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:{column}{records.Count() + 3}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:{column}{records.Count() + 3}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:{column}{records.Count() + 3}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:{column}{records.Count() + 3}"].Style.Font.Name = Resource.ExcelFontContent;
                sheet.Cells[$"A3:{column}{records.Count() + 3}"].Style.Font.Size = 11;
                // customize tên header của file excel
                // lấy các thuộc tính của nhân viên
                var properties = typeof(T).GetProperties();
                int indexHeader = 2;
                string listField = string.Join(",", v_Select);
                // Duyệt từng property
                foreach (var property in properties)
                {
                    // lấy tên hiển thị đầu tiên của thuộc tính
                    var displayNameAttributes = property.GetCustomAttributes(typeof(ColumnName), true);
                    if (listField.Contains(property.Name) && displayNameAttributes != null && displayNameAttributes.Length > 0)
                    {
                        // add vào header của file excel
                        sheet.Column(indexHeader).Width = (displayNameAttributes[0] as ColumnName).Width;
                        sheet.Cells[3, indexHeader].Value = (displayNameAttributes[0] as ColumnName).Name;
                        indexHeader++;
                    }
                }

                int index = 4;
                int indexRow = 0;
                string convertDate = "M/d/yyyy hh:mm:ss tt";
                // thêm dữ liệu vào excel (phần body)
                // duyệt các nhân viên
                foreach (var recordItem in records)
                {
                    int indexBody = 2;
                    sheet.Cells[index, 1].Value = indexRow + 1;
                    sheet.Cells[index, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    index++;
                    foreach (var property in properties)
                    {
                        var displayNameAttributes = property.GetCustomAttributes(typeof(ColumnName), true);
                        if (listField.Contains(property.Name) && displayNameAttributes != null && displayNameAttributes.Length > 0)
                        {
                            // xử lí các datetime
                            if ((displayNameAttributes[0] as ColumnName).IsDate)
                            {
                                try
                                {
                                    sheet.Cells[indexRow + 4, indexBody].Value = property.GetValue(recordItem) != null ? DateTime.ParseExact(property.GetValue(recordItem).ToString(), convertDate, CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
                                }
                                catch
                                {
                                    if (convertDate == "M/d/yyyy hh:mm:ss tt")
                                    {
                                        convertDate = "d/M/yyyy hh:mm:ss tt";
                                    }
                                    else
                                    {
                                        convertDate = "M/d/yyyy hh:mm:ss tt";
                                    }
                                    sheet.Cells[indexRow + 4, indexBody].Value = property.GetValue(recordItem) != null ? DateTime.ParseExact(property.GetValue(recordItem).ToString(), convertDate, CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
                                }
                                sheet.Cells[indexRow + 4, indexBody].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            }
                            else if ((displayNameAttributes[0] as ColumnName).IsBollen)
                            {
                                sheet.Cells[indexRow + 4, indexBody].Value = (bool)property.GetValue(recordItem) == true ? "Có" : "Không";
                                sheet.Cells[indexRow + 4, indexBody].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            }
                            else if (property.Name == "IsActive")
                            {
                                sheet.Cells[indexRow + 4, indexBody].Value = (bool)property.GetValue(recordItem) == true ? Resource.ActiveTrue : Resource.ActiveFalse;
                            }
                            // các kiểu dữ liệu khác datatime và giới tính
                            else
                            {
                                if (!CustomValuePropertieExport(property, ref sheet, indexRow, indexBody, recordItem))
                                {
                                    sheet.Cells[indexRow + 4, indexBody].Value = property.GetValue(recordItem);
                                }
                            }
                            indexBody++;
                        }
                    }
                    indexRow++;
                }
                package.Save();

                package.Stream.Position = 0;
                string execlName = $"{CustomOptionExport().FileName} ({DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}).xlsx";
                return SaveFileImage.SaveExcelFileToDisk(package.Stream, execlName);
            }
        }

        #endregion
    }
}
