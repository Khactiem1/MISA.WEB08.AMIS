using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.API.Enums
{
    /// <summary>
    /// Lỗi tuỳ chỉnh
    /// Create by: Nguyễn Khắc Tiềm 21.09.2022
    /// </summary>
    public class MisaAmisErrorResult
    {
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
        public string MoreInfo { get; set; }

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="devMsg"></param>
        /// <param name="userMsg"></param>
        /// <param name="moreInfo"></param>
        /// Created by : Nguyễn Khắc Tiềm 21.09.2022
        public MisaAmisErrorResult(MisaAmisErrrorCode errorCode, string devMsg, string userMsg, string moreInfo)
        {
            ErrorCode = errorCode;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = moreInfo;
        }
    }
}
