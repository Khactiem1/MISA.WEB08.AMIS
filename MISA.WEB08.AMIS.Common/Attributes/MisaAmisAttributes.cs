using System;

namespace MISA.WEB08.AMIS.Common.Attributes
{
    /// <summary>
    /// Attributes xác định cần validate chung
    /// Create by: Nguyễn Khắc Tiềm 23.09.2022
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateAttribute : Attribute
    {
        /// <summary>
        /// Lỗi trả về cho client
        /// </summary>
        public string ErrorMessage = "";

        /// <summary>
        /// xác định 1 property là khoá chính
        /// </summary>
        public bool PrimaryKey = false;

        /// <summary>
        /// xác định 1 property không được để trống
        /// </summary>
        public bool IsNotNullOrEmpty = false;

        /// <summary>
        /// xác định trường là số điện thoại
        /// </summary>
        public bool PhoneNumber = false;

        /// <summary>
        /// xác định trường là email
        /// </summary>
        public bool Email = false;

        /// <summary>
        /// xác định trường là Date và có điều kiện đầu vào ngày không được lớn hơn ngày hiện tại
        /// </summary>
        public bool MaxDateNow = false;

        /// <summary>
        /// Xác định độ dài tối đa của cột
        /// </summary>
        public int MaxLength = -1;
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
        public string Name = "";

        /// <summary>
        /// Độ rộng column
        /// </summary>
        public int Width = 0;

        /// <summary>
        /// Có phải ngày hay không
        /// </summary>
        public bool IsDate = false;

        /// <summary>
        /// Có phải giới tính hay không
        /// </summary>
        public bool IsGender = false;

        /// <summary>
        /// Là số hay không
        /// </summary>
        public bool IsNumber = false;

        /// <summary>
        /// Là kiểu có hay không
        /// </summary>
        public bool IsBollen = false;

        #endregion
    }
}
