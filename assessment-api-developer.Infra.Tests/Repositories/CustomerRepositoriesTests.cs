using assessment_api_developer.Domain.Models;
using assessment_api_developer.Infra.Repositories;
using assessment_api_developer.Infra.DataContext;

using Microsoft.EntityFrameworkCore;

namespace assessment_api_developer.Infra.Tests.Repositories
{
    public class CustomerRepositoryTests
    {
        private DbContextOptions<AppDbContext> GetInMemoryDbContextOptions(string dbName)
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }



        [Fact]
        public async Task GetAllAsync_ReturnsAllCustomers()
        {
            // Arrange
            // Create a new in-memory database and seed it with two customers
            var options = GetInMemoryDbContextOptions("GetAllAsyncDb");

            using (var context = new AppDbContext(options))
            {
                context.Customers.AddRange(new Customer { Name = "John Doe" }, new Customer { Name = "Jane Doe" });
                context.SaveChanges();
            }

            // Act
            using (var context = new AppDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var customers = await repository.GetAllAsync();

                // Assert
                Assert.Equal(2, customers.Count());
            }
        }

        [Fact]
        public async Task GetAsync_ReturnsCustomerById()
        {
            // Arrange
            // Create a new in-memory database and seed it with a customer
            var options = GetInMemoryDbContextOptions("GetAsyncDb");

            using (var context = new AppDbContext(options))
            {
                var customer = new Customer { Name = "John Doe" };
                context.Customers.Add(customer);
                context.SaveChanges();
            }

            // Act
            using (var context = new AppDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var customer = await repository.GetAsync(1);

                // Assert
                Assert.NotNull(customer);
                Assert.Equal("John Doe", customer.Name);
            }
        }

        [Fact]
        public async Task AddAsync_AddsCustomer()
        {
            // Arrange
            // Create a new in-memory database
            var options = GetInMemoryDbContextOptions("AddAsyncDb");

            using (var context = new AppDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var customer = new Customer { Name = "John Doe" };

                // Act
                await repository.AddAsync(customer);

                // Assert
                Assert.Equal(1, context.Customers.Count());
                Assert.Equal("John Doe", context.Customers.Single().Name);
            }
        }

        [Fact]
        public async Task UpdateAsync_UpdatesCustomer()
        {
            // Arrange
            // Create a new in-memory database and seed it with a customer
            var options = GetInMemoryDbContextOptions("UpdateAsyncDb");

            using (var context = new AppDbContext(options))
            {
                var customer = new Customer { Name = "John Doe" };
                context.Customers.Add(customer);
                context.SaveChanges();
            }

            // Act
            using (var context = new AppDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var customer = await repository.GetAsync(1);
                customer.Name = "Jane Doe";

                await repository.UpdateAsync(customer);

                // Assert
                Assert.Equal("Jane Doe", context.Customers.Single().Name);
            }
        }

        [Fact]
        public async Task DeleteAsync_DeletesCustomer()
        {
            // Arrange
            // Create a new in-memory database and seed it with a customer
            var options = GetInMemoryDbContextOptions("DeleteAsyncDb");

            using (var context = new AppDbContext(options))
            {
                var customer = new Customer { Name = "John Doe" };
                context.Customers.Add(customer);
                context.SaveChanges();
            }

            // Act
            using (var context = new AppDbContext(options))
            {
                var repository = new CustomerRepository(context);
                await repository.DeleteAsync(1);

                // Assert
                Assert.Equal(0, context.Customers.Count());
            }
        }
    }
}
