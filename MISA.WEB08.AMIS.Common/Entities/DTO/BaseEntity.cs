using MISA.WEB08.AMIS.Common.Resources;
using System;

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Base của các class model mà model của bảng trong Database nào cũng thường có
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class BaseEntity
    {
        /// <summary>
        /// Người thêm
        /// </summary>
        public string? CreatedBy { get; set; } = Resource.DefaultUser;

        /// <summary>
        /// ngày thêm
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// người sửa
        /// </summary>
        public string? ModifiedBy { get; set; } = Resource.DefaultUser;

        /// <summary>
        /// ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Vị trí cột khi nhập khẩu
        /// </summary>
        public int? LineExcel { get; set; }

        /// <summary>
        /// Trạng thái nhập khẩu
        /// </summary>
        public string? StatusImportExcel { get; set; }

        /// <summary>
        /// Chi tiết lỗi khi nhập từ excel
        /// </summary>
        public string? ErrorDetail { get; set; }
    }
}