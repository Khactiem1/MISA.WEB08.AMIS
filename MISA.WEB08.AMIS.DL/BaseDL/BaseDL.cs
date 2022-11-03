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
        /// Hàm Lấy danh sách tất cả bản ghi trong 1 bảng đang hoạt động
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetAllRecordActive()
        {
            object result;
            // Khai báo stored procedure
            string storeProcedureName = string.Format(Resource.Proc_GetAllActive, typeof(T).Name);
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
        /// <param name="offset">Thứ tự bản ghi bắt đầu lấy</param>
        /// <param name="limit">Số lượng bản ghi muốn lấy</param>
        /// <param name="keyword">Từ khoá tìm kiếm</param>
        /// <param name="sort">Trường muốn sắp xếp</param>
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
        /// Validate trùng mã nếu mã bản ghi đã tồn tại trong hệ thống
        /// </summary>
        /// <param name="propertyName">Trường cần kiểm tra mã trùng </param>
        /// <param name="propertyValue">Giá trị cần kiểm tra </param>
        /// <param name="guidUpdate">Ko kiểm tra chính nó khi update </param>
        /// <returns>true- mã bị trùng; false-mã k bị trùng</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public bool CheckDuplicate(string propertyName, object propertyValue, Guid? guidUpdate)
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
            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                // thực hiện gọi vào DB
                result = mysqlConnection.QueryFirstOrDefault<T>(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
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

        /// <summary>
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="listRecordID">danh sách bản ghi cần xoá</param>
        /// <returns>Số kết quả bản ghi đã xoá</returns>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public int DeleteMultiple(Guid[] listRecordID)
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
                        string storeProcedureName = string.Format(Resource.Proc_DeleteOne, typeof(T).Name);
                        //lặp lại thao tác xóa với từng Id có trong mảng Id đầu vào 
                        for (int i = 0; i < listRecordID.Length; i++)
                        {
                            // Khởi tạo các parameter để chèn vào trong Proc
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add($"v_{typeof(T).Name}ID", listRecordID[i]);
                            // thực hiện gọi vào DB
                            rowAffects += mysqlConnection.Execute(storeProcedureName,
                                parameters,
                                transaction: transaction,
                                commandType: System.Data.CommandType.StoredProcedure
                                );
                        }
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
                        mysqlConnection.Close();
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
        public Guid ToggleActive(Guid recordID)
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
            }
            parameters.Add($"v_ModifiedBy", Resource.DefaultUser);
            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = string.Format(Resource.Proc_UpdateActive, typeof(T).Name);
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