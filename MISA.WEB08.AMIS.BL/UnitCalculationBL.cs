using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Resources;
using MISA.WEB08.AMIS.Common.Result;
using MISA.WEB08.AMIS.DL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng UnitCalculation từ tầng BL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class UnitCalculationBL : BaseBL<UnitCalculation>, IUnitCalculationBL
    {
        #region Field

        private IUnitCalculationDL _unitCalculationDL;

        #endregion

        #region Contructor

        public UnitCalculationBL(IUnitCalculationDL unitCalculationDL) : base(unitCalculationDL)
        {
            _unitCalculationDL = unitCalculationDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm Lấy danh sách bản ghi nhân viên theo từ khoá tìm kiếm không phân trang
        /// <param name="keyword">Tìm kiếm </param>
        /// <param name="sort">Sắp xếp</param>
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Stream GetExport(string? keyword, string? sort)
        {
            // lấy dữ liệu nhân viên
            List<UnitCalculation> employees = (List<UnitCalculation>)_unitCalculationDL.GetsDataExport(keyword, sort);
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream ?? new MemoryStream()))
            {
                // thêm 1 sheet vào file excel
                var sheet = package.Workbook.Worksheets.Add("DANH SÁCH ĐƠN VỊ TÍNH");
                // style header
                sheet.Cells["A1:D1"].Merge = true;
                sheet.Cells["A1:D1"].Value = "DANH SÁCH ĐƠN VỊ TÍNH";
                sheet.Cells["A1:D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:D1"].Style.Font.Bold = true;
                sheet.Cells["A1:D1"].Style.Font.Size = 16;
                sheet.Cells["A1:D1"].Style.Font.Name = Resource.ExcelFontHeader;
                sheet.Cells["A2:D2"].Merge = true;
                sheet.Row(3).Style.Font.Name = Resource.ExcelFontHeader;
                sheet.Row(3).Style.Font.Bold = true;
                sheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Row(3).Style.Font.Size = 10;
                sheet.Cells["A3:D3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells["A3:D3"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(Resource.BackGroundColorHeaderExport));
                sheet.Cells[3, 1].Value = "STT";
                sheet.Column(1).Width = 10;
                sheet.Cells[$"A3:D{employees.Count() + 3}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:D{employees.Count() + 3}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:D{employees.Count() + 3}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:D{employees.Count() + 3}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:D{employees.Count() + 3}"].Style.Font.Name = Resource.ExcelFontContent;
                sheet.Cells[$"A3:D{employees.Count() + 3}"].Style.Font.Size = 11;
                // customize tên header của file excel
                var employee = new UnitCalculation();
                // lấy các thuộc tính của nhân viên
                var properties = employee.GetType().GetProperties();
                int indexHeader = 2;
                // Duyệt từng property
                foreach (var property in properties)
                {
                    // lấy tên hiển thị đầu tiên của thuộc tính
                    var displayNameAttributes = property.GetCustomAttributes(typeof(ColumnName), true);
                    if (displayNameAttributes != null && displayNameAttributes.Length > 0)
                    {
                        // add vào header của file excel
                        sheet.Column(indexHeader).Width = (displayNameAttributes[0] as ColumnName).Width;
                        sheet.Cells[3, indexHeader].Value = (displayNameAttributes[0] as ColumnName).Name;
                        indexHeader++;
                    }
                }

                int index = 4;
                int indexRow = 0;
                // thêm dữ liệu vào excel (phần body)
                // duyệt các nhân viên
                foreach (var employeeItem in employees)
                {
                    int indexBody = 2;
                    sheet.Cells[index, 1].Value = indexRow + 1;
                    sheet.Cells[index, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    index++;
                    foreach (var property in properties)
                    {
                        var displayNameAttributes = property.GetCustomAttributes(typeof(ColumnName), true);
                        if (displayNameAttributes != null && displayNameAttributes.Length > 0)
                        {

                            // xử lí các datetime ở ngày nhân viên
                            if ((displayNameAttributes[0] as ColumnName).Name == "Trạng thái")
                            {
                                sheet.Cells[indexRow + 4, indexBody].Value = (bool)property.GetValue(employeeItem) == true ? Resource.ActiveTrue : Resource.ActiveFalse;
                            }
                            else
                            {
                                sheet.Cells[indexRow + 4, indexBody].Value = property.GetValue(employeeItem);
                            }
                            indexBody++;
                        }
                    }
                    indexRow++;
                }
                package.Save();
                return package.Stream;
            }
        }

        /// <summary>
        /// Hàm xử lý kiểm tra phát sinh
        /// </summary>
        /// <param name="record">Record cần custom validate</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override ServiceResponse CheckIncurred(Guid record)
        {
            var validateFailures = new List<string>();
            if (!_unitCalculationDL.CheckIncurred("InventoryItem", "UnitCalculationID", record.ToString()))
            {
                validateFailures.Add(string.Format(Resource.UserMsg_CheckIncurred, "danh sách hàng hóa, dịch vụ"));
            }
            if (validateFailures.Count > 0)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = validateFailures,
                    ErrorCode = MisaAmisErrorCode.Incurred
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

        #endregion
    }
}