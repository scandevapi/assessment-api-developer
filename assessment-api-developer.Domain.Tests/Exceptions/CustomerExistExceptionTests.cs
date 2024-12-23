using assessment_api_developer.Domain.Exceptions;

namespace assessment_api_developer.Domain.Tests.Exceptions
{
    public class CustomerExistExceptionTests
    {
        [Fact]
        public void ConstructorWithMessage_ShouldInitializeCorrectly()
        {
            var message = "Customer already exists.";
            var exception = new CustomerExistException(message);
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void ConstructorWithMessageAndInnerException_ShouldInitializeCorrectly()
        {
            var message = "Customer already exists.";
            var innerException = new Exception("Inner exception message.");
            var exception = new CustomerExistException(message, innerException);

            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}
