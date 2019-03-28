using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using HCCustomers.Models;
using Microsoft.EntityFrameworkCore;

namespace HCCustomers
{
    public class Program
    {
    public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        static byte[] imageBytes { get; set; }

        static async void ImageStreamer()
        {
         
            byte[] imageStream;

            imageStream = await System.IO.File.ReadAllBytesAsync(@"./DB/me.jpg");

            imageBytes= imageStream;
        }

        public static void DoDB()
        {
            ImageStreamer();
 
      var dbConnection = Configuration.GetConnectionString("DBConn");

      DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();

      if (!optionsBuilder.IsConfigured)
      {
        IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
           .Build();
        var connectionString = configuration.GetConnectionString("DBConn");
        optionsBuilder.UseSqlite(connectionString);
      }

      using (var db = new CustomerContext())
            {
                var customer = new Customer
                {
                    LName = "Cox",
                    FName = "Johann",
                    DOB = new DateTime(2001, 01, 30).ToString(),
                    Address = "200 Heywood Ave",
                    City = "Spartanburg",
                    State = "SC",
                    ZipCode = "29304",
                    Interests = "Hiking, Biking, Cloud Watching",
                    Image = imageBytes
                };
        if (db.Customers.Count() >=1)
        {
         // string tablename = "Customers";
          FormattableString sql =$"delete from Customers;";

          db.Database.ExecuteSqlCommand(sql);
          //db.Customers.RemoveRange(db.Customers);
          db.SaveChanges();
        }
        db.Add(customer);
        db.SaveChangesAsync();
      }
        }


    }
}
