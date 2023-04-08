using Dapper;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resources;
using MISA.WEB08.AMIS.Common.Result;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Data;
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
        /// Hàm lấy ra bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID bản ghi</param>
        /// <param name="stateForm">Trạng thái lấy (sửa hay nhân bản, ...)</param>
        /// <returns>Thông tin chi tiết một bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public override object GetRecordByID(string recordID, string? stateForm)
        {
            InventoryItem result;
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"v_{typeof(InventoryItem).Name}ID", recordID);
            parameters.Add($"v_StateForm", stateForm);
            // Khai báo stored procedure
            string storeProcedureName = string.Format(Resource.Proc_GetDetailOne, typeof(InventoryItem).Name);
            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                //nếu như kết nối đang đóng thì tiến hành mở lại
                if (mysqlConnection.State != ConnectionState.Open)
                {
                    mysqlConnection.Open();
                }
                // thực hiện gọi vào DB
                var records = mysqlConnection.QueryMultiple(
                    storeProcedureName,
                    parameters,
                    commandType: CommandType.StoredProcedure
                    );
                result = records.Read<InventoryItem>().First();
                result.CommodityGroupID = records.Read<Guid>().ToList();
                if (mysqlConnection.State == ConnectionState.Open)
                {
                    mysqlConnection.Close();
                }
            }
            return result;
        }

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
            parameters.Add($"v_CommodityGroupID", JsonConvert.SerializeObject(record.CommodityGroupID.Select(ds => new { id = ds })));
        }

        /// <summary>
        /// Hàm xử lý custom các tham số parameter truyền vào proc create ngoài những tham số mặc định
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="record"></param>
        /// create by: nguyễn khắc tiềm (21/10/2022)
        public override void CustomParameterForUpdate(ref DynamicParameters? parameters, InventoryItem record)
        {
            parameters.Add($"v_CommodityGroupID", JsonConvert.SerializeObject(record.CommodityGroupID.Select(ds => new { id = ds })));
        }

        /// <summary>
        /// Hàm xử lý custom các tham số trả ra của proc
        /// </summary>
        /// <param name="records"></param>
        /// <param name="result"></param>
        /// create by: nguyễn khắc tiềm (21/10/2022)
        public override void CustomResultProc(GridReader records, ref Paging result)
        {
            result.DataMore = new
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
