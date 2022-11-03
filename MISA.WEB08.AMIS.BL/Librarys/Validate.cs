using MISA.WEB08.AMIS.Common.Attributes;
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
            var validateFailures = new List<string>();
            foreach (var property in properties)
            {
                // Kiểm tra không được rỗng
                var propertyValue = property.GetValue(record)?.ToString();
                var isNotNullOrEmptyAttribute = (IsNotNullOrEmptyAttribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAttribute));
                if (isNotNullOrEmptyAttribute != null && string.IsNullOrEmpty(propertyValue))
                {
                    validateFailures.Add(isNotNullOrEmptyAttribute.ErrorMessage);
                }
                // Kiểm tra phải đúng định dạng số điện thoại
                var phoneNumberAttribute = (PhoneNumberAttribute?)Attribute.GetCustomAttribute(property, typeof(PhoneNumberAttribute));
                if (phoneNumberAttribute != null && !string.IsNullOrEmpty(propertyValue))
                {
                    if (!Regex.IsMatch(propertyValue, @"(03|02|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b"))
                    {
                        validateFailures.Add(phoneNumberAttribute.ErrorMessage);
                    }
                }
                // kiểm tra phải đúng định dạng email
                var emailAttribute = (EmailAttribute?)Attribute.GetCustomAttribute(property, typeof(EmailAttribute));
                if (emailAttribute != null && !string.IsNullOrEmpty(propertyValue))
                {
                    if (!Regex.IsMatch(propertyValue, @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$"))
                    {
                        validateFailures.Add(emailAttribute.ErrorMessage);
                    }
                }
                // Kiểm tra ngày không được lớn hơn ngày hiện tại
                var maxDateNowAttribute = (MaxDateNowAttribute?)Attribute.GetCustomAttribute(property, typeof(MaxDateNowAttribute));
                if (maxDateNowAttribute != null && !string.IsNullOrEmpty(propertyValue))
                {
                    if (DateTime.Parse(propertyValue) > DateTime.Now)
                    {
                        validateFailures.Add(maxDateNowAttribute.ErrorMessage);
                    }
                }
            }
            if (validateFailures.Count > 0)
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
        /// Hàm xử lý dữ liệu không được trùng
        /// </summary>
        /// <param name="record">Đối tượng cần validate</param>
        /// <param name="guidUpdate">Nếu là update thì sẽ không kiểm tra trùng chính nó</param>
        /// <returns>Danh sách các trường bị trùng</returns>
        /// NK Tiềm 05/10/2022
        public ServiceResponse CheckUnique(T record, Guid? guidUpdate)
        {
            // Kiểm tra trùng dữ liệu đầu vào
            var properties = typeof(T).GetProperties();
            var validateFailures = new List<string>();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(record, null)?.ToString();
                var uniqueAttribute = (UniqueAttribute?)Attribute.GetCustomAttribute(property, typeof(UniqueAttribute));
                if (uniqueAttribute != null && _baseDL.CheckDuplicate(property.Name, propertyValue, guidUpdate))
                {
                    validateFailures.Add(string.Format(uniqueAttribute.ErrorMessage, propertyValue));
                }
            }

            if (validateFailures.Count > 0)
            {
                return new ServiceResponse
                {
                    Success = false,
                    ErrorCode = MisaAmisErrorCode.Duplicate,
                    Data = validateFailures
                };
            }

            return new ServiceResponse
            {
                Success = true
            };
        }

        #endregion
    }
}
