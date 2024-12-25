using assessment_api_developer.Domain.Models;
using assessment_api_developer.Domain.Exceptions;
using assessment_api_developer.Services.Services;
using assessment_api_developer.Domain.Interfaces;

using Moq;

namespace assessment_api_developer.Services.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockRepository;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_mockRepository.Object);
        }



        [Fact]
        public async Task GetAllCustomersAsync_ShouldReturnAllCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { ID=1, Name="Customer One" },
                new Customer { ID=2, Name="Customer Two" }
            };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(customers);

            // Act
            var result = await _customerService.GetAllCustomersAsync();

            // Assert
            Assert.Equal(customers, result);
        }

        [Fact]
        public async Task GetCustomerAsync_ShouldReturnCustomer()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One" };
            _mockRepository.Setup(r => r.GetAsync(1)).ReturnsAsync(customer);

            // Act
            var result = await _customerService.GetCustomerAsync(1);

            // Assert
            Assert.Equal(customer, result);
        }

        [Fact]
        public async Task AddCustomerAsync_ShouldAddCustomer()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "UnitedStates", State = "California", Zip = "12345" };

            // Act
            await _customerService.AddCustomerAsync(customer);

            // Assert
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task AddCustomerAsync_WithoutZipCode_ShouldAddCustomer()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One" };

            // Act
            await _customerService.AddCustomerAsync(customer);

            // Assert
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task AddCustomerAsync_InvalidStateUS_ShouldThrowException()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "UnitedStates", State = "InvalidState", Zip = "12345" };

            // Act and Assert
            await Assert.ThrowsAsync<CustomerStateZipException>(() => _customerService.AddCustomerAsync(customer));
        }

        [Fact]
        public async Task AddCustomerAsync_InvalidStateCanada_ShouldThrowException()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "Canada", State = "InvalidState", Zip = "T3B 6F7" };

            // Act and Assert
            await Assert.ThrowsAsync<CustomerStateZipException>(() => _customerService.AddCustomerAsync(customer));
        }

        [Fact]
        public async Task AddCustomerAsync_InvalidZipCodeUS_ShouldThrowException()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "UnitedStates", State = "California", Zip = "1234" };

            // Act and Assert
            await Assert.ThrowsAsync<CustomerStateZipException>(() => _customerService.AddCustomerAsync(customer));
        }

        [Fact]
        public async Task AddCustomerAsync_InvalidZipCodeCanada_ShouldThrowException()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "Canada", State = "Alberta", Zip = "T3B000" };

            // Act and Assert
            await Assert.ThrowsAsync<CustomerStateZipException>(() => _customerService.AddCustomerAsync(customer));
        }

        [Fact]
        public async Task UpdateCustomer_ShouldCallRepositotyUpdate()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "UnitedStates", State = "California", Zip = "12345" };
            _mockRepository.Setup(r => r.GetAsync(customer.ID)).ReturnsAsync(customer);

            // Act
            await _customerService.UpdateCustomerAsync(customer);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(It.Is<Customer>(c => c.ID == customer.ID && c.Name == customer.Name)), Times.Once);
        }

        [Fact]
        public async Task UpdateCustomer_WithoutZipCode_ShouldCallRepositotyUpdate()
        {
            // Arrange  
            var customer = new Customer { ID = 1, Name = "Customer One" };
            _mockRepository.Setup(r => r.GetAsync(customer.ID)).ReturnsAsync(customer);

            // Act
            await _customerService.UpdateCustomerAsync(customer);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(It.Is<Customer>(c => c.ID == customer.ID && c.Name == customer.Name)), Times.Once);
        }

        [Fact]
        public async Task UpdateCustomerAsync_CustomerNotFound_ShouldThrowException()
        {
            // Arrange
            var customer = new Customer { ID = 99, Name = "NonExistent Customer" };
            _mockRepository.Setup(r => r.GetAsync(customer.ID)).ReturnsAsync((Customer)null);

            // Act and Assert
            await Assert.ThrowsAsync<CustomerNotFoundException>(() => _customerService.UpdateCustomerAsync(customer));
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldCallRepositoryDelete()
        {
            // Arrange
            var customer = new Customer { ID = 1, Name = "Customer One" };
            _mockRepository.Setup(r => r.GetAsync(customer.ID)).ReturnsAsync(customer);

            // Act
            await _customerService.DeleteCustomerAsync(customer.ID);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync(customer.ID), Times.Once);
        }

        [Fact]
        public async Task DeleteCustomerAsync_CustomerNotFound_ShouldThrowException()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);

            // Act and Assert
            await Assert.ThrowsAsync<CustomerNotFoundException>(() => _customerService.DeleteCustomerAsync(99));
        }
    }
}
