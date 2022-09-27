using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resource;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng Unit từ tầng DL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class UnitDL : IUnitDL
    {
        #region Field

        /// <summary>
        /// chuỗi kết nối
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        /// </summary>
        private string SqlConnection { get; set; }

        #endregion

        #region Contructor

        /// <summary>
        /// lấy ra chuỗi kết nối từ file appsettings.json
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        /// </summary>
        /// <param name="config"></param>
        public UnitDL(IConfiguration config)
        {
            SqlConnection = config["ConnectionStrings:DefaultConnection"];
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm lấy ra danh sách tất cả đơn vị
        /// </summary>
        /// <returns>Danh sách đơn vị</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetAllUnits()
        {
            var mysqlConnection = new MySqlConnection(SqlConnection);
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_GetAll, typeof(Unit).Name);


            // thực hiện gọi vào DB
            var unitList = mysqlConnection.Query<Unit>(
                storeProcedureName,
                commandType: System.Data.CommandType.StoredProcedure
                );
            return unitList;
        } 

        #endregion
    }
}
