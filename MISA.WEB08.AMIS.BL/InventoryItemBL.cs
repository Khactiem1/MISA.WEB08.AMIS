using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resources;
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
        /// Hàm Lấy danh sách bản ghi nhân viên theo từ khoá tìm kiếm không phân trang
        /// <param name="keyword">Tìm kiếm </param>
        /// <param name="sort">Sắp xếp</param>
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Stream GetExport(string? keyword, string? sort)
        {
            // lấy dữ liệu nhân viên
            List<InventoryItem> employees = (List<InventoryItem>)_inventoryItemBL.GetsDataExport(keyword, sort);
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream ?? new MemoryStream()))
            {
                // thêm 1 sheet vào file excel
                var sheet = package.Workbook.Worksheets.Add("DANH SÁCH VẬT TƯ HÀNG HOÁ DỊCH VỤ");
                // style header
                sheet.Cells["A1:AD1"].Merge = true;
                sheet.Cells["A1:AD1"].Value = "DANH SÁCH VẬT TƯ HÀNG HOÁ DỊCH VỤ";
                sheet.Cells["A1:AD1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:AD1"].Style.Font.Bold = true;
                sheet.Cells["A1:AD1"].Style.Font.Size = 16;
                sheet.Cells["A1:AD1"].Style.Font.Name = Resource.ExcelFontHeader;
                sheet.Cells["A2:AD2"].Merge = true;
                sheet.Row(3).Style.Font.Name = Resource.ExcelFontHeader;
                sheet.Row(3).Style.Font.Bold = true;
                sheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Row(3).Style.Font.Size = 10;
                sheet.Cells["A3:AD3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells["A3:AD3"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(Resource.BackGroundColorHeaderExport));
                sheet.Cells[3, 1].Value = "STT";
                sheet.Column(1).Width = 10;
                sheet.Cells[$"A3:AD{employees.Count() + 3}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:AD{employees.Count() + 3}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:AD{employees.Count() + 3}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:AD{employees.Count() + 3}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:AD{employees.Count() + 3}"].Style.Font.Name = Resource.ExcelFontContent;
                sheet.Cells[$"A3:AD{employees.Count() + 3}"].Style.Font.Size = 11;
                // customize tên header của file excel
                var employee = new InventoryItem();
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
                            else if ((displayNameAttributes[0] as ColumnName).Name == "Tính chất")
                            {
                                int temp = (int)property.GetValue(employeeItem);
                                sheet.Cells[indexRow + 4, indexBody].Value = temp == 1 ? "Hàng hoá" : temp == 2 ? "Dịch vụ" : temp == 3 ? "Nguyên vật liệu" : temp == 4 ? "Thành phẩm" : "Dụng cụ công cụ";
                            }
                            else
                            {
                                if ((displayNameAttributes[0] as ColumnName).IsNumber && !string.IsNullOrEmpty(Convert.ToString(property.GetValue(employeeItem))))
                                {
                                    sheet.Cells[indexRow + 4, indexBody].Value = Validate<InventoryItem>.FormatNumber(double.Parse(property.GetValue(employeeItem) + ""));
                                    sheet.Cells[indexRow + 4, indexBody].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                }
                                else
                                {
                                    sheet.Cells[indexRow + 4, indexBody].Value = property.GetValue(employeeItem);
                                }
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
        /// Hàm lấy ra tổng số lượng hàng sắp hết và hết hàng
        /// </summary>
        /// <returns></returns>
        //// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetInventoryStatus()
        {
            return _inventoryItemBL.GetInventoryStatus();
        }

        /// <summary>
        /// Hàm xử lý lưu hình ảnh
        /// <param name="record">Record cần custom </param>
        /// </summary>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override void SaveImage(ref InventoryItem record)
        {
            if (!string.IsNullOrEmpty(record.Avatar))
            {
                if (!record.Avatar.Contains("/Assets/Images/"))
                {
                    string path = SaveFileImage.Post(record.Avatar, "InventoryItemImages");
                    record.Avatar = path;
                }
            }
        }

        /// <summary>
        /// Hàm xử lý lưu mã để tự sinh
        /// </summary>
        /// <param name="record">Bản ghi</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override void SaveCode(InventoryItem record)
        {
            string prefix = "";
            string number = "";
            string last = "";
            for (int i = 0; i < record.InventoryItemCode.Length; i++)
            {
                char[] keyNumber = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'};
                char temp = record.InventoryItemCode[i];
                if ((keyNumber.Contains(temp)) && last == "")
                {
                    if (number == "" && temp == '0')
                    {
                        prefix += temp;
                    }
                    else
                    {
                        number += temp;
                    }
                }
                else
                {
                    if (number != "")
                    {
                        last += temp;
                    }
                    else
                    {
                        prefix += temp; 
                    }
                }
            }
            if (number == "")
            {
                number = "0";
            }
            _inventoryItemBL.SaveCode(prefix, number, last);
        }
        #endregion
    }
}
