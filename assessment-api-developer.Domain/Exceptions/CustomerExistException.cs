
namespace assessment_api_developer.Domain.Exceptions
{
    public class CustomerExistException : Exception
    {
        public CustomerExistException() : base() { }
        public CustomerExistException(string message) : base(message) { }
        public CustomerExistException(string message, Exception innerException) : base(message, innerException) { }
    }
}
