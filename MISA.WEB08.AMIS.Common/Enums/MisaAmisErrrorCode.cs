﻿using System;
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
        /// Lỗi trùng các trường
        /// </summary>
        /// Created by : Khắc Tiềm 21.09.2022
        Duplicate = 2,

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
        /// Mã lỗi xoá nhiều bản ghi thất bại
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        DeleteMultiple = 5,

        /// <summary>
        /// Mã lỗi thêm mới không thành công
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        InsertFailed = 6,

        /// <summary>
        /// Mã lỗi sửa không thành công
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        UpdateFailed = 6,

        /// <summary>
        /// Mã lỗi xoá không thành công
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        DeleteFailed = 7,
    }
}
