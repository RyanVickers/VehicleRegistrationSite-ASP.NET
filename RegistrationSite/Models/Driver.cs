using System;
using System.IO;
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
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Required]
        [Display(Name = "License Number")]
        [MaxLength(15)]
        public string LicenseNumber { get; set; }
        [Required]
        [Display(Name = "License Expiry")]
        [DataType(DataType.Date)]
        public DateTime LicenseExpiry { get; set; }
        [Display(Name = "Driver Photo")]
        public string DriverPhoto { get; set; }
        public List<Vehicle> Vehicles { get; set; }

    }
}
