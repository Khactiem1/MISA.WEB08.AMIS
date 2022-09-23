using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.API.Enums
{
    /// <summary>
    /// Mã lỗi
    /// Create by: Nguyễn Khắc Tiềm 21.09.2022
    /// </summary>
    public enum MisaAmisErrrorCode : int
    {
        /// <summary>
        /// Lỗi do ngoại lệ
        /// </summary>
        /// Created by : Khắc Tiềm 21.09.2022
        Exception = 1,

        /// <summary>
        /// Lỗi mã employee_id
        /// </summary>
        /// Created by : Khắc Tiềm 21.09.2022
        DuplicateCode = 2,

        /// <summary>
        /// Lỗi mã (employee_id) để trống
        /// </summary>
        /// Created by : Khắc Tiềm 21.09.2022
        EmptyCode = 3,
    }
}
