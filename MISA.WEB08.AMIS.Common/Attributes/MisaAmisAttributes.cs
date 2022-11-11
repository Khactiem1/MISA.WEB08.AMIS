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
    /// Attributes xác định 1 không được trùng
    /// Create by: Nguyễn Khắc Tiềm 23.09.2022
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueAttribute : Attribute
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
        public UniqueAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        #endregion
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

    /// <summary>
    /// Attribute column tên của Property
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm 23.09.2022
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnName : Attribute
    {
        #region Filed

        /// <summary>
        /// Tên column excel
        /// </summary>
        public string Name;

        /// <summary>
        /// Độ rộng column
        /// </summary>
        public int Width;

        /// <summary>
        /// Có phải ngày hay không
        /// </summary>
        public bool IsDate;

        /// <summary>
        /// Có phải giới tính hay không
        /// </summary>
        public bool IsGender;

        /// <summary>
        /// Là số hay không
        /// </summary>
        public bool IsNumber;

        #endregion

        #region Constructor

        /// <summary>
        /// Hàm khởi tạo 
        /// </summary>
        /// <param name="name"></param>
        public ColumnName(string name, int width, bool isDate, bool isGender, bool isNumber)
        {
            Name = name;
            Width = width;
            IsDate = isDate;
            IsGender = isGender;
            IsNumber = isNumber;
        }

        #endregion
    }

    /// <summary>
    /// Attribute xác định trường là số điện thoại
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm 23.09.2022
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneNumberAttribute : Attribute
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
        public PhoneNumberAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        #endregion
    }

    /// <summary>
    /// Attribute xác định trường là email
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm 23.09.2022
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailAttribute : Attribute
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
        public EmailAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        #endregion
    }

    /// <summary>
    /// Attribute xác định trường là Date và có điều kiện đầu vào
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm 23.09.2022
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxDateNowAttribute : Attribute
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
        public MaxDateNowAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        #endregion
    }
}
