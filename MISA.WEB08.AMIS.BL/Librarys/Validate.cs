using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Enums;
using MISA.WEB08.AMIS.Common.Result;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Validate kiểm tra dữ liệu các trường bắt buộc
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Validate<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Contructor

        public Validate(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Validate dữ liệu truyền lên từ tầng controller
        /// </summary>
        /// <param name="record">Đối tượng cần validate</param>
        /// <returns></returns>
        /// NK Tiềm 05/10/2022
        public static ServiceResponse ValidateData(T record)
        {
            // Validate dữ liệu đầu vào 
            var properties = typeof(T).GetProperties();
            var validateFailures = "";
            foreach (var property in properties)
            {
                // Kiểm tra không được rỗng
                var propertyValue = property.GetValue(record)?.ToString();
                var ValidateAttribute = (ValidateAttribute?)Attribute.GetCustomAttribute(property, typeof(ValidateAttribute));
                if (ValidateAttribute != null)
                {
                    if (ValidateAttribute.IsNotNullOrEmpty && string.IsNullOrEmpty(propertyValue))
                    {
                        validateFailures = ValidateAttribute.ErrorMessage;
                        break;
                    }
                    // Kiểm tra phải đúng định dạng số điện thoại
                    else if (ValidateAttribute.PhoneNumber && !string.IsNullOrEmpty(propertyValue))
                    {
                        if (!Regex.IsMatch(propertyValue, @"(03|02|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b"))
                        {
                            validateFailures = ValidateAttribute.ErrorMessage;
                            break;
                        }
                    }
                    // kiểm tra phải đúng định dạng email
                    else if (ValidateAttribute.Email && !string.IsNullOrEmpty(propertyValue))
                    {
                        if (!Regex.IsMatch(propertyValue, @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$"))
                        {
                            validateFailures = ValidateAttribute.ErrorMessage;
                            break;
                        }
                    }
                    // Kiểm tra ngày không được lớn hơn ngày hiện tại
                    else if (ValidateAttribute.MaxDateNow && !string.IsNullOrEmpty(propertyValue))
                    {
                        if (DateTime.Parse(propertyValue) > DateTime.Now)
                        {
                            validateFailures = ValidateAttribute.ErrorMessage;
                            break;
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(validateFailures))
            {
                return new ServiceResponse
                {
                    Success = false,
                    ErrorCode = MisaAmisErrorCode.InvalidInput,
                    Data = validateFailures
                };
            }
            return new ServiceResponse
            {
                Success = true
            };
        }

        /// <summary>
        /// Hàm xử lý fomat số đúng định dạng
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        /// NK Tiềm 05/10/2022
        public static string FormatNumber(double number)
        {
            return String.Format("{0:0,0.0}", number);
        }

        /// <summary>
        /// Hàm xử lý build truy vấn
        /// </summary>
        /// <param name="key">Column trong data base cần so sánh</param>
        /// <param name="value">Giá trị cần so sánh</param>
        /// <param name="typeSearch">Kiểu so sánh là chữ hay dạng số</param>
        /// <param name="table">Table search</param>
        /// <param name="comparisonType">toán tử so sánh</param>
        /// <returns>truy vấn sau khi build</returns>
        ///  NK Tiềm 05/10/2022
        public static string FormatQuery(string key, string value, string typeSearch, string comparisonType, string table)
        {
            string v_Query = "";
            if ((comparisonType == "=" || comparisonType == ">" || comparisonType == ">=" || comparisonType == "<" || comparisonType == "<=") && (typeSearch == "number" || typeSearch == "date"))
            {
                if (typeSearch == "number")
                {
                    v_Query += $" AND {table}.{key} {comparisonType} {value}";
                }
                else
                {
                    v_Query += $" AND {table}.{key} {comparisonType} STR_TO_DATE('{DateTime.Parse(value).ToString("dd/MM/yyyy")}', '%d/%m/%Y')";
                }
            }
            else if ((comparisonType == "=Null" || comparisonType == "!=Null" || comparisonType == "!=") && (typeSearch == "number" || typeSearch == "date"))
            {
                switch (comparisonType)
                {
                    case "=Null":
                        if (typeSearch == "number")
                        {
                            v_Query += $" AND ({table}.{key} IS NULL OR {table}.{key} = '' AND {table}.{key} != 0)";
                        }
                        else
                        {
                            v_Query += $" AND {table}.{key} IS NULL";
                        }
                        break;
                    case "!=Null":
                        if (typeSearch == "number")
                        {
                            v_Query += $" AND ({table}.{key} != NULL OR {table}.{key} != '' OR {table}.{key} = 0)";
                        }
                        else
                        {
                            v_Query += $" AND {table}.{key} IS NOT NULL";
                        }
                        break;
                    case "!=":
                        if (typeSearch == "number")
                        {
                            v_Query += $" AND ({table}.{key} != {value} OR {table}.{key} = '' OR {table}.{key} IS NULL)";
                        }
                        else
                        {
                            v_Query += $" AND {table}.{key} != STR_TO_DATE('{DateTime.Parse(value).ToString("dd/MM/yyyy")}', '%d/%m/%Y')";
                        }
                        break;
                }
            }
            else
            {
                switch (comparisonType)
                {
                    //Chứa
                    case "%%":
                        v_Query += $" AND {table}.{key} LIKE '%{value}%'";
                        break;
                    //Rỗng
                    case "=Null":
                        v_Query += $" AND ({table}.{key} IS NULL OR {table}.{key} = '')";
                        break;
                    //Không rỗng
                    case "!=Null":
                        v_Query += $" AND ({table}.{key} != NULL OR {table}.{key} != '')";
                        break;
                    //Bằng
                    case "=":
                        v_Query += $" AND {table}.{key} = '{value}'";
                        break;
                    //Khác
                    case "!=":
                        v_Query += $" AND ({table}.{key} != '{value}' OR {table}.{key} = '' OR {table}.{key} IS NULL)";
                        break;
                    //Không chứa
                    case "!%%":
                        v_Query += $" AND ({table}.{key} NOT LIKE '%{value}%' OR {table}.{key} = '' OR {table}.{key} IS NULL)";
                        break;
                    //Bắt đầu bởi
                    case "_%":
                        v_Query += $" AND {table}.{key} LIKE '{value}%'";
                        break;
                    //Kết thúc bởi
                    case "%_":
                        v_Query += $" AND {table}.{key} LIKE '%{value}'";
                        break;
                }
            }
            return v_Query;
        }

        #endregion
    }
}
