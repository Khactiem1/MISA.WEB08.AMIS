using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Enums;
using System;
using System.Collections.Generic;

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Vật tư hàng hoá ứng với bảng InventoryItem trong database
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm 21.09.2022
    public class InventoryItem : BaseEntity
    {
        /// <summary>
        /// id hàng hoá
        /// </summary>
        [Validate(PrimaryKey = true)]
        public Guid? InventoryItemID { get; set; }

        /// <summary>
        /// id đơn vị tính
        /// </summary>
        public Guid? UnitCalculationID { get; set; }

        /// <summary>
        /// mã hàng hoá vật tư
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "validate.empty", MaxLength = 25)]
        [ColumnName(Name = "Mã", Width = 16)]
        public string InventoryItemCode { get; set; }

        /// <summary>
        /// tên hàng hoá vật tư
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "validate.empty", MaxLength = 255)]
        [ColumnName(Name = "Tên hàng hoá vật tư", Width = 40)]
        public string InventoryItemName { get; set; }

        /// <summary>
        /// ID nhóm vật tư hàng hoá
        /// </summary>
        [Validate(NotMapParameterProc = true)]
        public List<Guid> CommodityGroupID { get; set; } = new List<Guid>();

        /// <summary>
        /// Mã nhóm vật tư hàng hoá
        /// </summary>
        [ColumnName(Name = "Mã nhóm vật tư hàng hoá", Width = 50)]
        public string CommodityCode { get; set; }

        /// <summary>
        /// tính chất
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "validate.empty")]
        [ColumnName(Name = "Tính chất", Width = 40)]
        public Nature? Nature { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        [ColumnName(Name = "Tên đơn vị tính", Width = 40)]
        public string UnitCalculationName { get; set; }

        /// <summary>
        /// Giảm thuế
        /// </summary>
        [Validate(MaxLength = 255)]
        [ColumnName(Name = "Giảm thuế", Width = 40)]
        public DepreciatedTax? DepreciatedTax { get; set; }

        /// <summary>
        /// Thời hạn bảo hành
        /// </summary>
        [Validate(MaxLength = 100)]
        [ColumnName(Name = "Thời hạn bảo hành", Width = 40)]
        public string WarrantyPeriod { get; set; }

        /// <summary>
        /// Số lượng tồn tối thiểu
        /// </summary>
        [ColumnName(Name = "Số lượng tồn tối thiểu", Width = 40, IsNumber = true)]
        public double? MinimumStock { get; set; }

        /// <summary>
        /// Số lượng tồn
        /// </summary>
        [ColumnName(Name = "Số lượng tồn", Width = 40, IsNumber = true)]
        public double? QuantityTock { get; set; }

        /// <summary>
        /// Nguồn gốc
        /// </summary>
        [Validate(MaxLength = 255)]
        [ColumnName(Name = "Nguồn gốc", Width = 40)]
        public string Origin { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        [Validate(MaxLength = 255)]
        [ColumnName(Name = "Mô tả", Width = 40)]
        public string Description { get; set; }

        /// <summary>
        /// Hình ảnh
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// Diễn giải khi mua
        /// </summary>
        [Validate(MaxLength = 255)]
        [ColumnName(Name = "Diễn giải khi mua", Width = 40)]
        public string ExplanationBuy { get; set; }

        /// <summary>
        /// Diễn giải khi bán
        /// </summary>
        [Validate(MaxLength = 255)]
        [ColumnName(Name = "Diễn giải khi bán", Width = 40)]
        public string ExplanationSell { get; set; }

        /// <summary>
        /// ID kho
        /// </summary>
        public Guid? DepotID { get; set; }

        /// <summary>
        /// Mã nhà kho
        /// </summary>
        [ColumnName(Name = "Mã nhà kho", Width = 40)]
        public string? DepotCode { get; set; }

        /// <summary>
        /// Tên nhà kho
        /// </summary>
        [ColumnName(Name = "Tên nhà kho", Width = 40)]
        public string? DepotName { get; set; }

        /// <summary>
        /// Tài khoản kho
        /// </summary>
        [Validate(MaxLength = 100)]
        [ColumnName(Name = "Tài khoản kho", Width = 40)]
        public string AccountDepot { get; set; }

        /// <summary>
        /// Tài khoản doanh thu
        /// </summary>
        [Validate(MaxLength = 100)]
        [ColumnName(Name = "Tài khoản doanh thu", Width = 40)]
        public string AccountRevenue { get; set; }

        /// <summary>
        /// Tài khoản chiết khấu
        /// </summary>
        [Validate(MaxLength = 100)]
        [ColumnName(Name = "Tài khoản chiết khấu", Width = 40)]
        public string AccountDiscount { get; set; }

        /// <summary>
        /// Tài khoản giảm giá
        /// </summary>
        [Validate(MaxLength = 100)]
        [ColumnName(Name = "Tài khoản giảm giá", Width = 40)]
        public string AccountSale { get; set; }

        /// <summary>
        /// Tài khoản trả lại
        /// </summary>
        [Validate(MaxLength = 100)]
        [ColumnName(Name = "Tài khoản trả lại", Width = 40)]
        public string AccountReturn { get; set; }

        /// <summary>
        /// Tài khoản chi phí
        /// </summary>
        [Validate(MaxLength = 100)]
        [ColumnName(Name = "Tài khoản chi phí", Width = 40)]
        public string AccountCost { get; set; }

        /// <summary>
        /// Tỉ lệ chiết khấu khuyến mại
        /// </summary>
        [ColumnName(Name = "Tỉ lệ chiết khấu khuyến mại", Width = 40, IsNumber = true)]
        public float? RatioDiscount { get; set; }

        /// <summary>
        /// Thuế giá trị gia tăng
        /// </summary>
        [ColumnName(Name = "Thuế giá trị gia tăng", Width = 40, IsNumber = true)]
        public float? VATTax { get; set; }

        /// <summary>
        /// Thuế nhập khẩu
        /// </summary>
        [ColumnName(Name = "Thuế nhập khẩu", Width = 40, IsNumber = true)]
        public float? VATImport { get; set; }

        /// <summary>
        /// Thuế xuất khẩu
        /// </summary>
        [ColumnName(Name = "Thuế xuất khẩu", Width = 40, IsNumber = true)]
        public float? VATExport { get; set; }

        /// <summary>
        /// Nhóm hàng hoá dịch vụ chịu thuế tiêu thụ đặc biệt
        /// </summary>
        [Validate(MaxLength = 255)]
        [ColumnName(Name = "Nhóm hàng hoá dịch vụ chịu thuế tiêu thụ đặc biệt", Width = 50)]
        public string VATGroupExciceTax { get; set; }

        /// <summary>
        /// Đơn mua cố định
        /// </summary>
        [ColumnName(Name = "Đơn mua cố định", Width = 40, IsNumber = true)]
        public double? OrderFix { get; set; }

        /// <summary>
        /// Đơn mua gần nhất
        /// </summary>
        [ColumnName(Name = "Đơn mua gần nhất", Width = 40, IsNumber = true)]
        public double? OrderNearest { get; set; }

        /// <summary>
        /// Đơn giá bán
        /// </summary>
        [ColumnName(Name = "Đơn giá bán", Width = 40, IsNumber = true)]
        public double? OrderSell { get; set; }

        /// <summary>
        /// Hoạt động hay không hoạt động
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "validate.empty")]
        [ColumnName(Name = "Trạng thái", Width = 20)]
        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// Vật tư hàng hoá ứng với bảng InventoryItem trong database Import
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm 21.09.2022
    public class InventoryItemImport : BaseEntity
    {
        /// <summary>
        /// id hàng hoá
        /// </summary>
        public string? InventoryItemID { get; set; }

        /// <summary>
        /// id đơn vị tính
        /// </summary>
        public string? UnitCalculationID { get; set; }

        /// <summary>
        /// mã hàng hoá vật tư
        /// </summary>
        public string InventoryItemCode { get; set; }

        /// <summary>
        /// tên hàng hoá vật tư
        /// </summary>
        public string InventoryItemName { get; set; }

        /// <summary>
        /// ID nhóm vật tư hàng hoá
        /// </summary>
        public List<string> CommodityGroupID { get; set; } = new List<string>();

        /// <summary>
        /// Mã nhóm vật tư hàng hoá
        /// </summary>
        public string CommodityCode { get; set; }

        /// <summary>
        /// tính chất
        /// </summary>
        public string? Nature { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        public string UnitCalculationName { get; set; }

        /// <summary>
        /// Giảm thuế
        /// </summary>
        public string? DepreciatedTax { get; set; }

        /// <summary>
        /// Thời hạn bảo hành
        /// </summary>
        public string WarrantyPeriod { get; set; }

        /// <summary>
        /// Số lượng tồn tối thiểu
        /// </summary>
        [ValidateString(IsNumber = true)]
        public string? MinimumStock { get; set; }

        /// <summary>
        /// Số lượng tồn
        /// </summary>
        [ValidateString(IsNumber = true)]
        public string? QuantityTock { get; set; }

        /// <summary>
        /// Nguồn gốc
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Hình ảnh
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// Diễn giải khi mua
        /// </summary>
        public string ExplanationBuy { get; set; }

        /// <summary>
        /// Diễn giải khi bán
        /// </summary>
        public string ExplanationSell { get; set; }

        /// <summary>
        /// ID kho
        /// </summary>
        public string? DepotID { get; set; }

        /// <summary>
        /// Mã nhà kho
        public string? DepotCode { get; set; }

        /// <summary>
        /// Tên nhà kho
        /// </summary>
        public string? DepotName { get; set; }

        /// <summary>
        /// Tài khoản kho
        /// </summary>
        public string AccountDepot { get; set; }

        /// <summary>
        /// Tài khoản doanh thu
        /// </summary>
        public string AccountRevenue { get; set; }

        /// <summary>
        /// Tài khoản chiết khấu
        /// </summary>
        public string AccountDiscount { get; set; }

        /// <summary>
        /// Tài khoản giảm giá
        /// </summary>
        public string AccountSale { get; set; }

        /// <summary>
        /// Tài khoản trả lại
        /// </summary>
        public string AccountReturn { get; set; }

        /// <summary>
        /// Tài khoản chi phí
        /// </summary>
        public string AccountCost { get; set; }

        /// <summary>
        /// Tỉ lệ chiết khấu khuyến mại
        /// </summary>
        [ValidateString(IsNumber = true)]
        public string? RatioDiscount { get; set; }

        /// <summary>
        /// Thuế giá trị gia tăng
        /// </summary>
        [ValidateString(IsNumber = true)]
        public string? VATTax { get; set; }

        /// <summary>
        /// Thuế nhập khẩu
        /// </summary>
        [ValidateString(IsNumber = true)]
        public string? VATImport { get; set; }

        /// <summary>
        /// Thuế xuất khẩu
        /// </summary>
        [ValidateString(IsNumber = true)]
        public string? VATExport { get; set; }

        /// <summary>
        /// Nhóm hàng hoá dịch vụ chịu thuế tiêu thụ đặc biệt
        /// </summary>
        public string VATGroupExciceTax { get; set; }

        /// <summary>
        /// Đơn mua cố định
        /// </summary>
        [ValidateString(IsNumber = true)]
        public string? OrderFix { get; set; }

        /// <summary>
        /// Đơn mua gần nhất
        /// </summary>
        [ValidateString(IsNumber = true)]
        public string? OrderNearest { get; set; }

        /// <summary>
        /// Đơn giá bán
        /// </summary>
        [ValidateString(IsNumber = true)]
        public string? OrderSell { get; set; }

        /// <summary>
        /// Hoạt động hay không hoạt động
        /// </summary>
        [ValidateString(IsBoolean = true)]
        public string? IsActive { get; set; }
    }
}
