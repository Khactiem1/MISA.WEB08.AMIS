using MISA.WEB08.AMIS.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// đơn vị ứng với bảng UnitCalculation trong database
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm 21.09.2022
    public class UnitCalculation : BaseEntity
    {
        /// <summary>
        /// id đơn vị tính
        /// </summary>
        [PrimaryKey]
        public Guid? UnitCalculationID { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        [Unique("Đơn vị <{0}> đã tồn tại trong hệ thống.")]
        [IsNotNullOrEmpty("Đơn vị không được để trống.")]
        [ColumnName("Đơn vị", 16, false, false, false)]
        public string UnitCalculationName { get; set; }

        /// <summary>
        /// Mô tả đơn vị tính
        /// </summary>
        [ColumnName("Mô tả", 16, false, false, false)]
        public string UnitCalculationDetail { get; set; }

        /// <summary>
        /// Hoạt động hay không hoạt động
        /// </summary>
        [ColumnName("Trạng thái", 16, false, false, false)]
        public bool IsActive { get; set; }
    }
}
