using MISA.WEB08.AMIS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Result
{
    /// <summary>
    /// Custom dữ liệu trả về từ tầng BL DL
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public class ServiceResponse
    {
        #region Field

        /// <summary>
        /// trả về true hoặc false (thành công hoặc thất bại)
        /// </summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        public bool Success { get; set; }

        /// <summary>
        /// Mã lỗi đi kèm
        /// </summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        public MisaAmisErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Dữ liệu đi kèm
        /// </summary>
        /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
        public dynamic? Data { get; set; }

        #endregion
    }
}
