using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.API.Entities
{
    /// <summary>
    /// Đơn vị ứng với bảng unit trong database
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm 21.09.2022
    public class Unit
    {
        /// <summary>
        /// id đơn vị
        /// </summary>
        public Guid unit_id { get; set; }

        /// <summary>
        /// mã đơn vị
        /// </summary>
        public string unit_code { get; set; }

        /// <summary>
        /// tên đơn vị
        /// </summary>
        public string unit_name { get; set; } 

        /// <summary>
        /// Người thêm
        /// </summary>
        public string created_by { get; set; }

        /// <summary>
        /// ngày thêm
        /// </summary>
        public DateTime created_date { get; set; }

        /// <summary>
        /// người sửa
        /// </summary>
        public string modified_by { get; set; }

        /// <summary>
        /// ngày sửa
        /// </summary>
        public DateTime modified_date { get; set; }
    }
}
