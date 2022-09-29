using Dapper;
using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng trong Database từ tầng DL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class BaseDL<T> : IBaseDL<T>
    {
        #region Method

        /// <summary>
        /// Hàm Lấy danh sách tất cả bản ghi trong 1 bảng
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetAllRecords()
        {
            object result;
            // Khai báo stored procedure
            string storeProcedureName = string.Format(Resource.Proc_GetAll, typeof(T).Name);
            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                // thực hiện gọi vào DB
                result = mysqlConnection.Query<T>(
                    storeProcedureName,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
            return result;
        }

        /// <summary>
        /// Hàm lấy ra bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>Thông tin chi tiết một bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetRecordByID(Guid recordID)
        {
            object result;
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"v_{typeof(T).Name}ID", recordID);

            // Khai báo stored procedure
            string storeProcedureName = string.Format(Resource.Proc_GetDetailOne, typeof(T).Name);
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
        /// Hàm lấy ra mã record tự sinh
        /// </summary>
        /// <returns>Mã bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetRecordCodeNew()
        {
            object result;
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_GetNewCode, typeof(T).Name);
            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                // thực hiện gọi vào DB
                result = mysqlConnection.QueryFirstOrDefault<string>(
                    storeProcedureName,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
            return result;
        }

        /// <summary>
        /// Hàm lấy ra danh sách record có lọc và phân trang
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="keyword"></param>
        /// <param name="sort"></param>
        /// <returns>Danh sách record và tổng số bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetFitterRecords(int offset, int limit, string? keyword, string? sort)
        {
            object result;
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_Offset", offset);
            parameters.Add("v_Limit", limit);
            parameters.Add("v_Sort", sort);
            parameters.Add("v_Where", keyword);

            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_GetFilterPaging, typeof(T).Name);

            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                // thực hiện gọi vào DB
                var records = mysqlConnection.QueryMultiple(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
                result = new
                {
                    recordList = records.Read<T>().ToList(),
                    totalCount = records.ReadSingle().totalCount,
                };
            }
            return result;
        }

        /// <summary>
        /// Hàm thêm mới một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>ID bản ghi sau khi thêm</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Guid InsertRecord(T record)
        {
            // tạo recordID
            Guid recordID = Guid.NewGuid();

            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                // Kiểm tra trường nào là khoá chính thì thêm param là id tự sinh Guid
                var primaryKeyAttribute = (PrimaryKeyAttribute?)Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));
                if(primaryKeyAttribute != null)
                {
                    parameters.Add($"v_{propertyName}", recordID);
                }
                else
                {
                    var propertyValue = property.GetValue(record, null);
                    parameters.Add($"v_{propertyName}", propertyValue);
                }
            }
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_InsertOne, typeof(T).Name);
            int numberOfAffectdRows = 0;
            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                // thực hiện gọi vào DB
                numberOfAffectdRows = mysqlConnection.Execute(storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
            if (numberOfAffectdRows > 0)
            {
                return recordID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Hàm cập nhật thông tin một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <param name="record"></param>
        /// <returns>ID record sau khi cập nhật</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Guid UpdateRecord(Guid recordID, T record)
        {
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                // Kiểm tra trường nào là khoá chính thì thêm param là id tự sinh Guid
                var primaryKeyAttribute = (PrimaryKeyAttribute?)Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));
                if (primaryKeyAttribute != null)
                {
                    parameters.Add($"v_{propertyName}", recordID);
                }
                else
                {
                    var propertyValue = property.GetValue(record, null);
                    parameters.Add($"v_{propertyName}", propertyValue);
                }
            }
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_UpdateOne, typeof(T).Name);
            int numberOfAffectdRows = 0;
            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                // thực hiện gọi vào DB
                numberOfAffectdRows = mysqlConnection.Execute(storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
            if (numberOfAffectdRows > 0)
            {
                return recordID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Xoá một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>ID record sau khi xoá</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Guid DeleteRecord(Guid recordID)
        {
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_DeleteOne, typeof(T).Name);
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"v_{typeof(T).Name}ID", recordID);
            int numberOfAffectdRows = 0;
            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                // thực hiện gọi vào DB
                numberOfAffectdRows = mysqlConnection.Execute(storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
            if (numberOfAffectdRows > 0)
            {
                return recordID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        #endregion
    }
}
