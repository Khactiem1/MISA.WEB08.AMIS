using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Dữ liệu tìm kiếm với các cài đặt
    /// </summary>
    public class ComparisonTypeSearch
    {
        /// <summary>
        /// Kiểu dữ liệu
        /// </summary>
        public string? typeSearch { get; set; }

        /// <summary>
        /// Value tìm kiếm
        /// </summary>
        public string? valueSearch { get; set; }

        /// <summary>
        /// Tên cột tìm kiếm
        /// </summary>
        public string columnSearch { get; set; }

        /// <summary>
        /// Kiểu tìm kiếm
        /// </summary>
        public string? comparisonType { get; set; }
    }
}
