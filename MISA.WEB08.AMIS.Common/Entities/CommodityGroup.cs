using MISA.WEB08.AMIS.Common.Attributes;
using System;

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Nhóm vật liệu ứng với bảng CommodityGroup trong database
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm 21.09.2022
    public class CommodityGroup : BaseEntity
    {
        /// <summary>
        /// id nhóm vật liệu
        /// </summary>
        [Validate(PrimaryKey = true)]
        public Guid? CommodityGroupID { get; set; }

        /// <summary>
        /// Mã kho
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "validate.empty", MaxLength = 25)]
        [ColumnName(Name = "Mã Nhóm vật tư", Width = 35)]
        public string CommodityCode { get; set; }

        /// <summary>
        /// Tên kho
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "validate.empty", MaxLength = 100)]
        [ColumnName(Name = "Tên nhóm vật tư", Width = 50)]
        public string CommodityName { get; set; }

        /// <summary>
        /// Nhóm vật tư cha
        /// </summary>
        public string ParentID { get; set; } = "0";

        /// <summary>
        /// Hoạt động hay không hoạt động
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "validate.empty")]
        [ColumnName(Name = "Trạng thái", Width = 20)]
        public bool? IsActive { get; set; }
    }
}
