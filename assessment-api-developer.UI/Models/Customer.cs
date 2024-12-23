using System.ComponentModel.DataAnnotations;

namespace assessment_api_developer.UI.Models
{
    public class Customer
    {
        public int ID { get; set; } = 0;

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string? Name { get; set; }
        public string? Address { get; set; }

        //[EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }

        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Invalid phone number format.")] /*(e.g., (123) 456-7890 or 123-456-7890)*/
        public string? Phone { get; set; }
        public string? City { get; set; }

        [RegularExpression(@"^[A-Z][a-z]+(?:[A-Z][a-z]+)*$", ErrorMessage = "State must be a valid full name with each word capitalized and no spaces.")]
        public string? State { get; set; }

        [RegularExpression(@"(^\d{5}(-\d{4})?$)|(^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$)", ErrorMessage = "Invalid postal code format")]
        public string? Zip { get; set; }

        [RegularExpression(@"^[A-Z][a-z]+(?:[A-Z][a-z]+)*$", ErrorMessage = "Country must be a valid full name with each word capitalized and no spaces.")]
        public string? Country { get; set; }
        public string? Notes { get; set; }
        public string? ContactName { get; set; }

        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Invalid phone number format.")] /*(e.g., (123) 456-7890 or 123-456-7890)*/
        public string? ContactPhone { get; set; }

        //[EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address format.")]
        public string? ContactEmail { get; set; }
        public string? ContactTitle { get; set; }
        public string? ContactNotes { get; set; }
    }
}
