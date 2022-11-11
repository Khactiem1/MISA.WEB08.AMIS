using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
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
            var recordList = _baseBL.GetAllRecords();
            return StatusCode(StatusCodes.Status200OK, new ServiceResponse
            {
                Success = true,
                Data = recordList
            });
        }

        /// <summary>
        /// API lấy ra danh sách tất bản ghi đang hoạt động trong 1 bảng
        /// <summary>
        /// <return> Danh sách tất cả bản ghi <return>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpGet("active")]
        public IActionResult GetAllRecordActive()
        {
            var recordList = _baseBL.GetAllRecordActive();
            return StatusCode(StatusCodes.Status200OK, new ServiceResponse
            {
                Success = true,
                Data = recordList
            });
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
            var record = _baseBL.GetRecordByID(recordID);
            return StatusCode(StatusCodes.Status200OK, new ServiceResponse
            {
                Success = true,
                Data = record
            });
        }

        /// <summary>
        /// Hàm lấy ra mã record tự sinh
        /// </summary>
        /// <returns>Mã bản ghi</returns>
        /// Create by: Nguyễn Khắc Tiềm (26/09/2022)
        [HttpGet("next_value")]
        public IActionResult GetRecordCodeNew()
        {
            var newCode = _baseBL.GetRecordCodeNew();
            return StatusCode(StatusCodes.Status200OK, new ServiceResponse
            {
                Success = true,
                Data = newCode
            });
        }

        /// <summary> 
        /// API trả về danh sách đã lọc và phân trang
        /// <summary>
        /// <param name="formData">Trường muốn filter và sắp xếp</param>
        /// <return> Danh sách bản ghi sau khi phân trang, chỉ lấy ra số bản ghi và số trang yêu cầu, và tổng số lượg bản ghi có điều kiện <return>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpPost("fitter")]
        public IActionResult GetFitterRecords([FromBody] Dictionary<string, object> formData)
        {
            var records = _baseBL.GetFitterRecords(formData);
            return StatusCode(StatusCodes.Status200OK, new ServiceResponse
            {
                Success = true,
                Data = records
            });
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
            var result = _baseBL.InsertRecord(record);
            if (result.Success)
            {
                return StatusCode(StatusCodes.Status201Created, new ServiceResponse
                {
                    Success = true,
                    Data = result.Data
                });
            }
            else
            {
                MisaAmisErrorCode errrorCode;
                if (result.ErrorCode == MisaAmisErrorCode.Duplicate)
                {
                    errrorCode = MisaAmisErrorCode.Duplicate;
                }
                else if (result.ErrorCode == MisaAmisErrorCode.InsertFailed)
                {
                    errrorCode = MisaAmisErrorCode.InsertFailed;
                }
                else
                {
                    errrorCode = MisaAmisErrorCode.InvalidInput;
                }
                return StatusCode(StatusCodes.Status200OK, new ServiceResponse
                {
                    Success = false,
                    ErrorCode = errrorCode,
                    Data = new MisaAmisErrorResult(
                            errrorCode,
                            Resource.DevMsg_ValidateFailed,
                            result.Data,
                            Resource.MoreInfo_Exception,
                            HttpContext.TraceIdentifier
                        )
                });
            }
        }

        /// <summary> 
        /// API xoá hàng loạt bản ghi
        /// <summary>
        /// <param name="listRecordID">Danh sách ID bản ghi muốn xoá</param>
        /// <return> danh sách ID bản ghi sau khi xoá <return>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        [HttpPost("bulk_delete")]
        public IActionResult DeleteMultiple([FromBody] Guid[] listRecordID)
        {
            var result = _baseBL.DeleteMultiple(listRecordID);
            if (result.Success)
            {
                return StatusCode(StatusCodes.Status200OK, new ServiceResponse
                {
                    Success = true,
                    Data = result
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new ServiceResponse
                {
                    Success = false,
                    ErrorCode = MisaAmisErrorCode.DeleteMultiple,
                    Data = new MisaAmisErrorResult(
                            MisaAmisErrorCode.DeleteMultiple,
                            Resource.DevMsg_DeleteMultipleFailed.ToString(),
                            Resource.UserMsg_DeleteMultipleFailed,
                            Resource.MoreInfo_Exception,
                            HttpContext.TraceIdentifier
                        )
                });
            }
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
            var result = _baseBL.UpdateRecord(recordID, record);
            if (result.Success)
            {
                return StatusCode(StatusCodes.Status200OK, new ServiceResponse
                {
                    Success = true,
                    Data = result.Data
                });
            }
            else
            {
                MisaAmisErrorCode errrorCode;
                if (result.ErrorCode == MisaAmisErrorCode.Duplicate)
                {
                    errrorCode = MisaAmisErrorCode.Duplicate;
                }
                else if (result.ErrorCode == MisaAmisErrorCode.InsertFailed)
                {
                    errrorCode = MisaAmisErrorCode.InsertFailed;
                }
                else
                {
                    errrorCode = MisaAmisErrorCode.InvalidInput;
                }
                return StatusCode(StatusCodes.Status200OK, new ServiceResponse
                {
                    Success = false,
                    ErrorCode = errrorCode,
                    Data = new MisaAmisErrorResult(
                            errrorCode,
                            Resource.DevMsg_ValidateFailed,
                            result.Data,
                            Resource.MoreInfo_Exception,
                            HttpContext.TraceIdentifier
                        )
                });
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
            var result = _baseBL.DeleteRecord(recordID);
            if (result.Success)
            {
                return StatusCode(StatusCodes.Status200OK, new ServiceResponse
                {
                    Success = true,
                    Data = result
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new ServiceResponse
                {
                    Success = false,
                    ErrorCode = result.ErrorCode,
                    Data = new MisaAmisErrorResult(
                            (MisaAmisErrorCode)result.ErrorCode,
                            Resource.DevMsg_DeleteFailed.ToString(),
                            result.Data,
                            Resource.MoreInfo_Exception,
                            HttpContext.TraceIdentifier
                        )
                });
            }
        }

        /// <summary>
        /// Toggle active
        /// </summary>
        /// <returns>ID bản ghi </returns>
        /// CreatedBy: Nguyễn Khắc Tiềm (6/10/2022)
        [HttpGet("ToggleActive/{recordID}")]
        public IActionResult ToggleActive([FromRoute] Guid recordID)
        {
            var result = _baseBL.ToggleActive(recordID);
            if (result.Success)
            {
                return StatusCode(StatusCodes.Status200OK, new ServiceResponse
                {
                    Success = true,
                    Data = result.Data
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new ServiceResponse
                {
                    Success = false,
                    ErrorCode = MisaAmisErrorCode.Exception,
                    Data = new MisaAmisErrorResult(
                            MisaAmisErrorCode.Exception,
                            Resource.DevMsg_Exception,
                            result.Data,
                            Resource.MoreInfo_Exception,
                            HttpContext.TraceIdentifier
                        )
                });
            }
        }

        #endregion

        #endregion
    }
}