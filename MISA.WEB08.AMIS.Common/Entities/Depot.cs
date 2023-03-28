using MISA.WEB08.AMIS.Common.Attributes;
using System;

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
        [Validate(PrimaryKey = true)]
        public Guid? DepotID { get; set; }

        /// <summary>
        /// Mã kho
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "validate.empty")]
        [ColumnName(Name = "Mã kho", Width = 30)]
        public string DepotCode { get; set; }

        /// <summary>
        /// Tên kho
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "validate.empty")]
        [ColumnName(Name = "Tên kho", Width = 50)]
        public string DepotName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [ColumnName(Name = "Địa chỉ", Width = 100)]
        public string DepotDelivery { get; set; }

        /// <summary>
        /// Hoạt động hay không hoạt động
        /// </summary>
        [ColumnName(Name = "Trạng thái", Width = 20)]
        public bool? IsActive { get; set; }
    }
}
