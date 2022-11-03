using MISA.WEB08.AMIS.Common.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Exceptions
{
    public class ValidateException : Exception
    {
        public IDictionary? DataError { get; set; }

        public MisaAmisErrorCode ErrorCode { get; set; }

        public ValidateException(string message, IDictionary dataError, MisaAmisErrorCode errorCode) : base(message)
        {
            DataError = dataError;
            ErrorCode = errorCode;
        }

        public ValidateException(string message, IDictionary dataError) : base(message)
        {
            DataError = dataError;
            
        }
    }
}
