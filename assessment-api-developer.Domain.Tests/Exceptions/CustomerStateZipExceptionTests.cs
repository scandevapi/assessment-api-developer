using assessment_api_developer.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment_api_developer.Domain.Tests.Exceptions
{
    public class CustomerStateZipExceptionTests
    {
        [Fact]
        public void ConstructorWithMessage_ShouldInitializeCorrectly()
        {
            var message = "Invalid state or ZIP code.";
            var exception = new CustomerStateZipException(message);
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void ConstructorWithMessageAndInnerException_ShouldInitializeCorrectly()
        {
            var message = "Invalid state or ZIP code.";
            var innerException = new Exception("Inner exception message.");
            var exception = new CustomerStateZipException(message, innerException);

            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}
