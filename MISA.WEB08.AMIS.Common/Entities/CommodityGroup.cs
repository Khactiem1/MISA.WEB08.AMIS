using MISA.WEB08.AMIS.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [PrimaryKey]
        public Guid? CommodityGroupID { get; set; }

        /// <summary>
        /// Mã kho
        /// </summary>
        [Unique("Nhóm vật tư <{0}> đã tồn tại trong hệ thống.")]
        [IsNotNullOrEmpty("Mã Nhóm vật tư không được để trống.")]
        [ColumnName("Mã Nhóm vật tư", 35, false, false)]
        public string CommodityCode { get; set; }

        /// <summary>
        /// Tên kho
        /// </summary>
        [IsNotNullOrEmpty("Tên nhóm vật tư không được để trống.")]
        [ColumnName("Tên nhóm vật tư", 50, false, false)]
        public string CommodityName { get; set; }

        /// <summary>
        /// Nhóm vật tư cha
        /// </summary>
        public string ParentID { get; set; } = "0";

        /// <summary>
        /// Hoạt động hay không hoạt động
        /// </summary>
        [ColumnName("Trạng thái", 20, false, false)]
        public bool IsActive { get; set; }
    }
}
