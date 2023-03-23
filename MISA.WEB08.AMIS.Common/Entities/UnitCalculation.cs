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
        [Validate(PrimaryKey = true)]
        public Guid? UnitCalculationID { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "Đơn vị không được để trống.")]
        [ColumnName(Name = "Đơn vị", Width = 16)]
        public string UnitCalculationName { get; set; }

        /// <summary>
        /// Mô tả đơn vị tính
        /// </summary>
        [ColumnName(Name = "Mô tả", Width = 25)]
        public string UnitCalculationDetail { get; set; }

        /// <summary>
        /// Hoạt động hay không hoạt động
        /// </summary>
        [ColumnName(Name = "Trạng thái", Width = 20)]
        public bool IsActive { get; set; }
    }
}
