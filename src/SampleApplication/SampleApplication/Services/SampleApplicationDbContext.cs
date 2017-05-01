using Microsoft.EntityFrameworkCore;
using SampleApplication.Models.Entities;

namespace SampleApplication.Services
{
    public class SampleApplicationDbContext : DbContext
    {
        public SampleApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<CustomerEntity> Customers { get; set; }
    }
}