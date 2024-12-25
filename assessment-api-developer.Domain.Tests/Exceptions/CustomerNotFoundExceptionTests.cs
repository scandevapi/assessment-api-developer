using assessment_api_developer.Domain.Exceptions;

namespace assessment_api_developer.Domain.Tests.Exceptions
{
    public class CustomerNotFoundExceptionTests
    {


        [Fact]
        public void ConstructorWithMessage_ShouldInitializeCorrectly()
        {
            // Arrange
            var message = "Customer not found.";

            // Act
            var exception = new CustomerNotFoundException(message);

            // Assert
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void ConstructorWithMessageAndInnerException_ShouldInitializeCorrectly()
        {
            // Arrange
            var message = "Customer not found.";

            // Act
            var innerException = new Exception("Inner exception message.");
            var exception = new CustomerNotFoundException(message, innerException);

            // Assert
            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}
