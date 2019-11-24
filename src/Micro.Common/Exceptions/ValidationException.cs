using System;

namespace Micro.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string code)
        {
            Code = code;
        }

        public ValidationException(string code, string message) : base(message)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
