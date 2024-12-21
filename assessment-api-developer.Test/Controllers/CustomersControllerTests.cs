using assessment_api_developer.API.Controllers;
using assessment_api_developer.Domain.Interfaces;
using assessment_api_developer.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace assessment_api_developer.Test.Services
{
    public class CustomersControllerTests
    {
        private readonly Mock<ICustomerService> _mockService;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _mockService = new Mock<ICustomerService>();
            _controller = new CustomersController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllCustomers_ShouldReturnokObjectResult()
        {
            var customers = new List<Customer>
            {
                new Customer {ID=1, Name = "Customer One"},
                new Customer {ID=1, Name = "Customer Two"}
            };
            _mockService.Setup(s => s.GetAllCustomersAsync()).ReturnsAsync(customers);

            var result = await _controller.GetAllCustomers() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(customers, result.Value);

        }

        [Fact]
        public async Task GetCustomer_ShouldReturnOkObjectResult()
        {
            var customer = new Customer { ID = 1, Name = "Customer One" };
            _mockService.Setup(s => s.GetCustomerAsync(customer.ID)).ReturnsAsync(customer);

            var result = await _controller.GetCustomer(customer.ID) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(customer, result.Value);
        }

        [Fact]
        public async Task AddCustomer_ShouldReturnCreatedAtAction()
        {
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "UnitedStates", State = "California", Zip = "12345" };
            _mockService.Setup(s => s.AddCustomerAsync(It.IsAny<Customer>())).Verifiable();

            var result = await _controller.AddCustomer(customer) as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(nameof(_controller.GetCustomer), result.ActionName);
        }

        [Fact]
        public async Task AddCustomer_WithoutZipCode_ShouldReturnCreatedAtAction()
        {
            var customer = new Customer { ID = 1, Name = "Customer One" };
            _mockService.Setup(s => s.AddCustomerAsync(It.IsAny<Customer>())).Verifiable();

            var result = await _controller.AddCustomer(customer) as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(nameof(_controller.GetCustomer), result.ActionName);
        }

        [Fact]
        public async Task UpdateCustomer_ShouldReturnNoContentResult()
        {
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "UnitedStates", State = "California", Zip = "12345" };
            _mockService.Setup(s => s.GetCustomerAsync(customer.ID)).ReturnsAsync(new Customer { ID = 1 });
            _mockService.Setup(s => s.UpdateCustomerAsync(It.IsAny<Customer>())).Verifiable();

            var result = await _controller.UpdateCustomer(customer.ID, customer) as NoContentResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateCustomer_WithoutZipCode_ShouldReturnNoContentResult()
        {
            var customer = new Customer { ID = 1, Name = "Customer One"};
            _mockService.Setup(s => s.GetCustomerAsync(customer.ID)).ReturnsAsync(new Customer { ID = 1 });
            _mockService.Setup(s => s.UpdateCustomerAsync(It.IsAny<Customer>())).Verifiable();

            var result = await _controller.UpdateCustomer(customer.ID, customer) as NoContentResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldReturnNoContentResult()
        {
            _mockService.Setup(s => s.GetCustomerAsync(1)).ReturnsAsync(new Customer { ID = 1 });
            _mockService.Setup(s => s.DeleteCustomerAsync(1)).Verifiable();

            var result = await _controller.DeleteCustomer(1) as NoContentResult;

            Assert.NotNull(result);
        }
    }
}