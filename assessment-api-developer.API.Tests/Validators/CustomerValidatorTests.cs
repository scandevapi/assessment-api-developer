using assessment_api_developer.API.Validators;
using assessment_api_developer.Domain.Models;
using FluentValidation.TestHelper;

namespace assessment_api_developer.API.Tests.Validators
{
    public class CustomerValidatorTests
    {
        private readonly CustomerValidator _validator;

        public CustomerValidatorTests()
        {
            _validator = new CustomerValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var customer = new Customer { Name = string.Empty };
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(c => c.Name).WithErrorMessage("Name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Name_Has_Invalid_Characters()
        {
            var customer = new Customer { Name = "John123" };
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(c => c.Name).WithErrorMessage("Name can only contain letters and spaces.");
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            var customer = new Customer { Email = "invalid-email" };
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(c => c.Email).WithErrorMessage("Invalid email address format.");
        }

        [Fact]
        public void Should_Have_Error_When_Phone_Is_Invalid()
        {
            var customer = new Customer { Phone = "12345" };
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(c => c.Phone).WithErrorMessage("Invalid phone number format.");
        }

        [Fact]
        public void Should_Have_Error_When_State_Is_Invalid()
        {
            var customer = new Customer { State = "invalidstate" };
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(c => c.State).WithErrorMessage("State must be a valid full name with each word capitalized and no spaces.");
        }

        [Fact]
        public void Should_Have_Error_When_Zip_Is_Invalid()
        {
            var customer = new Customer { Zip = "123" };
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(c => c.Zip).WithErrorMessage("Invalid postal code format.");
        }

        [Fact]
        public void Should_Have_Error_When_Country_Is_Invalid()
        {
            var customer = new Customer { Country = "invalidcountry" };
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(c => c.Country).WithErrorMessage("Country must be a valid full name with each word capitalized and no spaces.");
        }

        [Fact]
        public void Should_Have_Error_When_ContactPhone_Is_Invalid()
        {
            var customer = new Customer { ContactPhone = "12345" };
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(c => c.ContactPhone).WithErrorMessage("Invalid phone number format.");
        }

        [Fact]
        public void Should_Have_Error_When_ContactEmail_Is_Invalid()
        {
            var customer = new Customer { ContactEmail = "invalid-email" };
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail).WithErrorMessage("Invalid email address format.");
        }
    }
}
