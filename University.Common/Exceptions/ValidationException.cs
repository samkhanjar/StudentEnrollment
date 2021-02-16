using System;

namespace University.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public string[] ErrorCodes { get; set; }

        public ValidationException(string[] errorCodes)
        {
            ErrorCodes = errorCodes;
        }

        public ValidationException(string errorCode)
        {
            ErrorCodes = new[] { errorCode };
        }
    }
}
