using assessment_api_developer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace assessment_api_developer.Infra.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}
