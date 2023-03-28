using MISA.WEB08.AMIS.Common.Result;
using OfficeOpenXml;
using System.Reflection;

namespace MISA.WEB08.AMIS.BL
{
    public class CustomVirtual<T>
    {
        #region Method

        /// <summary>
        /// Hàm xử lý custom validate những model riêng biệt
        /// </summary>
        /// <param name="record">Record cần custom validate</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public virtual ServiceResponse CustomValidate(T record)
        {
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
        public virtual bool CustomValuePropertieExport(PropertyInfo property, ref ExcelWorksheet sheet, int indexRow, int indexBody, T record)
        {
            return false;
        }

        /// <summary>
        /// Hàm custom dữ liệu tên file, header, ... khi xuất file
        /// </summary>
        /// <returns></returns>
        ///  NK Tiềm 05/10/2022
        public virtual OptionExport CustomOptionExport()
        {
            return new OptionExport
            {
                FileName = "Danh sách",
                Header = "DANH SÁCH"
            };
        }

        #endregion
    }
}
