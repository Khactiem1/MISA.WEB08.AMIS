using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Resources;
using MISA.WEB08.AMIS.Common.Result;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Dữ liệu thao tác với Database và trả về với bảng trong Database từ tầng BL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Contructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm Lấy danh sách tất cả bản ghi của 1 bảng
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        /// <summary> 
        /// Hàm Lấy danh sách tất cả bản ghi của 1 bảng đang hoạt động
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetAllRecordActive()
        {
            return _baseDL.GetAllRecordActive();
        }

        /// <summary>
        /// Hàm lấy ra bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>Thông tin chi tiết một bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetRecordByID(Guid recordID)
        {
            return _baseDL.GetRecordByID(recordID);
        }

        /// <summary>
        /// Hàm lấy ra mã record tự sinh
        /// </summary>
        /// <returns>Mã bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public object GetRecordCodeNew()
        {
            return _baseDL.GetRecordCodeNew();
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
            return _baseDL.GetFitterRecords(offset, limit, keyword, sort);
        }

        /// <summary>
        /// Hàm thêm mới một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>ID bản ghi sau khi thêm</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public ServiceResponse InsertRecord(T record)
        {
            Validate<T> Valid = new Validate<T>(_baseDL);
            var validateUnique = Valid.CheckUnique(record, null);
            if (validateUnique.Success)
            {
                var validateResult = Validate<T>.ValidateData(record);
                if (validateResult.Success)
                {
                    var validateCustom = CustomValidate(record);
                    if(validateCustom.Success)
                    {
                        Guid result = _baseDL.InsertRecord(record);
                        if (result != Guid.Empty)
                        {
                            return new ServiceResponse
                            {
                                Success = true,
                                Data = result,
                            };
                        }
                        else
                        {
                            return new ServiceResponse
                            {
                                Success = false,
                                ErrorCode = MisaAmisErrorCode.InsertFailed,
                                Data = Resource.UserMsg_InsertFailed,
                            };
                        }
                    }
                    else
                    {
                        return validateCustom;
                    }
                }
                else
                {
                    return validateResult;
                }
            }
            else
            {
                return validateUnique;
            }
        }

        /// <summary>
        /// Hàm cập nhật thông tin một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <param name="record"></param>
        /// <returns>ID record sau khi cập nhật</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public ServiceResponse UpdateRecord(Guid recordID, T record)
        {
            Validate<T> Valid = new Validate<T>(_baseDL);
            var validateUnique = Valid.CheckUnique(record, recordID);
            if (validateUnique.Success)
            {
                var validateResult = Validate<T>.ValidateData(record);
                if (validateResult.Success)
                {
                    var validateCustom = CustomValidate(record);
                    if (validateCustom.Success)
                    {
                        Guid result = _baseDL.UpdateRecord(recordID, record);
                        if (result != Guid.Empty)
                        {
                            return new ServiceResponse
                            {
                                Success = true,
                                Data = result,
                            };
                        }
                        else
                        {
                            return new ServiceResponse
                            {
                                Success = false,
                                ErrorCode = MisaAmisErrorCode.UpdateFailed,
                                Data = result,
                            };
                        }
                    }
                    else
                    {
                        return validateCustom;
                    }
                }
                else
                {
                    return validateResult;
                }
            }
            else
            {
                return validateUnique;
            }
        }

        /// <summary>
        /// Xoá một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>ID record sau khi xoá</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public ServiceResponse DeleteRecord(Guid recordID)
        {
            Guid result = _baseDL.DeleteRecord(recordID);
            if (result != Guid.Empty)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Data = result,
                };
            }
            else
            {
                return new ServiceResponse
                {
                    Success = false,
                    ErrorCode = MisaAmisErrorCode.DeleteFailed,
                    Data = result,
                };
            }
        }

        /// <summary>
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="listRecordID">danh sách bản ghi cần xoá</param>
        /// <returns>Số kết quả bản ghi đã xoá</returns>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public ServiceResponse DeleteMultiple(Guid[] listRecordID)
        {
            int rowAffects = _baseDL.DeleteMultiple(listRecordID);
            if (rowAffects == 0)
            {
                return new ServiceResponse
                {
                    Success = false,
                    ErrorCode = MisaAmisErrorCode.DeleteMultiple,
                    Data = rowAffects
                };
            }
            else
            {
                return new ServiceResponse
                {
                    Success = true,
                    Data = rowAffects
                };
            }
        }

        /// <summary>
        /// Hàm xử lý custom validate những model riêng biệt
        /// </summary>
        /// <param name="record">Record cần custom validate</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public virtual ServiceResponse CustomValidate(T record)
        {
            return new ServiceResponse
            {
                Success = true
            };
        }

        /// <summary>
        /// Hàm cập nhật toggle active bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>ID record sau khi cập nhật</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        public ServiceResponse ToggleActive(Guid recordID)
        {
            Guid result = _baseDL.ToggleActive(recordID);
            if (result != Guid.Empty)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Data = result,
                };
            }
            else
            {
                return new ServiceResponse
                {
                    Success = false,
                    ErrorCode = MisaAmisErrorCode.UpdateFailed,
                    Data = result,
                };
            }
        }

        #endregion
    }
}
