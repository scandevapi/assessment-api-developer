//Requaired Nuget Packages
//FluentValidation, FluentValidation.DependencyInjectionExtensions, FluentValidation.AspNetCore

using assessment_api_developer.Domain.Models;
using FluentValidation;

namespace assessment_api_developer.API.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Name can only contain letters and spaces.");

            RuleFor(x => x.Email)
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("Invalid email address format.");

            RuleFor(x => x.Phone)
                .Matches(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$")
                .WithMessage("Invalid phone number format.");

            RuleFor(x => x.State)
                .Matches(@"^[A-Z][a-z]+(?:[A-Z][a-z]+)*$")
                .WithMessage("State must be a valid full name with each word capitalized and no spaces.");

            RuleFor(x => x.Zip)
                .Matches(@"(^\d{5}(-\d{4})?$)|(^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$)")
                .WithMessage("Invalid postal code format.");

            RuleFor(x => x.Country)
                .Matches(@"^[A-Z][a-z]+(?:[A-Z][a-z]+)*$")
                .WithMessage("Country must be a valid full name with each word capitalized and no spaces.");

            RuleFor(x => x.ContactPhone)
                .Matches(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$")
                .WithMessage("Invalid phone number format.");

            RuleFor(x => x.ContactEmail)
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("Invalid email address format.");
        }
    }
}
