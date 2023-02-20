using Dapper;
using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Resources;
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
    /// Dữ liệu thao tác với Database và trả về với bảng trong Database từ tầng DL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class BaseDL<T> : IBaseDL<T>
    {

        #region Field

        private IDatabaseHelper<T> _dbHelper;

        #endregion

        #region Contructor

        public BaseDL(IDatabaseHelper<T> dbHelper)
        {
            _dbHelper = dbHelper;
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm Lấy danh sách tất cả bản ghi trong 1 bảng
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual object GetAllRecords()
        {
            string storeProcedureName = string.Format(Resource.Proc_GetAll, typeof(T).Name);
            return _dbHelper.RunProcWithQuery(storeProcedureName, null);
        }

        /// <summary>
        /// Hàm Lấy danh sách tất cả bản ghi trong 1 bảng đang hoạt động
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual object GetAllRecordActive()
        {
            string storeProcedureName = string.Format(Resource.Proc_GetAllActive, typeof(T).Name);
            return _dbHelper.RunProcWithQuery(storeProcedureName, null);
        }

        /// <summary>
        /// Hàm Lấy danh sách bản ghi theo từ khoá tìm kiếm không phân trang
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual object GetsDataExport(string? keyword, string? sort)
        {
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_Sort", sort);
            parameters.Add("v_Where", keyword);
            // Khai báo stored procedure
            string storeProcedureName = string.Format(Resource.Proc_GetFilterExport, typeof(T).Name);
            return _dbHelper.RunProcWithQuery(storeProcedureName, parameters);
        }

        /// <summary>
        /// Hàm lấy ra bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>Thông tin chi tiết một bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual object GetRecordByID(string recordID)
        {
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"v_{typeof(T).Name}ID", recordID);
            // Khai báo stored procedure
            string storeProcedureName = string.Format(Resource.Proc_GetDetailOne, typeof(T).Name);
            return _dbHelper.RunProcWithQueryFirstOrDefault(storeProcedureName, parameters);
        }

        /// <summary>
        /// Validate trùng mã nếu mã bản ghi đã tồn tại trong hệ thống
        /// </summary>
        /// <param name="propertyName">Trường cần kiểm tra mã trùng </param>
        /// <param name="propertyValue">Giá trị cần kiểm tra </param>
        /// <param name="guidUpdate">Ko kiểm tra chính nó khi update </param>
        /// <returns>true- mã bị trùng; false-mã k bị trùng</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual bool CheckDuplicate(string propertyName, object propertyValue, Guid? guidUpdate)
        {
            object result;
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_PropertyName", propertyName);
            parameters.Add("v_Table", typeof(T).Name);
            parameters.Add("v_PropertyValue", propertyValue.ToString().Trim());
            parameters.Add("v_GuidUpdate", guidUpdate);

            //Khai báo stored truy vấn
            string storeProcedureName = Resource.Proc_GetDataByAttribute;
            result = _dbHelper.RunProcWithQueryFirstOrDefault(storeProcedureName, parameters);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Hàm lấy ra mã record tự sinh
        /// </summary>
        /// <returns>Mã bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual object GetRecordCodeNew()
        {
            object result;
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = "Proc_coderecord_GetNewCode";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_TableName", typeof(T).Name);
            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                // thực hiện gọi vào DB
                result = mysqlConnection.QueryFirstOrDefault<string>(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
            return result;
        }

        /// <summary>
        /// Hàm lấy ra danh sách record có lọc và phân trang
        /// </summary>
        /// <param name="offset">Thứ tự bản ghi bắt đầu lấy</param>
        /// <param name="limit">Số lượng bản ghi muốn lấy</param>
        /// <param name="keyword">Từ khoá tìm kiếm</param>
        /// <param name="sort">Trường muốn sắp xếp</param>
        /// <param name="v_Query">Lọc theo yêu cầu</param>
        /// <returns>Danh sách record và tổng số bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual object GetFitterRecords(int offset, int limit, string? keyword, string? sort, string v_Query)
        {
            object result;
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_Offset", offset);
            parameters.Add("v_Limit", limit);
            parameters.Add("v_Sort", sort);
            parameters.Add("v_Where", keyword);
            parameters.Add("v_Query", v_Query);

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
        /// Hàm kiểm tra giá trị phát sinh khi xoá
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="valueCheck"></param>
        /// <returns>true hoặc false tương ứng với phát sinh hoặc không</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual bool CheckIncurred(string tableName, string columnName, string valueCheck)
        {
            int result;
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_TableName", tableName);
            parameters.Add("v_ColumnName", columnName);
            parameters.Add("v_ValueCheck", valueCheck);

            //Khai báo stored truy vấn
            string storeProcedureName = Resource.Proc_GetDataByCheckIncurred;
            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                // thực hiện gọi vào DB
                result = mysqlConnection.QueryFirstOrDefault<int>(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
            if (result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Hàm thêm mới một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>ID bản ghi sau khi thêm</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual Guid InsertRecord(T record)
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
                var primaryKeyAttribute = (ValidateAttribute?)Attribute.GetCustomAttribute(property, typeof(ValidateAttribute));
                if (primaryKeyAttribute != null && primaryKeyAttribute.PrimaryKey)
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
            int numberOfAffectdRows = _dbHelper.RunProcWithExecute(storeProcedureName, parameters);
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
        /// Hàm cập nhật bản ghi mã tự sinh
        /// </summary>
        /// <param name="prefix">Phần tiền tố</param>
        /// <param name="number">Phần số</param>
        /// <param name="last">phần hậu tố</param>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual void SaveCode(string prefix, string number, string last)
        {
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_TableName", typeof(T).Name);
            parameters.Add("v_Prefix", prefix);
            parameters.Add("v_ValueNumber", number);
            parameters.Add("v_Last", last);
            parameters.Add("v_LengthNumber", number.Length);
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = "Proc_coderecord_UpdateOne";
            _dbHelper.RunProcWithExecute(storeProcedureName, parameters);
        }

        /// <summary>
        /// Hàm cập nhật thông tin một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <param name="record"></param>
        /// <returns>ID record sau khi cập nhật</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual Guid UpdateRecord(Guid recordID, T record)
        {
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                // Kiểm tra trường nào là khoá chính thì thêm param là id tự sinh Guid
                var primaryKeyAttribute = (ValidateAttribute?)Attribute.GetCustomAttribute(property, typeof(ValidateAttribute));
                if (primaryKeyAttribute != null && primaryKeyAttribute.PrimaryKey)
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
            int numberOfAffectdRows = _dbHelper.RunProcWithExecute(storeProcedureName, parameters);
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
        public virtual Guid DeleteRecord(Guid recordID)
        {
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_DeleteOne, typeof(T).Name);
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"v_{typeof(T).Name}ID", recordID);
            int numberOfAffectdRows = _dbHelper.RunProcWithExecute(storeProcedureName, parameters);
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
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="listRecordID">danh sách bản ghi cần xoá</param>
        /// <returns>Số kết quả bản ghi đã xoá</returns>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public virtual int DeleteMultiple(Guid[] listRecordID)
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
                        // chuẩn bị câu lệnh MySQL
                        string storeProcedureName = string.Format(Resource.Proc_DeleteMultiple, typeof(T).Name);
                        // Khởi tạo các parameter để chèn vào trong Proc
                        DynamicParameters parameters = new DynamicParameters();
                        string listID = string.Join(";", listRecordID);
                        parameters.Add("v_ListID", listID);
                        // thực hiện gọi vào DB
                        rowAffects += mysqlConnection.Execute(storeProcedureName,
                            parameters,
                            transaction: transaction,
                            commandType: System.Data.CommandType.StoredProcedure
                            );
                        if (rowAffects == listRecordID.Length)
                        {
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                            rowAffects = 0;
                        }
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
            //trả về kết quả(số bản ghi xóa được)
            return rowAffects;
        }

        /// <summary>
        /// Hàm cập nhật toggle active bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>ID record sau khi cập nhật</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public virtual Guid ToggleActive(Guid recordID)
        {
            // Khởi tạo các parameter để chèn vào trong Proc
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_ID", recordID);
            parameters.Add("v_ModifiedBy", Resource.DefaultUser);
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_UpdateActive, typeof(T).Name);
            int numberOfAffectdRows = _dbHelper.RunProcWithExecute(storeProcedureName, parameters);
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