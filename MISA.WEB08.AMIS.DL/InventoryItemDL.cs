using Dapper;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resources;
using MISA.WEB08.AMIS.Common.Result;
using MySqlConnector;
using System.Linq;
using static Dapper.SqlMapper;

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
        /// Hàm xử lý custom các tham số parameter truyền vào proc create ngoài những tham số mặc định
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="record"></param>
        /// create by: nguyễn khắc tiềm (21/10/2022)
        public override void CustomParameterForCreate(ref DynamicParameters? parameters, InventoryItem record)
        {
            string prefix = "";
            string number = "";
            string last = "";
            _dbHelper.SaveCode(record.InventoryItemCode, ref prefix, ref number, ref last);
            parameters.Add($"v_prefix", prefix);
            parameters.Add($"v_number", number);
            parameters.Add($"v_last", last);
            parameters.Add($"v_lengthNumber", number.Length);
        }

        /// <summary>
        /// Hàm xử lý custom các tham số trả ra của proc
        /// </summary>
        /// <param name="records"></param>
        /// <param name="result"></param>
        /// create by: nguyễn khắc tiềm (21/10/2022)
        public override void CustomResultProc(GridReader records, ref Paging result)
        {
            result.dataMore = new
            {
                // Tổng số lượng tồn
                quantityInStock = records.ReadSingle().quantityInStock,
            };
        }

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
                    // Sắp Hết hàng
                    outOfStockSoon = records.ReadSingle().outOfStockSoon,
                    // Hết hàng
                    outOfStock = records.ReadSingle().outOfStock,
                };
            }
            return result;
        }

        #endregion
    }
}
