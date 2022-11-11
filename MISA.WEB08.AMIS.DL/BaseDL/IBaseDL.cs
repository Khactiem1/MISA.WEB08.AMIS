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
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Hàm Lấy danh sách tất cả bản ghi trong 1 bảng
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetAllRecords();

        /// <summary>
        /// Hàm Lấy danh sách tất cả bản ghi trong 1 bảng đang hoạt động
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetAllRecordActive();
        /// <summary>
        /// Hàm lấy ra bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>Thông tin chi tiết một bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetRecordByID(Guid recordID);

        /// <summary>
        /// Hàm lấy ra mã record tự sinh
        /// </summary>
        /// <returns>Mã bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetRecordCodeNew();

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
        public object GetFitterRecords(int offset, int limit, string? keyword, string? sort,string v_Query);

        /// <summary>
        /// Validate trùng mã nếu mã bản ghi đã tồn tại trong hệ thống
        /// </summary>
        /// <param name="propertyName">Trường cần kiểm tra mã trùng </param>
        /// <param name="propertyValue">Giá trị cần kiểm tra </param>
        /// <param name="guidUpdate">Ko kiểm tra chính nó khi update </param>
        /// <returns>true- mã bị trùng; false-mã k bị trùng</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public bool CheckDuplicate(string propertyName, object propertyValue, Guid? guidUpdate);

        /// <summary>
        /// Hàm kiểm tra giá trị phát sinh khi xoá
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="valueCheck"></param>
        /// <returns>true hoặc false tương ứng với phát sinh hoặc không</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public bool CheckIncurred(string tableName, string columnName, string valueCheck);

        /// <summary>
        /// Hàm thêm mới một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>ID bản ghi sau khi thêm</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Guid InsertRecord(T record);

        /// <summary>
        /// Hàm cập nhật thông tin một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <param name="record"></param>
        /// <returns>ID record sau khi cập nhật</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Guid UpdateRecord(Guid recordID, T record);

        /// <summary>
        /// Hàm cập nhật bản ghi mã tự sinh
        /// </summary>
        /// <param name="prefix">Phần tiền tố</param>
        /// <param name="number">Phần số</param>
        /// <param name="last">phần hậu tố</param>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public void SaveCode(string prefix, string number, string last);

        /// <summary>
        /// Xoá một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>ID record sau khi xoá</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Guid DeleteRecord(Guid recordID);

        /// <summary>
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="listRecordID">danh sách bản ghi cần xoá</param>
        /// <returns>Dữ liệu của bản ghi nếu như bản ghi đó có tồn tại trong hệ thống</returns>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public int DeleteMultiple(Guid[] listRecordID);

        /// <summary>
        /// Hàm cập nhật toggle active bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>ID record sau khi cập nhật</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public Guid ToggleActive(Guid recordID);
    }
}
