using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Enums
{
    /// <summary>
    /// Mã lỗi
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm 21.09.2022
    public enum MisaAmisErrrorCode : int
    {
        /// <summary>
        /// Lỗi do ngoại lệ
        /// </summary>
        /// Created by : Khắc Tiềm 21.09.2022
        Exception = 1,

        /// <summary>
        /// Lỗi mã trùng
        /// </summary>
        /// Created by : Khắc Tiềm 21.09.2022
        DuplicateCode = 2,

        /// <summary>
        /// Lỗi mã để trống
        /// </summary>
        /// Created by : Khắc Tiềm 21.09.2022
        EmptyCode = 3,

        /// <summary>
        /// Lỗi sai dữ liệu đầu vào
        /// </summary>
        /// Created by : Khắc Tiềm 21.09.2022
        InvalidInput = 4,

        /// <summary>
        /// Mã lỗi trùng khi trường đặt là primarykey hoặc unique
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        DuplicateEntry = 1062,
    }
}
