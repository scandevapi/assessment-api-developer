using assessment_api_developer.Domain.Models;

namespace assessment_api_developer.Domain.Tests.Models
{
    public class CustomerModelTests
    {
        [Fact]
        public void CustomerModel_ShouldInitializeCorrectly()
        {
            var customer = new Customer() 
            { 
                ID= 1, 
                Name= "Name",
                Address = "Address",
                Email = "Email",
                Phone = "Phone",
                City = "City",
                State = "State",
                Zip = "Zip",
                Country = "Country",
                Notes = "Notes",
                ContactName = "ContactName",
                ContactPhone = "ContactPhone",
                ContactEmail = "ContactEmail",
                ContactTitle = "ContactTitle",
                ContactNotes = "ContactNotes", 
            };

            Assert.Equal(1, customer.ID);
            Assert.Equal("Name", customer.Name);
            Assert.Equal("Address", customer.Address);
            Assert.Equal("Email", customer.Email);
            Assert.Equal("Phone", customer.Phone);
            Assert.Equal("City", customer.City);
            Assert.Equal("State", customer.State);
            Assert.Equal("Zip", customer.Zip);
            Assert.Equal("Country", customer.Country);
            Assert.Equal("Notes", customer.Notes);
            Assert.Equal("ContactName", customer.ContactName);
            Assert.Equal("ContactPhone", customer.ContactPhone);
            Assert.Equal("ContactEmail", customer.ContactEmail);
            Assert.Equal("ContactTitle", customer.ContactTitle);
            Assert.Equal("ContactNotes", customer.ContactNotes);
        }
    }
}
