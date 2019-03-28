using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCCustomers.Models;
using Microsoft.Extensions.Configuration;

namespace HCCustomers.Controllers
{
  [Route("api/[Controller]")]
  public class TestController :Controller
  {
    private readonly CustomerContext _context;
    public TestController(CustomerContext context)
    {
      _context = context;
    }

    [HttpGet("/api/names")]
    public IEnumerable<string> Get()
    {
      List<string> names = new List<string>();

        foreach (var item in _context.Customers)
        {
          names.Add(String.Format(item.LName + ", " + item.FName));
        }
      
   if (names.Count < 1)
      {
        return new string[]{ "None Found"};
      }
      else
      {
        return names;
      }
    }



  }
}
