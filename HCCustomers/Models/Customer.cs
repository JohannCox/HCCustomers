using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HCCustomers.Models
{
    public class Customer
    {
    [Key]
    public int CustomerId { get; set; }


    [Required]
    public string LName { get; set; }

    [Required]
    public string FName { get; set; }

    [Required]
    public string DOB { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string State { get; set; }

    [Required]
    public string ZipCode { get; set; }

    [Required]
    public string Interests { get; set; }

    [Required]
    public byte[] Image { get; set; }


    }
}
