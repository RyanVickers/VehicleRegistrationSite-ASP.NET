using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationSite.Models
{
    public class Driver
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        [DataType(DataType.Date)]
        public DateTime LicenseExpiry { get; set; }
        public string licenseNumber { get; set; }
        public List<Vehicle> Vehicles { get; set; }

    }
}
