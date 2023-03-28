using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.DL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using MISA.WEB08.AMIS.Common.Resources;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Result;
using System.Reflection;

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
