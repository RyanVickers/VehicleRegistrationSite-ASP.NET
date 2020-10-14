using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationSite.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Colour { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string LicencePlate { get; set; }
        [Required]
        public double Price { get; set; }
        public List<Registration> OrderDetails { get; set; }

    }
}
