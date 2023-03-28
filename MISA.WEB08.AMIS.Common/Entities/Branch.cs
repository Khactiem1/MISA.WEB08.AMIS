using MISA.WEB08.AMIS.Common.Attributes;
using System;

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Đơn vị ứng với bảng unit trong database
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm 21.09.2022
    public class Branch : BaseEntity
    {
        /// <summary>
        /// id đơn vị
        /// </summary>
        [Validate(PrimaryKey = true)]
        public Guid? BranchID { get; set; }

        /// <summary>
        /// mã đơn vị
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "validate.empty")]
        [ColumnName(Name = "Mã đơn vị", Width = 16)]
        public string BranchCode { get; set; }

        /// <summary>
        /// tên đơn vị
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "validate.empty")]
        [ColumnName(Name = "Tên đơn vị", Width = 40)]
        public string BranchName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        [ColumnName(Name = "Mô tả", Width = 25)]
        public string Description { get; set; }

        /// <summary>
        /// Hoạt động hay không hoạt động
        /// </summary>
        [ColumnName(Name = "Trạng thái", Width = 20)]
        public bool? IsActive { get; set; }
    }
}