using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment_api_developer.Domain.Exceptions
{
    public class CustomerStateZipException : Exception
    {
        public CustomerStateZipException() : base() { }
        public CustomerStateZipException(string message) : base(message) { }
        public CustomerStateZipException(string message, Exception innerException) : base(message, innerException) { }
    }
}
