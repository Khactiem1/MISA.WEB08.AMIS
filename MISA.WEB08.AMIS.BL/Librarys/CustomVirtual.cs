using MISA.WEB08.AMIS.Common.Result;
using OfficeOpenXml;
using System.Collections.Generic;
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
        /// Hàm xử lý custom tham số cho bản ghi cần validate
        /// </summary>
        /// <param name="record">Record cần custom validate</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public virtual void CustomParameterValidate(ref T record)
        {

        }

        /// <summary>
        /// Hàm xử lý custom validate đối với nhập từ tệp
        /// </summary>
        /// <param name="listRecord">Danh sách từ tệp</param>
        /// <param name="record">Record cần custom validate</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public virtual ServiceResponse CustomValidateImportXlsx(T record, List<T> listRecord)
        {
            return new ServiceResponse
            {
                Success = true
            };
        }

        /// <summary>
        /// Hàm xử lý custom kết quả validate cho bản ghi cần validate
        /// </summary>
        /// <param name="record">Record cần custom validate</param>
        /// <param name="line">Line nhập khẩu</param>
        /// <param name="errorDetail">Lỗi chi tiết khi nhập</param>
        /// <param name="status">Trạng thái nhập khẩu</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public virtual void CustomResultValidate(ref T record, int line, string? errorDetail, string? status)
        {

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
