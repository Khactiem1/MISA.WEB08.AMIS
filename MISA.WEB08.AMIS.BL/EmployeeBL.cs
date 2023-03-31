using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.DL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using MISA.WEB08.AMIS.Common.Resources;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Result;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// Hàm xử lý custom override validate model employee
        /// </summary>
        /// <param name="employee">Record cần custom validate</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override ServiceResponse? CustomValidate(Employee employee)
        {
            var validateFailures = "";
            ///
            ///Code logic validate custom
            ///
            if(!string.IsNullOrEmpty(validateFailures))
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
        /// Hàm xử lý custom tham số cho bản ghi cần validate
        /// </summary>
        /// <param name="record">Record cần custom validate</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override void CustomParameterValidate(ref Employee record)
        {
            record.BranchID = Guid.NewGuid();
        }

        /// <summary>
        /// Hàm xử lý custom kết quả validate cho bản ghi cần validate
        /// </summary>
        /// <param name="record">Record cần custom validate</param>
        /// <param name="line">Line nhập khẩu</param>
        /// <param name="errorDetail">Lỗi chi tiết khi nhập</param>
        /// <param name="status">Trạng thái nhập khẩu</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public override void CustomResultValidate(ref Employee record, int line, string? errorDetail, string? status)
        {
            record.EmployeeID = Guid.NewGuid();
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
        public override ServiceResponse CustomValidateImportXlsx(Employee record, List<Employee> listRecord)
        {
            var validateFailures = "";
            int count = listRecord.Count(e => e.EmployeeCode == record.EmployeeCode);
            if (count >= 2)
            {
                // Có ít nhất 2 bản ghi trong danh sách "DSEmployee" có EmployeeCode trùng với EmployeeCode của đối tượng "employee"
                validateFailures = $"validate.unique_import MESSAGE.VALID.SPLIT EmployeeCode MESSAGE.VALID.SPLIT {record.EmployeeCode}";
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
        /// Hàm custom dữ liệu xuất file
        /// </summary>
        /// <param name="property">Cột dữ liệu cần custom</param>
        /// <returns>Trả ra false khi không custom gì</returns>
        ///  NK Tiềm 05/10/2022
        public override bool CustomValuePropertieExport(PropertyInfo property, ref ExcelWorksheet sheet, int indexRow, int indexBody, Employee employee)
        {
            if(property.Name == "Gender")
            {
                sheet.Cells[indexRow + 4, indexBody].Value = employee.Gender == Gender.Male ? Resource.GenderMale : employee.Gender == Gender.Female ? Resource.GenderFemale : Resource.GenderOther;
                sheet.Cells[indexRow + 4, indexBody].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
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
                FileName = "Danh sách nhân viên",
                Header = "DANH SÁCH NHÂN VIÊN"
            };
        }

        #endregion
    }
}
