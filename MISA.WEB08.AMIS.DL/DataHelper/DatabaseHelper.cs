using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// Các thao tác gọi proc
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class DatabaseHelper<T> : IDatabaseHelper<T>
    {
        #region Method

        /// <summary>
        /// Chạy proc với query trong dapper
        /// </summary>
        /// <returns>object</returns>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        public virtual object RunProcWithQuery(string storeProcedureName, DynamicParameters? parameters)
        {
            object result;
            // Khai báo stored procedure
            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                // thực hiện gọi vào DB
                result = mysqlConnection.Query<T>(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
            return result;
        }

        /// <summary>
        /// Chạy proc với QueryFirstOrDefault trong dapper
        /// </summary>
        /// <returns>object</returns>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        public virtual object RunProcWithQueryFirstOrDefault(string storeProcedureName, DynamicParameters? parameters)
        {
            object result;
            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                // thực hiện gọi vào DB
                result = mysqlConnection.QueryFirstOrDefault<T>(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
            return result;
        }

        /// <summary>
        /// Chạy proc với Execute trong dapper
        /// </summary>
        /// <returns>object</returns>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        public int RunProcWithExecute(string storeProcedureName, DynamicParameters? parameters)
        {
            var rowAffects = 0;
            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                //nếu như kết nối đang đóng thì tiến hành mở lại
                if (mysqlConnection.State != ConnectionState.Open)
                {
                    mysqlConnection.Open();
                }
                //mở một giao dịch( nếu xóa thành công thì xóa hết, nếu lỗi giữa chừng thì dừng lại và khôi phục các dữ liệu đã bị xóa)
                using (var transaction = mysqlConnection.BeginTransaction())
                {
                    try
                    {
                        // thực hiện gọi vào DB
                        rowAffects += mysqlConnection.Execute(storeProcedureName,
                            parameters,
                            transaction: transaction,
                            commandType: System.Data.CommandType.StoredProcedure
                            );
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        //nếu thực hiện không thành công thì rollback
                        transaction.Rollback();
                        rowAffects = 0;
                    }
                    finally
                    {
                        if (mysqlConnection.State == ConnectionState.Open)
                        {
                            mysqlConnection.Close();
                        }
                    }
                }
            }
            return rowAffects;
        }

        #endregion
    }
}
