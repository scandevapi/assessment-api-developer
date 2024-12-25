using assessment_api_developer.Domain.Exceptions;

namespace assessment_api_developer.Domain.Tests.Exceptions
{
    public class CustomerStateZipExceptionTests
    {

            
        [Fact]
        public void ConstructorWithMessage_ShouldInitializeCorrectly()
        {
            // Arrange
            var message = "Invalid state or ZIP code.";

            // Act
            var exception = new CustomerStateZipException(message);

            // Assert
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void ConstructorWithMessageAndInnerException_ShouldInitializeCorrectly()
        {
            // Arrange
            var message = "Invalid state or ZIP code.";

            // Act
            var innerException = new Exception("Inner exception message.");
            var exception = new CustomerStateZipException(message, innerException);

            // Assert
            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}
