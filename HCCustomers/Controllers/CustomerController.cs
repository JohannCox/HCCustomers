using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCCustomers.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace HCCustomers.Controllers
{
  [Route("api/[Controller]")]
  public class CustomerController :ControllerBase
  {
    private readonly CustomerContext _context;
    
    public CustomerController(CustomerContext context)
    {
      _context = context;
    }

    [HttpGet]
    public List<Models.Customer> GetCustomers()
    {
      List<Models.Customer> custs = new List<Models.Customer>();

      foreach (var item in _context.Customers)
      {
        custs.Add(item);
      }

        return custs;
    }

    [HttpGet("/api/{id}")]
    public async Task<ActionResult<Models.Customer>> Get(int id)
    {
      var customer = await _context.Customers.FindAsync(id);

      if (customer == null)
      {
        return NotFound();
      }
      return customer;
    }

    [HttpGet("{name}")]
    public  ActionResult<List<Models.Customer>> GetCustomer(string name)
    {
      List<Models.Customer> customer = new List<Models.Customer>();

      foreach (var item in _context.Customers)
      {
        if (item.LName.ToLower().StartsWith(name.ToLower()) ||
          item.FName.ToLower().StartsWith(name.ToLower())
          )
        {
          customer.Add(item);
        }
      }

      if (customer == null)
      {
        return NotFound();
      }
      return customer;
    }


    public IConfiguration Configuration { get; }

    public IEnumerable<Customer> Customers()
    {
      var dbConnection = Configuration.GetConnectionString("DBConn");

      List<Customer> customers = new List<Customer>();


      using (var db = new CustomerContext())
      {
        foreach (var item in db.Customers)
        {
          Customer readCust = new Customer()
          {
            LName = item.LName,
            FName = item.FName,
            Address = item.Address,
            City = item.City,
            State = item.State,
            ZipCode = item.ZipCode,
            Interests = item.Interests,
            Image = item.Image
          };

          customers.Add(readCust);
        }
        return customers;
      }
    }

    [HttpGet("/api/customers")]
    public ActionResult<List<Customer>> Get()
    {
      List<Customer> customers = new List<Customer>();

      var dbConnection = Configuration.GetConnectionString("DBConn");

        foreach (var item in _context.Customers)
        {
          Customer readCust = new Customer()
          {
            CustomerId =item.CustomerId,
            LName = item.LName,
            FName = item.FName,
            Address = item.Address,
            City = item.City,
            State = item.State,
            ZipCode = item.ZipCode,
            Interests = item.Interests,
            Image = item.Image,
            DOB=item.DOB
          };

          customers.Add(readCust);
        }
     
      if (customers.Count < 1)
      {
        return NotFound();
      }
      else
      {
        return customers;
      }
    }

    [HttpPost]
    public async Task<ActionResult<Models.Customer>> PostCustomer(Customer customer)
    {
      _context.Customers.Add(customer);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetCustomer", new { id = customer.CustomerId}, customer);
    }

    [HttpDelete]
    public async Task<ActionResult<Models.Customer>> DeleteCustomer(int id)
    {
      var delCustomer = await _context.Customers.FindAsync(id);
      if (delCustomer == null)
      {
        return NotFound();
      }
      _context.Customers.Remove(delCustomer);
      await _context.SaveChangesAsync();

      return delCustomer;
    }

    public class Customera
    {
      public int CustomerId { get; set; }
      public string LName { get; set; }
      public string FName { get; set; }
      public string DOB { get; set; }
      public string Address { get; set; }
      public string City { get; set; }
      public string State { get; set; }
      public string ZipCode { get; set; }
      public string Interests { get; set; }
      public byte[] Image { get; set; }
    }

  }
}
