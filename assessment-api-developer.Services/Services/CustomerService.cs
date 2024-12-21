﻿using assessment_api_developer.Domain.Interfaces;
using assessment_api_developer.Domain.Models;

namespace assessment_api_developer.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await _customerRepository.GetAsync(id);
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteAsync(id);
        }
    }
}
