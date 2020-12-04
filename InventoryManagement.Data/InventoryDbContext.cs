using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data.Models;

namespace InventoryManagement.Data
{
    public class InventoryDbContext : IdentityDbContext
    {
        public InventoryDbContext() { }

        public InventoryDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
    }
}
