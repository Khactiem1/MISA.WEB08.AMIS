using MISA.WEB08.AMIS.Common.Entities;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng InventoryItem từ tầng DL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public interface IInventoryItemDL : IBaseDL<InventoryItem>
    {
        /// <summary>
        /// Hàm lấy ra tổng số lượng hàng sắp hết và hết hàng
        /// </summary>
        /// <returns></returns>
        //// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetInventoryStatus();
    }
}
