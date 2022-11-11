using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [PrimaryKey]
        public Guid? InventoryItemID { get; set; }

        /// <summary>
        /// id đơn vị tính
        /// </summary>7
        public Guid? UnitCalculationID { get; set; }

        /// <summary>
        /// mã hàng hoá vật tư
        /// </summary>
        [Unique("Mã hàng hoá vật tư <{0}> đã tồn tại trong hệ thống.")]
        [IsNotNullOrEmpty("Mã hàng hoá vật tư không được để trống.")]
        [ColumnName("Mã hàng hoá vật tư", 16, false, false, false)]
        public string InventoryItemCode { get; set; }

        /// <summary>
        /// tên hàng hoá vật tư
        /// </summary>
        [IsNotNullOrEmpty("Tên hàng hoá vật tư không được để trống.")]
        [ColumnName("Tên hàng hoá vật tư", 40, false, false, false)]
        public string InventoryItemName { get; set; }

        /// <summary>
        /// ID nhóm vật tư hàng hoá
        /// </summary>
        public string CommodityGroupID { get; set; }

        /// <summary>
        /// Mã nhóm vật tư hàng hoá
        /// </summary>
        [ColumnName("Mã nhóm vật tư hàng hoá", 50, false, false, false)]
        public string CommodityCode { get; set; }

        /// <summary>
        /// Hình ảnh
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// tính chất
        /// </summary>
        [IsNotNullOrEmpty("Tính chất không được để trống.")]
        [ColumnName("Tính chất", 40, false, false, false)]
        public Nature Nature { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        [ColumnName("Tên đơn vị tính", 40, false, false, false)]
        public string UnitCalculationName { get; set; }

        /// <summary>
        /// Giảm thuế
        /// </summary>
        [ColumnName("Giảm thuế", 40, false, false, false)]
        public string DepreciatedTax { get; set; }

        /// <summary>
        /// Thời hạn bảo hành
        /// </summary>
        [ColumnName("Thời hạn bảo hành", 40, false, false, false)]
        public string WarrantyPeriod { get; set; }

        /// <summary>
        /// Số lượng tồn tối thiểu
        /// </summary>
        [ColumnName("Số lượng tồn tối thiểu", 40, false, false, true)]
        public double? MinimumStock { get; set; }


        /// <summary>
        /// Số lượng tồn
        /// </summary>
        [ColumnName("Số lượng tồn", 40, false, false, true)]
        public double? QuantityTock { get; set; }

        /// <summary>
        /// Nguồn gốc
        /// </summary>
        [ColumnName("Nguồn gốc", 40, false, false, false)]
        public string Origin { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        [ColumnName("Mô tả", 40, false, false, false)]
        public string Description { get; set; }

        /// <summary>
        /// Diễn giải khi mua
        /// </summary>
        [ColumnName("Diễn giải khi mua", 40, false, false, false)]
        public string ExplanationBuy { get; set; }

        /// <summary>
        /// Diễn giải khi bán
        /// </summary>
        [ColumnName("Diễn giải khi bán", 40, false, false, false)]
        public string ExplanationSell { get; set; }

        /// <summary>
        /// ID kho
        /// </summary>
        public Guid? DepotID { get; set; }

        /// <summary>
        /// Tên nhà kho
        /// </summary>
        [ColumnName("Tên nhà kho", 40, false, false, false)]
        public string? DepotName { get; set; }

        /// <summary>
        /// Tài khoản kho
        /// </summary>
        [ColumnName("Tài khoản kho", 40, false, false, false)]
        public string AccountDepot { get; set; }

        /// <summary>
        /// Tài khoản doanh thu
        /// </summary>
        [ColumnName("Tài khoản doanh thu", 40, false, false, false)]
        public string AccountRevenue { get; set; }

        /// <summary>
        /// Tài khoản chiết khấu
        /// </summary>
        [ColumnName("Tài khoản chiết khấu", 40, false, false, false)]
        public string AccountDiscount { get; set; }

        /// <summary>
        /// Tài khoản giảm giá
        /// </summary>
        [ColumnName("Tài khoản giảm giá", 40, false, false, false)]
        public string AccountSale { get; set; }

        /// <summary>
        /// Tài khoản trả lại
        /// </summary>
        [ColumnName("Tài khoản trả lại", 40, false, false, false)]
        public string AccountReturn { get; set; }

        /// <summary>
        /// Tài khoản chi phí
        /// </summary>
        [ColumnName("Tài khoản chi phí", 40, false, false, false)]
        public string AccountCost { get; set; }

        /// <summary>
        /// Tỉ lệ chiết khấu khuyến mại
        /// </summary>
        [ColumnName("Tỉ lệ chiết khấu khuyến mại", 40, false, false, true)]
        public float? RatioDiscount { get; set; }

        /// <summary>
        /// Thuế giá trị gia tăng
        /// </summary>
        [ColumnName("Thuế giá trị gia tăng", 40, false, false, true)]
        public float? VATTax { get; set; }

        /// <summary>
        /// Thuế nhập khẩu
        /// </summary>
        [ColumnName("Thuế nhập khẩu", 40, false, false, true)]
        public float? VATImport { get; set; }

        /// <summary>
        /// Thuế xuất khẩu
        /// </summary>
        [ColumnName("Thuế xuất khẩu", 40, false, false, true)]
        public float? VATExport { get; set; }

        /// <summary>
        /// Nhóm hàng hoá dịch vụ chịu thuế tiêu thụ đặc biệt
        /// </summary>
        [ColumnName("Nhóm hàng hoá dịch vụ chịu thuế tiêu thụ đặc biệt", 40, false, false, false)]
        public string VATGroupExciceTax { get; set; }

        /// <summary>
        /// Đơn mua cố định
        /// </summary>
        [ColumnName("Đơn mua cố định", 40, false, false, true)]
        public double? OrderFix { get; set; }

        /// <summary>
        /// Đơn mua gần nhất
        /// </summary>
        [ColumnName("Đơn mua gần nhất", 40, false, false, true)]
        public double? OrderNearest { get; set; }

        /// <summary>
        /// Đơn giá bán
        /// </summary>
        [ColumnName("Đơn giá bán", 40, false, false, true)]
        public double? OrderSell { get; set; }

        /// <summary>
        /// Hoạt động hay không hoạt động
        /// </summary>
        [ColumnName("Trạng thái", 20, false, false, false)]
        public bool IsActive { get; set; }
    }
}
