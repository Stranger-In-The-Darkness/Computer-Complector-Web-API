using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models.Exceptions
{
    public class ValidationException : Exception
    {
        public string ParameterName { get; private set; }

        public ValidationException(string message, string paramName) : base(message)
        {
            ParameterName = paramName;
        }

        public ValidationException(string message, Exception innerException, string paramName) : base(message, innerException)
        {
            ParameterName = paramName;
        }
    }
}
