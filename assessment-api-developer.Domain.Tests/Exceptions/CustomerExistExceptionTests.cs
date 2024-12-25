using assessment_api_developer.Domain.Exceptions;

namespace assessment_api_developer.Domain.Tests.Exceptions
{
    public class CustomerExistExceptionTests
    {


        [Fact]
        public void ConstructorWithMessage_ShouldInitializeCorrectly()
        {
            // Arrange
            var message = "Customer already exists.";

            // Act
            var exception = new CustomerExistException(message);

            // Assert
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void ConstructorWithMessageAndInnerException_ShouldInitializeCorrectly()
        {
            // Arrange
            var message = "Customer already exists.";

            // Act
            var innerException = new Exception("Inner exception message.");
            var exception = new CustomerExistException(message, innerException);

            // Assert
            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}
