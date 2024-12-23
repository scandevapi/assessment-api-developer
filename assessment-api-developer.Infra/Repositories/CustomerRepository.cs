using assessment_api_developer.Domain.Exceptions;
using assessment_api_developer.Domain.Interfaces;
using assessment_api_developer.Domain.Models;
using assessment_api_developer.Infra.DataContext;
using Microsoft.EntityFrameworkCore;

namespace assessment_api_developer.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        //private readonly AppDbContext _context;

        //public CustomerRepository(AppDbContext context)
        //{
        //    _context = context;
        //}

        // Assuming you have a DbContext named 'context'
        private readonly List<Customer> customers = new List<Customer>();

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            //return await _context.Customers.ToListAsync();

            return await Task.FromResult(customers);
        }

        public async Task<Customer> GetAsync(int id)
        {
            //return await _context.Customers.FindAsync(id);

            return await Task.FromResult(customers.FirstOrDefault(c => c.ID == id));
        }

        public async Task AddAsync(Customer customer)
        {
            //if (await _context.Customers.AnyAsync(c => c.Name == customer.Name))
            //{
            //    throw new CustomerExistException($"Customer with Name {customer.Name} already exists.");
            //}

            //await _context.Customers.AddAsync(customer);
            //await _context.SaveChangesAsync();

            if (customers.Any(c => c.Name == customer.Name))
            {
                throw new CustomerExistException($"Customer with Name {customer.Name} already exists.");
            }

            customers.Add(customer);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Customer customer)
        {
            //var existingCustomer = await _context.Customers.FindAsync(customer.ID);
            //if (existingCustomer != null)
            //{
            //    _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
            //    await _context.SaveChangesAsync();
            //}

            var existCustomer = customers.FirstOrDefault(c => c.ID == customer.ID);
            if (existCustomer != null)
            {
                existCustomer.Name = customer.Name;
                existCustomer.Address = customer.Address;
                existCustomer.Email = customer.Email;
                existCustomer.Phone = customer.Phone;
                existCustomer.City = customer.City;
                existCustomer.State = customer.State;
                existCustomer.Zip = customer.Zip;
                existCustomer.Country = customer.Country;
                existCustomer.Notes = customer.Notes;
                existCustomer.ContactName = customer.ContactName;
                existCustomer.ContactPhone = customer.ContactPhone;
                existCustomer.ContactEmail = customer.ContactEmail;
                existCustomer.ContactTitle = customer.ContactTitle;
                existCustomer.ContactNotes = customer.ContactNotes;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            //var customer = await _context.Customers.FindAsync(id);
            //if (customer != null)
            //{
            //    _context.Customers.Remove(customer);
            //    await _context.SaveChangesAsync();
            //}

            var customer = customers.FirstOrDefault(c => c.ID == id);
            if (customer != null)
            {
                customers.Remove(customer);
            }
            await Task.CompletedTask;
        }
    }
}
