using System.ComponentModel.DataAnnotations;

namespace assessment_api_developer.Domain.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Country { get; set; }
        public string? Notes { get; set; }
        public string? ContactName { get; set; }
        public string? ContactPhone { get; set; }

        [EmailAddress]
        public string? ContactEmail { get; set; }
        public string? ContactTitle { get; set; }
        public string? ContactNotes { get; set; }
    }
}
