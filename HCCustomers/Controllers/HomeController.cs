using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HCCustomers.Models;
using Microsoft.Extensions.Configuration;

namespace HCCustomers.Controllers
{
    public class HomeController : Controller
    {
    //public IConfiguration Configuration { get; }

    private readonly CustomerContext _context;

    public HomeController(CustomerContext context)
    {
      _context = context;
    }

    public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            Program.DoDB();

            foreach (var item in _context.Customers)
            {
                ViewData["Message"] = String.Format(item.LName + ", " + item.FName);

                string base64String = Convert.ToBase64String(item.Image, 0, item.Image.Length);

                getImg(item.Image);

                Console.WriteLine(item.LName + ", " + item.FName);
            }
          

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


       public  void GetDataBack()
       {

          foreach (var item in _context.Customers)
          {
              Console.WriteLine(item.LName + ", " + item.FName);
          }
            
       }

        public FileContentResult getImg(byte[] imgBytes)
        {
            byte[] imgArray = imgBytes;
            return imgArray != null
            ? new FileContentResult(imgArray, "image/jpeg")
            : null;
        }

    }
}
