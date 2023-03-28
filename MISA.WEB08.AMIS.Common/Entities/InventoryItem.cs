using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Enums;
using System;

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
        /// </summary>7
        public Guid? UnitCalculationID { get; set; }

        /// <summary>
        /// mã hàng hoá vật tư
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "Mã hàng hoá vật tư không được để trống.")]
        [ColumnName(Name = "Mã hàng hoá vật tư", Width = 16)]
        public string InventoryItemCode { get; set; }

        /// <summary>
        /// tên hàng hoá vật tư
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "Tên hàng hoá vật tư không được để trống.")]
        [ColumnName(Name = "Tên hàng hoá vật tư", Width = 40)]
        public string InventoryItemName { get; set; }

        /// <summary>
        /// ID nhóm vật tư hàng hoá
        /// </summary>
        public string CommodityGroupID { get; set; }

        /// <summary>
        /// Mã nhóm vật tư hàng hoá
        /// </summary>
        [ColumnName(Name = "Mã nhóm vật tư hàng hoá", Width = 50)]
        public string CommodityCode { get; set; }

        /// <summary>
        /// Hình ảnh
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// tính chất
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "Tính chất không được để trống.")]
        [ColumnName(Name = "Tính chất", Width = 40)]
        public Nature Nature { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        [ColumnName(Name = "Tên đơn vị tính", Width = 40)]
        public string UnitCalculationName { get; set; }

        /// <summary>
        /// Giảm thuế
        /// </summary>
        [ColumnName(Name = "Giảm thuế", Width = 40)]
        public string DepreciatedTax { get; set; }

        /// <summary>
        /// Thời hạn bảo hành
        /// </summary>
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
        [ColumnName(Name = "Nguồn gốc", Width = 40)]
        public string Origin { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        [ColumnName(Name = "Mô tả", Width = 40)]
        public string Description { get; set; }

        /// <summary>
        /// Diễn giải khi mua
        /// </summary>
        [ColumnName(Name = "Diễn giải khi mua", Width = 40)]
        public string ExplanationBuy { get; set; }

        /// <summary>
        /// Diễn giải khi bán
        /// </summary>
        [ColumnName(Name = "Diễn giải khi bán", Width = 40)]
        public string ExplanationSell { get; set; }

        /// <summary>
        /// ID kho
        /// </summary>
        public Guid? DepotID { get; set; }

        /// <summary>
        /// Tên nhà kho
        /// </summary>
        [ColumnName(Name = "Tên nhà kho", Width = 40)]
        public string? DepotName { get; set; }

        /// <summary>
        /// Tài khoản kho
        /// </summary>
        [ColumnName(Name = "Tài khoản kho", Width = 40)]
        public string AccountDepot { get; set; }

        /// <summary>
        /// Tài khoản doanh thu
        /// </summary>
        [ColumnName(Name = "Tài khoản doanh thu", Width = 40)]
        public string AccountRevenue { get; set; }

        /// <summary>
        /// Tài khoản chiết khấu
        /// </summary>
        [ColumnName(Name = "Tài khoản chiết khấu", Width = 40)]
        public string AccountDiscount { get; set; }

        /// <summary>
        /// Tài khoản giảm giá
        /// </summary>
        [ColumnName(Name = "Tài khoản giảm giá", Width = 40)]
        public string AccountSale { get; set; }

        /// <summary>
        /// Tài khoản trả lại
        /// </summary>
        [ColumnName(Name = "Tài khoản trả lại", Width = 40)]
        public string AccountReturn { get; set; }

        /// <summary>
        /// Tài khoản chi phí
        /// </summary>
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
        [ColumnName(Name = "Nhóm hàng hoá dịch vụ chịu thuế tiêu thụ đặc biệt", Width = 40)]
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
        [ColumnName(Name = "Trạng thái", Width = 20)]
        public bool? IsActive { get; set; }
    }
}
