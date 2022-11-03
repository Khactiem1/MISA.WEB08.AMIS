using MISA.WEB08.AMIS.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Nhà kho ứng với bảng depot trong database
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm 21.09.2022
    public class Depot : BaseEntity
    {
        /// <summary>
        /// id kho
        /// </summary>
        [PrimaryKey]
        public Guid? DepotID { get; set; }

        /// <summary>
        /// Mã kho
        /// </summary>
        [Unique("Mã kho <{0}> đã tồn tại trong hệ thống.")]
        [IsNotNullOrEmpty("Mã kho không được để trống.")]
        [ColumnName("Mã kho", 30, false, false)]
        public string DepotCode { get; set; }

        /// <summary>
        /// Tên kho
        /// </summary>
        [IsNotNullOrEmpty("Tên kho không được để trống.")]
        [ColumnName("Tên kho", 50, false, false)]
        public string DepotName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [ColumnName("Địa chỉ", 100, false, false)]
        public string DepotDelivery { get; set; }

        /// <summary>
        /// Hoạt động hay không hoạt động
        /// </summary>
        [ColumnName("Trạng thái", 20, false, false)]
        public bool IsActive { get; set; }
    }
}
