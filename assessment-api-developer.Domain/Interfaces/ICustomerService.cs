using assessment_api_developer.Domain.Models;

namespace assessment_api_developer.Domain.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
    }
}
