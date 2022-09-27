using MISA.WEB08.AMIS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Result
{
    /// <summary>
    /// Lỗi tuỳ chỉnh
    /// Create by: Nguyễn Khắc Tiềm 21.09.2022  
    /// </summary>
    public class MisaAmisErrorResult
    {
        #region Field
        /// <summary>
        /// Mã lỗi enum
        /// </summary>
        /// Created by : Nguyễn Khắc Tiềm 21.09.2022
        public MisaAmisErrrorCode ErrorCode { get; set; }

        /// <summary>
        /// Mã lỗi cho dev
        /// </summary>
        /// Created by : Nguyễn Khắc Tiềm 21.09.2022
        public string DevMsg { get; set; }

        /// <summary>
        /// Mã lỗi cho người dùng
        /// </summary>
        /// Created by : Nguyễn Khắc Tiềm 21.09.2022
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// </summary>
        /// Created by : Nguyễn Khắc Tiềm 21.09.2022
        public dynamic? MoreInfo { get; set; }

        /// <summary>
        /// ID kết nối để trace sau này để dò lỗi
        /// </summary>
        /// Created by : Nguyễn Khắc Tiềm 21.09.2022
        public string TraceId { get; set; }
        #endregion

        #region Contructor
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="devMsg"></param>
        /// <param name="userMsg"></param>
        /// <param name="moreInfo"></param>
        /// <param name="traceId">ID kết nối</param>
        /// Created by : Nguyễn Khắc Tiềm 21.09.2022
        public MisaAmisErrorResult(MisaAmisErrrorCode errorCode, string devMsg, string userMsg, dynamic? moreInfo, string traceId)
        {
            ErrorCode = errorCode;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = moreInfo;
            TraceId = traceId;
        }
        #endregion

        #region Method
        /// ctrl m o : đóng mở
        /// ctrl k s : thêm region
        #endregion
    }
}
