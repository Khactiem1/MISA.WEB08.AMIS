using MISA.WEB08.AMIS.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
