using assessment_api_developer.Domain.Models;
using assessment_api_developer.Infra.DataContext;
using assessment_api_developer.Infra.Repositories;
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
            var options = GetInMemoryDbContextOptions("GetAllAsyncDb");

            using (var context = new AppDbContext(options))
            {
                context.Customers.AddRange(new Customer { Name = "John Doe" }, new Customer { Name = "Jane Doe" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var customers = await repository.GetAllAsync();

                Assert.Equal(2, customers.Count());
            }
        }

        [Fact]
        public async Task GetAsync_ReturnsCustomerById()
        {
            var options = GetInMemoryDbContextOptions("GetAsyncDb");

            using (var context = new AppDbContext(options))
            {
                var customer = new Customer { Name = "John Doe" };
                context.Customers.Add(customer);
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var customer = await repository.GetAsync(1);

                Assert.NotNull(customer);
                Assert.Equal("John Doe", customer.Name);
            }
        }

        [Fact]
        public async Task AddAsync_AddsCustomer()
        {
            var options = GetInMemoryDbContextOptions("AddAsyncDb");

            using (var context = new AppDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var customer = new Customer { Name = "John Doe" };

                await repository.AddAsync(customer);

                Assert.Equal(1, context.Customers.Count());
                Assert.Equal("John Doe", context.Customers.Single().Name);
            }
        }

        [Fact]
        public async Task UpdateAsync_UpdatesCustomer()
        {
            var options = GetInMemoryDbContextOptions("UpdateAsyncDb");

            using (var context = new AppDbContext(options))
            {
                var customer = new Customer { Name = "John Doe" };
                context.Customers.Add(customer);
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var customer = await repository.GetAsync(1);
                customer.Name = "Jane Doe";

                await repository.UpdateAsync(customer);

                Assert.Equal("Jane Doe", context.Customers.Single().Name);
            }
        }

        [Fact]
        public async Task DeleteAsync_DeletesCustomer()
        {
            var options = GetInMemoryDbContextOptions("DeleteAsyncDb");

            using (var context = new AppDbContext(options))
            {
                var customer = new Customer { Name = "John Doe" };
                context.Customers.Add(customer);
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new CustomerRepository(context);
                await repository.DeleteAsync(1);

                Assert.Equal(0, context.Customers.Count());
            }
        }
    }
}
