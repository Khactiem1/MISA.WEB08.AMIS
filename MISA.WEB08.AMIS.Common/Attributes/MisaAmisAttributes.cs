using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Attributes
{
    /// <summary>
    /// Attributes xác định 1 property là khoá chính
    /// Create by: Nguyễn Khắc Tiềm 23.09.2022
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {

    }

    /// <summary>
    /// Attribute dùng để xác định 1 property không được để trống
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm 23.09.2022
    [AttributeUsage(AttributeTargets.Property)]
    public class IsNotNullOrEmptyAttribute : Attribute
    {
        #region Filed

        /// <summary>
        /// Lỗi trả về cho client
        /// </summary>
        public string ErrorMessage;

        #endregion

        #region Constructor

        /// <summary>
        /// Hàm khởi tạo 
        /// </summary>
        /// <param name="errorMessage"></param>
        public IsNotNullOrEmptyAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        #endregion
    }
}
