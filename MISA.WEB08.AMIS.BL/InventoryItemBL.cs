using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Result;
using MISA.WEB08.AMIS.DL;
using OfficeOpenXml;
using System.Reflection;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Dữ liệu thao tác với DatACase và trả về với bảng CommodityGroup từ tầng BL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class InventoryItemBL : BaseBL<InventoryItem>, IInventoryItemBL
    {
        #region Field

        private IInventoryItemDL _inventoryItemBL;

        #endregion

        #region Contructor

        public InventoryItemBL(IInventoryItemDL inventoryItemBL) : base(inventoryItemBL)
        {
            _inventoryItemBL = inventoryItemBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm lấy ra tổng số lượng hàng sắp hết và hết hàng
        /// </summary>
        /// <returns></returns>
        //// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetInventoryStatus()
        {
            return _inventoryItemBL.GetInventoryStatus();
        }

        /// <summary>
        /// Hàm custom dữ liệu xuất file
        /// </summary>
        /// <param name="property">Cột dữ liệu cần custom</param>
        /// <returns>Trả ra false khi không custom gì</returns>
        ///  NK Tiềm 05/10/2022
        public override bool CustomValuePropertieExport(PropertyInfo property, ref ExcelWorksheet sheet, int indexRow, int indexBody, InventoryItem inventoryItem)
        {
            if (property.Name == "Nature")
            {
                sheet.Cells[indexRow + 4, indexBody].Value = inventoryItem.Nature == Nature.Goods ? "Hàng hoá" : inventoryItem.Nature == Nature.Service ? "Dịch vụ" : inventoryItem.Nature == Nature.Materials ? "Nguyên vật liệu" : inventoryItem.Nature == Nature.FinishedProduct ? "Thành phẩm" : "Dụng cụ công cụ";
                return true;
            }
            return false;
        }

        /// <summary>
        /// Hàm custom dữ liệu tên file, header, ... khi xuất file
        /// </summary>
        /// <returns></returns>
        ///  NK Tiềm 05/10/2022
        public override OptionExport CustomOptionExport()
        {
            return new OptionExport
            {
                FileName = "Danh sách hàng hoá, dịch vụ",
                Header = "DANH SÁCH HÀNG HOÁ, DỊCH VỤ"
            };
        }

        #endregion
    }
}
