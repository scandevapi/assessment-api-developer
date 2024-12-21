﻿
namespace assessment_api_developer.Domain.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException() : base() { }
        public CustomerNotFoundException(string message) : base(message) { }
        public CustomerNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
