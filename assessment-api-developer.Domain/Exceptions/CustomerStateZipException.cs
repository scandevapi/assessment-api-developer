
namespace assessment_api_developer.Domain.Exceptions
{
    public class CustomerStateZipException : Exception
    {
        public CustomerStateZipException() : base() { }
        public CustomerStateZipException(string message) : base(message) { }
        public CustomerStateZipException(string message, Exception innerException) : base(message, innerException) { }
    }
}
