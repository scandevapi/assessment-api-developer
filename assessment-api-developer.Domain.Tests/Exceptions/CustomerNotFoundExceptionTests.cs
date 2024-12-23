using assessment_api_developer.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment_api_developer.Domain.Tests.Exceptions
{
    public class CustomerNotFoundExceptionTests
    {
        [Fact]
        public void ConstructorWithMessage_ShouldInitializeCorrectly()
        {
            var message = "Customer not found.";
            var exception = new CustomerNotFoundException(message);
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void ConstructorWithMessageAndInnerException_ShouldInitializeCorrectly()
        {
            var message = "Customer not found.";
            var innerException = new Exception("Inner exception message.");
            var exception = new CustomerNotFoundException(message, innerException);

            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}
