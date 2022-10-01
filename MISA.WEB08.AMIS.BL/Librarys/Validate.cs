using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// Validate kiểm tra dữ liệu các trường bắt buộc
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Validate<T>
    {
        /// <summary>
        /// Validate dữ liệu truyền lên từ tầng controller
        /// </summary>
        /// <param name="record">Đối tượng cần validate</param>
        /// <returns></returns>
        public static ServiceResponse ValidateData(T record)
        {
            // Validate dữ liệu đầu vào 
            var properties = typeof(T).GetProperties();
            var validateFailures = new List<string>();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(record);
                var isNotNullOrEmptyAttribute = (IsNotNullOrEmptyAttribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAttribute));
                if (isNotNullOrEmptyAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                {
                    validateFailures.Add(isNotNullOrEmptyAttribute.ErrorMessage);
                }
            }

            if (validateFailures.Count > 0)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = validateFailures
                };
            }

            return new ServiceResponse
            {
                Success = true
            };
        }
    }
}
