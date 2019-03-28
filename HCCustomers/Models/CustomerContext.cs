using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HCCustomers.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HCCustomers.Models
{
    public class CustomerContext :DbContext
    {
    public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
    {
    }

    public CustomerContext()
    {
    }

    public DbSet<Customer> Customers { get; set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DBConn");
                optionsBuilder.UseSqlite(connectionString);
            }
        }

    }
}
