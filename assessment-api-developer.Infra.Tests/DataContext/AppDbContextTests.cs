using assessment_api_developer.Domain.Models;
using assessment_api_developer.Infra.DataContext;

using Microsoft.EntityFrameworkCore;

namespace assessment_api_developer.Infra.Tests.DataContext
{
    public class AppDbContextTests
    {


        [Fact]
        public void CanInsertCustomerIntoDatabase()
        {
            // In-memory database only exists while the connection is open
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_DbContext")
                .Options;

            // Act
            using (var context = new AppDbContext(options))
            {
                var customer = new Customer { Name = "Customer One DbContext" };
                context.Customers.Add(customer);
                context.SaveChanges();
            }

            // Assert
            using (var context = new AppDbContext(options))
            {
                Assert.Equal(1, context.Customers.Count());
                Assert.Equal("Customer One DbContext", context.Customers.Single().Name);
            }
        }
    }
}
