using assessment_api_developer.Domain.Models;
using assessment_api_developer.Domain.Exceptions;
using assessment_api_developer.Domain.Interfaces;
using assessment_api_developer.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Moq;

namespace assessment_api_developer.API.Tests.Controllers
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
            // Arrange
            var customers = new List<Customer>
            {
                new Customer {ID=1, Name = "Customer One"},
                new Customer {ID=1, Name = "Customer Two"}
            };
            _mockService.Setup(s => s.GetAllCustomersAsync()).ReturnsAsync(customers);

            // Act
            var result = await _controller.GetAllCustomers() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customers, result.Value);

        }

        //[Fact]
        //public async Task GetAllCustomers_ShouldReturnInternalServerError_OnException()
        //{
        //    // Arrange
        //    _mockService.Setup(s => s.GetAllCustomersAsync()).ThrowsAsync(new Exception());

        //    // Act
        //    var result = await _controller.GetAllCustomers() as ObjectResult;

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        //}

        [Fact]
        public async Task GetCustomer_ShouldReturnOkObjectResult()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One" };
            _mockService.Setup(s => s.GetCustomerAsync(customer.ID)).ReturnsAsync(customer);

            // Act
            var result = await _controller.GetCustomer(customer.ID) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customer, result.Value);
        }

        [Fact]
        public async Task GetCustomer_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetCustomerAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);

            // Act
            var result = await _controller.GetCustomer(1) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddCustomer_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "UnitedStates", State = "California", Zip = "12345" };
            _mockService.Setup(s => s.AddCustomerAsync(It.IsAny<Customer>())).Verifiable();

            // Act
            var result = await _controller.AddCustomer(customer) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nameof(_controller.GetCustomer), result.ActionName);
        }

        [Fact]
        public async Task AddCustomer_ShouldReturnBadRequest_OnCustomerStateZipException()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "UnitedStates", State = "InvalidState", Zip = "12345" };
            _mockService.Setup(s => s.AddCustomerAsync(It.IsAny<Customer>())).ThrowsAsync(new CustomerStateZipException("Invalid state or province for the selected country."));

            // Act
            var result = await _controller.AddCustomer(customer) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid state or province for the selected country.", result.Value);
        }

        [Fact]
        public async Task AddCustomer_WithoutZipCode_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One" };
            _mockService.Setup(s => s.AddCustomerAsync(It.IsAny<Customer>())).Verifiable();

            // Act
            var result = await _controller.AddCustomer(customer) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nameof(_controller.GetCustomer), result.ActionName);
        }

        [Fact]
        public async Task AddCustomer_ShouldReturnBadRequest_OnInvalidModelState()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Required");
            var customer = new Customer { ID = 1, Country = "UnitedStates", State = "California", Zip = "12345" };

            // Act
            var result = await _controller.AddCustomer(customer) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateCustomer_ShouldReturnNoContentResult()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "UnitedStates", State = "California", Zip = "12345" };
            _mockService.Setup(s => s.GetCustomerAsync(customer.ID)).ReturnsAsync(new Customer { ID = 1 });
            _mockService.Setup(s => s.UpdateCustomerAsync(It.IsAny<Customer>())).Verifiable();

            // Act
            var result = await _controller.UpdateCustomer(customer.ID, customer) as NoContentResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateCustomer_ShouldReturnBadRequest_OnMismatchedId()
        {
            // Arrange
            var customer = new Customer { ID = 2, Name = "Customer One", Country = "UnitedStates", State = "California", Zip = "12345" };

            // Act
            var result = await _controller.UpdateCustomer(1, customer) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateCustomer_WithoutZipCode_ShouldReturnNoContentResult()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One" };
            _mockService.Setup(s => s.GetCustomerAsync(customer.ID)).ReturnsAsync(new Customer { ID = 1 });
            _mockService.Setup(s => s.UpdateCustomerAsync(It.IsAny<Customer>())).Verifiable();

            // Act
            var result = await _controller.UpdateCustomer(customer.ID, customer) as NoContentResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldReturnNoContentResult()
        {
            // Arrange
            _mockService.Setup(s => s.GetCustomerAsync(1)).ReturnsAsync(new Customer { ID = 1 });
            _mockService.Setup(s => s.DeleteCustomerAsync(1)).Verifiable();

            // Act
            var result = await _controller.DeleteCustomer(1) as NoContentResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteCustomerAsync(It.IsAny<int>())).ThrowsAsync(new CustomerNotFoundException("Customer not found"));

            // Act
            var result = await _controller.DeleteCustomer(1) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Customer not found", result.Value);
        }
    }
}
