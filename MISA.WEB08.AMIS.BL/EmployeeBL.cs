using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Globalization;
using System.Drawing;
using System.Text;
using MISA.WEB08.AMIS.Common.Resources;
using System.Threading.Tasks;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Result;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng employee từ tầng BL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Contructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
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
        public Stream GetEmployeeExport(string? keyword, string? sort)
        {
            // lấy dữ liệu nhân viên
            List<Employee> employees = (List<Employee>)_employeeDL.GetsDataExport(keyword, sort);
            if(employees == null || employees.Count == 0) return null;
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream ?? new MemoryStream()))
            {
                // thêm 1 sheet vào file excel
                var sheet = package.Workbook.Worksheets.Add(Resource.TitleFileExportEmployee);
                // style header
                sheet.Cells["A1:R1"].Merge = true;
                sheet.Cells["A1:R1"].Value = Resource.TitleFileExportEmployee;
                sheet.Cells["A1:R1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:R1"].Style.Font.Bold = true;
                sheet.Cells["A1:R1"].Style.Font.Size = 16;
                sheet.Cells["A1:R1"].Style.Font.Name = Resource.ExcelFontHeader;
                sheet.Cells["A2:R2"].Merge = true;
                sheet.Row(3).Style.Font.Name = Resource.ExcelFontHeader;
                sheet.Row(3).Style.Font.Bold = true;
                sheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Row(3).Style.Font.Size = 10;
                sheet.Cells["A3:R3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells["A3:R3"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(Resource.BackGroundColorHeaderExport));
                sheet.Cells[3, 1].Value = "STT";
                sheet.Column(1).Width = 10;
                sheet.Cells[$"A3:R{employees.Count() + 3}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:R{employees.Count() + 3}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:R{employees.Count() + 3}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:R{employees.Count() + 3}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet.Cells[$"A3:R{employees.Count() + 3}"].Style.Font.Name = Resource.ExcelFontContent;
                sheet.Cells[$"A3:R{employees.Count() + 3}"].Style.Font.Size = 11;
                // customize tên header của file excel
                var employee = new Employee();
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
                foreach(var employeeItem in employees)
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
                            if ((displayNameAttributes[0] as ColumnName).IsDate)
                            {
                                sheet.Cells[indexRow + 4, indexBody].Value = property.GetValue(employeeItem) != null ? DateTime.ParseExact(property.GetValue(employeeItem).ToString(), "d/M/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
                                sheet.Cells[indexRow + 4, indexBody].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            }
                            else if ((displayNameAttributes[0] as ColumnName).IsGender)
                            {
                                sheet.Cells[indexRow + 4, indexBody].Value = (Gender)property.GetValue(employeeItem) == Gender.Male ? Resource.GenderMale : (Gender)property.GetValue(employeeItem) == Gender.Female ? Resource.GenderFemale : Resource.GenderOther;
                            }
                            else if ((displayNameAttributes[0] as ColumnName).Name == "Trạng thái")
                            {
                                sheet.Cells[indexRow + 4, indexBody].Value = (bool)property.GetValue(employeeItem) == true ? Resource.ActiveTrue : Resource.ActiveFalse;
                            }
                            // các kiểu dữ liệu khác datatime và giới tính
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
        /// Hàm xử lý custom override validate model employee
        /// </summary>
        /// <param name="employee">Record cần custom validate</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override ServiceResponse? CustomValidate(Employee employee)
        {
            var validateFailures = new List<string>();
            if (employee.EmployeeName == "Khắc Tiềm")
            {
                validateFailures.Add("Trường tên không thể là Khắc Tiềm");
            }
            if(validateFailures.Count > 0)
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
        /// Hàm xử lý lưu mã để tự sinh
        /// </summary>
        /// <param name="record">Bản ghi</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override void SaveCode(Employee record)
        {
            string prefix = "";
            string number = "";
            string last = "";
            for (int i = 0; i < record.EmployeeCode.Length; i++)
            {
                char[] keyNumber = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
                char temp = record.EmployeeCode[i];
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
            _employeeDL.SaveCode(prefix, number, last);
        }

        #endregion
    }
}
