using Dapper;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resources;
using MISA.WEB08.AMIS.Common.Result;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng InventoryItem từ tầng DL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class InventoryItemDL : BaseDL<InventoryItem>, IInventoryItemDL
    {
        #region Field

        private IDatabaseHelper<InventoryItem> _dbHelper;

        #endregion

        #region Contructor

        public InventoryItemDL(IDatabaseHelper<InventoryItem> dbHelper) : base(dbHelper)
        {
            _dbHelper = dbHelper;
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
            object result;
            // Khai báo stored procedure
            string storeProcedureName = "Proc_inventoryitem_GetInventoryStatus";
            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                // thực hiện gọi vào DB
                var records = mysqlConnection.QueryMultiple(
                    storeProcedureName,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                result = new
                {
                    result1 = records.ReadSingle().result1,
                    result2 = records.ReadSingle().result2,
                };
            }
            return result;
        }

        /// <summary>
        /// Hàm lấy ra danh sách record có lọc và phân trang over lại để lấy thêm những giá trị
        /// </summary>
        /// <param name="offset">Thứ tự bản ghi bắt đầu lấy</param>
        /// <param name="limit">Số lượng bản ghi muốn lấy</param>
        /// <param name="keyword">Từ khoá tìm kiếm</param>
        /// <param name="sort">Trường muốn sắp xếp</param>
        /// <param name="v_Query">Lọc theo yêu cầu</param>
        /// <returns>Danh sách record và tổng số bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public override Paging GetFitterRecords(int offset, int limit, string? keyword, string? sort, string v_Query, string v_Select)
        {
            Paging result;
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_Offset", offset);
            parameters.Add("v_Limit", limit);
            parameters.Add("v_Sort", sort);
            parameters.Add("v_Where", keyword);
            parameters.Add("v_Query", v_Query);

            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_GetFilterPaging, "InventoryItem");

            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                // thực hiện gọi vào DB
                var records = mysqlConnection.QueryMultiple(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
                result = new Paging
                {
                    recordList = records.Read<InventoryItem>().ToList(),
                    totalCount = records.ReadSingle().totalCount,
                    dataMore = records.ReadSingle().result1,
                };
            }
            return result;
        }

        #endregion
    }
}
