using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Exceptions
{
    public class ApiException
    {
        public ApiException(int statusCode, string message = null, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        private int StatusCode { get; }
        private string Message { get; }
        private string Details { get; }
    }
}