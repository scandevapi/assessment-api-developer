﻿using assessment_api_developer.Domain.Exceptions;
using assessment_api_developer.Domain.Interfaces;
using assessment_api_developer.Domain.Models;
using assessment_api_developer.Services.Services;
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
            var customers = new List<Customer>
            {
                new Customer { ID=1, Name="Customer One" },
                new Customer { ID=2, Name="Customer Two" }
            };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(customers);

            var result = await _customerService.GetAllCustomersAsync();

            Assert.Equal(customers, result);
        }

        [Fact]
        public async Task GetCustomerAsync_ShouldReturnCustomer()
        {
            var customer = new Customer { ID = 1, Name = "Customer One" };
            _mockRepository.Setup(r => r.GetAsync(1)).ReturnsAsync(customer);

            var result = await _customerService.GetCustomerAsync(1);

            Assert.Equal(customer, result);
        }

        [Fact]
        public async Task AddCustomerAsync_ShouldAddCustomer()
        {
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "UnitedStates", State = "California", Zip = "12345" };

            await _customerService.AddCustomerAsync(customer);

            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task AddCustomerAsync_WithoutZipCode_ShouldAddCustomer()
        {
            var customer = new Customer { ID = 1, Name = "Customer One" };

            await _customerService.AddCustomerAsync(customer);

            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task AddCustomerAsync_InvalidStateUS_ShouldThrowException()
        {
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "UnitedStates", State = "InvalidState", Zip = "12345" };

            await Assert.ThrowsAsync<CustomerStateZipException>(() => _customerService.AddCustomerAsync(customer));
        }

        [Fact]
        public async Task AddCustomerAsync_InvalidStateCanada_ShouldThrowException()
        {
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "Canada", State = "InvalidState", Zip = "T3B 6F7" };

            await Assert.ThrowsAsync<CustomerStateZipException>(() => _customerService.AddCustomerAsync(customer));
        }

        [Fact]
        public async Task AddCustomerAsync_InvalidZipCodeUS_ShouldThrowException()
        {
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "UnitedStates", State = "California", Zip = "1234" };

            await Assert.ThrowsAsync<CustomerStateZipException>(() => _customerService.AddCustomerAsync(customer));
        }

        [Fact]
        public async Task AddCustomerAsync_InvalidZipCodeCanada_ShouldThrowException()
        {
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "Canada", State = "Alberta", Zip = "T3B000" };

            await Assert.ThrowsAsync<CustomerStateZipException>(() => _customerService.AddCustomerAsync(customer));
        }

        [Fact]
        public async Task UpdateCustomer_ShouldCallRepositotyUpdate()
        {
            var customer = new Customer { ID = 1, Name = "Customer One", Country = "UnitedStates", State = "California", Zip = "12345" };
            _mockRepository.Setup(r => r.GetAsync(customer.ID)).ReturnsAsync(customer);

            await _customerService.UpdateCustomerAsync(customer);

            _mockRepository.Verify(r => r.UpdateAsync(It.Is<Customer>(c => c.ID == customer.ID && c.Name == customer.Name)), Times.Once);
        }

        [Fact]
        public async Task UpdateCustomer_WithoutZipCode_ShouldCallRepositotyUpdate()
        {
            var customer = new Customer { ID = 1, Name = "Customer One" };
            _mockRepository.Setup(r => r.GetAsync(customer.ID)).ReturnsAsync(customer);

            await _customerService.UpdateCustomerAsync(customer);

            _mockRepository.Verify(r => r.UpdateAsync(It.Is<Customer>(c => c.ID == customer.ID && c.Name == customer.Name)), Times.Once);
        }

        [Fact]
        public async Task UpdateCustomerAsync_CustomerNotFound_ShouldThrowException()
        {
            var customer = new Customer { ID = 99, Name = "NonExistent Customer" };
            _mockRepository.Setup(r => r.GetAsync(customer.ID)).ReturnsAsync((Customer)null);

            await Assert.ThrowsAsync<CustomerNotFoundException>(() => _customerService.UpdateCustomerAsync(customer));
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldCallRepositoryDelete()
        {
            var customer = new Customer { ID = 1, Name = "Customer One" };
            _mockRepository.Setup(r => r.GetAsync(customer.ID)).ReturnsAsync(customer);

            await _customerService.DeleteCustomerAsync(customer.ID);

            _mockRepository.Verify(r => r.DeleteAsync(customer.ID), Times.Once);
        }

        [Fact]
        public async Task DeleteCustomerAsync_CustomerNotFound_ShouldThrowException()
        {
            _mockRepository.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);

            await Assert.ThrowsAsync<CustomerNotFoundException>(() => _customerService.DeleteCustomerAsync(99));
        }
    }
}
