using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Resources;
using MISA.WEB08.AMIS.Common.Result;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// API dữ liệu với bảng trong database
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm (21/09/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Contructor

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method

        #region API GET

        /// <summary>
        /// API lấy ra danh sách tất bản ghi trong 1 bảng
        /// <summary>
        /// <return> Danh sách tất cả bản ghi <return>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            try
            {
                var recordList = _baseBL.GetAllRecords();
                return StatusCode(StatusCodes.Status200OK, recordList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        /// <summary>
        /// Hàm lấy ra bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>Thông tin chi tiết một bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        [HttpGet("{recordID}")]
        public IActionResult GetRecordByID([FromRoute] Guid recordID)
        {
            try
            {
                var record = _baseBL.GetRecordByID(recordID);
                return StatusCode(StatusCodes.Status200OK, record);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        /// <summary>
        /// Hàm lấy ra mã record tự sinh
        /// </summary>
        /// <returns>Mã bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        [HttpGet("next_value")]
        public IActionResult GetRecordCodeNew()
        {
            try
            {
                var newCode = _baseBL.GetRecordCodeNew();
                return StatusCode(StatusCodes.Status200OK, newCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        /// <summary> 
        /// API trả về danh sách đã lọc và phân trang
        /// <summary>
        /// <param name="offset">Thứ tự bản ghi bắt đầu lấy</param>
        /// <param name="limit">Số lượng bản ghi muốn lấy</param>
        /// <param name="keyword">Từ khoá tìm kiếm</param>
        /// <param name="sort">Trường muốn sắp xếp</param>
        /// <return> Danh sách bản ghi sau khi phân trang, chỉ lấy ra số bản ghi và số trang yêu cầu, và tổng số lượg bản ghi có điều kiện <return>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpGet("fitter")]
        public IActionResult GetFitterRecords([FromQuery] int offset, [FromQuery] int limit, [FromQuery] string? keyword, [FromQuery] string? sort)
        {
            try
            {
                var records = _baseBL.GetFitterRecords(offset, limit, keyword, sort);
                return StatusCode(StatusCodes.Status200OK, records);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        #endregion

        #region API POST

        /// <summary> 
        /// API thêm mới một bản ghi
        /// <summary>
        /// <param name="record">Kiểu dữ liệu bản ghi</param>
        /// <return> ID bản ghi sau khi thêm <return>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpPost]
        public IActionResult InsertRecord([FromBody] T record)
        {
            try
            {
                // Validate dữ liệu đầu vào 
                var properties = typeof(T).GetProperties();
                var validateFailures = new List<string>();
                foreach (var property in properties)
                {
                    string propertyName = property.Name;
                    var propertyValue = property.GetValue(record);
                    var isNotNullOrEmptyAttribute = (IsNotNullOrEmptyAttribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAttribute));
                    if (isNotNullOrEmptyAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                    {
                        validateFailures.Add(isNotNullOrEmptyAttribute.ErrorMessage);
                    }
                }

                if (validateFailures.Count > 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.InvalidInput,
                    Resource.DevMsg_ValidateFailed,
                    Resource.UserMsg_ValidateFailed,
                    validateFailures,
                    HttpContext.TraceIdentifier
                    ));
                }
                Guid result = _baseBL.InsertRecord(record);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (MySqlException mySqlException)
            {
                Console.WriteLine(mySqlException.Message);
                //Duplicate : DuplicateKeyEntry
                //foreign key constraint : NoReferencedRow2
                return StatusCode(StatusCodes.Status400BadRequest, new MisaAmisErrorResult(
                    // nếu trùng mã sẽ trả về lỗi mã lỗi là DuplicateCode
                    mySqlException.Number == (int)MySqlErrorCode.DuplicateKeyEntry ? MisaAmisErrrorCode.DuplicateCode :
                    // nếu truyền khoá ngoại không map với bảng khoá chính sẽ trả mã lỗi InvalidInput
                    mySqlException.Number == (int)MySqlErrorCode.NoReferencedRow2 ? MisaAmisErrrorCode.InvalidInput :
                    MisaAmisErrrorCode.Exception,
                    Resource.DevMsg_InsertFailed,
                    Resource.UserMsg_InsertFailed,
                    Resource.MoreInfo_InsertFailed,
                    HttpContext.TraceIdentifier
                    ));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.Exception,
                    Resource.DevMsg_InsertFailed,
                    Resource.UserMsg_InsertFailed,
                    Resource.MoreInfo_InsertFailed,
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        /// <summary> 
        /// API xoá hàng loạt bản ghi
        /// <summary>
        /// <param name="listRecordID">Danh sách ID bản ghi muốn xoá</param>
        /// <return> danh sách ID bản ghi sau khi xoá <return>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpPost("bulk_delete")]
        public IActionResult BulkDeleteRecord([FromBody] Guid[] listRecordID)
        {
            return StatusCode(StatusCodes.Status200OK, listRecordID);
        }

        #endregion

        #region API PUT

        /// <summary> 
        /// API sửa một bản ghi
        /// <summary>
        /// <param name="recordID">ID bản ghi muốn cập nhật</param>
        /// <param name="record">Kiểu dữ liệu bản ghi cập nhật</param>
        /// <return> ID bản ghi sau khi cập nhật <return>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpPut("{recordID}")]
        public IActionResult UpdateRecord([FromRoute] Guid recordID, [FromBody] T record)
        {
            try
            {
                // Validate dữ liệu đầu vào 
                var properties = typeof(T).GetProperties();
                var validateFailures = new List<string>();
                foreach (var property in properties)
                {
                    string propertyName = property.Name;
                    var propertyValue = property.GetValue(record);
                    var isNotNullOrEmptyAttribute = (IsNotNullOrEmptyAttribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAttribute));
                    if (isNotNullOrEmptyAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                    {
                        validateFailures.Add(isNotNullOrEmptyAttribute.ErrorMessage);
                    }
                }

                if (validateFailures.Count > 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.InvalidInput,
                    Resource.DevMsg_ValidateFailed,
                    Resource.UserMsg_ValidateFailed,
                    validateFailures,
                    HttpContext.TraceIdentifier
                    ));
                }
                var result = _baseBL.UpdateRecord(recordID, record);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (MySqlException mySqlException)
            {
                Console.WriteLine(mySqlException.Message);
                //Duplicate : DuplicateKeyEntry
                //foreign key constraint : NoReferencedRow2
                return StatusCode(StatusCodes.Status400BadRequest, new MisaAmisErrorResult(
                    // nếu trùng mã sẽ trả về lỗi mã lỗi là DuplicateCode
                    mySqlException.Number == (int)MySqlErrorCode.DuplicateKeyEntry ? MisaAmisErrrorCode.DuplicateCode :
                    // nếu truyền khoá ngoại không map với bảng khoá chính sẽ trả mã lỗi InvalidInput
                    mySqlException.Number == (int)MySqlErrorCode.NoReferencedRow2 ? MisaAmisErrrorCode.InvalidInput :
                    MisaAmisErrrorCode.Exception,
                    Resource.DevMsg_InsertFailed,
                    Resource.UserMsg_InsertFailed,
                    Resource.MoreInfo_InsertFailed,
                    HttpContext.TraceIdentifier
                    ));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.Exception,
                    Resource.DevMsg_UpdateFailed,
                    Resource.UserMsg_UpdateFailed,
                    Resource.MoreInfo_Request,
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        #endregion

        #region API DELETE

        /// <summary>
        /// API xoá một bản ghi theo ID
        /// <summary>
        /// <param name="recordID">ID bản ghi</param>
        /// <return> ID bản ghi sau khi xoá <return>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpDelete("{recordID}")]
        public IActionResult DeleteRecord([FromRoute] Guid recordID)
        {
            try
            {
                var result = _baseBL.DeleteRecord(recordID);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new MisaAmisErrorResult(
                    MisaAmisErrrorCode.Exception,
                    Resource.DevMsg_DeleteFailed,
                    Resource.UserMsg_DeleteFailed,
                    Resource.MoreInfo_Request,
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        #endregion

        #endregion
    }
}
