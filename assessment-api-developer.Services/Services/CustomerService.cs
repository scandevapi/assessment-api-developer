using assessment_api_developer.Domain.Exceptions;
using assessment_api_developer.Domain.Interfaces;
using assessment_api_developer.Domain.Models;
using System.Text.RegularExpressions;

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

        public async Task AddCustomerAsync(Customer customer)
        {
            ValidateCustomerStateAndZip(customer);
            await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await EnsureCustomerExistsAsync(customer.ID);
            ValidateCustomerStateAndZip(customer);
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var existingCustomer = await EnsureCustomerExistsAsync(id);
            await _customerRepository.DeleteAsync(id);
        }



        private async Task<Customer> EnsureCustomerExistsAsync(int customerId)
        {
            var customer = await _customerRepository.GetAsync(customerId);
            if (customer == null)
            {
                throw new CustomerNotFoundException($"Customer with id {customerId} not found");
            }
            return customer;
        }


        private void ValidateCustomerStateAndZip(Customer customer)
        {
            if (!string.IsNullOrEmpty(customer.State))
            {
                if (!IsValidStateOrProvince(customer.Country, customer.State))
                {
                    throw new CustomerStateZipException("Invalid state or province for the selected country.");
                }
                if (!IsValidateZipCode(customer.Country, customer.Zip))
                {
                    throw new CustomerStateZipException("Invalid zip code for the selected country.");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(customer.Zip))
                {
                    throw new CustomerStateZipException("Zip code cannot be provided without a state or province.");
                }
            }
        }


        private bool IsValidStateOrProvince(string country, string stateOrProvince)
        {
            if (country == null || stateOrProvince == null)
            {
                return false;
            }

            if (!Enum.TryParse(country, out Countries parsedCountry))
            {
                return false;
            }

            if (parsedCountry == Countries.UnitedStates)
            {
                return Enum.TryParse(typeof(USStates), stateOrProvince, out _);
            }
            else if (parsedCountry == Countries.Canada)
            {
                return Enum.TryParse(typeof(CanadianProvinces), stateOrProvince, out _);
            }

            return false;
        }

        private bool IsValidateZipCode(string country, string zipCode)
        {
            if (country == null || zipCode == null)
            {
                return false;
            }

            if (!Enum.TryParse(country, out Countries parsedCountry))
            {
                return false;
            }

            if (parsedCountry == Countries.UnitedStates)
            {
                return Regex.IsMatch(zipCode, @"^\d{5}(-\d{4})?$");
            }
            if (parsedCountry == Countries.Canada)
            {
                return Regex.IsMatch(zipCode, @"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$");
            }

            return false;
        }
    }
}
