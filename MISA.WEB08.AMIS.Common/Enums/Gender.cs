using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Enums
{
    /// <summary>
    /// Giới tính 
    /// Enum giới tính của thực thể người, 3 loại nam, nữ, khác
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public enum Gender:int
    {
        /// <summary>
        /// nam
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        Male = 0,

        /// <summary>
        /// nữ
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        Female = 1,

        /// <summary>
        /// khác
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        Other = 2,

    }
}
