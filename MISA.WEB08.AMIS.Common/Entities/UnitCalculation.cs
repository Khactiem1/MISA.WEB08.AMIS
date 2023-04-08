using MISA.WEB08.AMIS.Common.Attributes;
using System;
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
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "validate.empty", MaxLength = 100)]
        [ColumnName(Name = "Đơn vị", Width = 16)]
        public string UnitCalculationName { get; set; }

        /// <summary>
        /// Mô tả đơn vị tính
        /// </summary>
        [Validate(MaxLength = 255)]
        [ColumnName(Name = "Mô tả", Width = 25)]
        public string UnitCalculationDetail { get; set; }

        /// <summary>
        /// Hoạt động hay không hoạt động
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "validate.empty")]
        [ColumnName(Name = "Trạng thái", Width = 20)]
        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// đơn vị ứng với bảng UnitCalculation trong database Import
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm 21.09.2022
    public class UnitCalculationImport : BaseEntity
    {
        /// <summary>
        /// id đơn vị tính
        /// </summary>
        public string? UnitCalculationID { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        public string UnitCalculationName { get; set; }

        /// <summary>
        /// Mô tả đơn vị tính
        /// </summary>
        public string UnitCalculationDetail { get; set; }

        /// <summary>
        /// Hoạt động hay không hoạt động
        /// </summary>
        [ValidateString(IsBoolean = true)]
        public string? IsActive { get; set; }
    }
}
