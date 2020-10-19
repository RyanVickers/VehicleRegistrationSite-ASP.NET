using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationSite.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Primary Driver")]
        public int DriverId { get; set; }
        [Required]
        [Display(Name = "VIN Number")]
        [MaxLength(17)]
        public string VINNumber { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Colour { get; set; }
        [Required]
        [Range(1900, 2020)]
        public int Year { get; set; }
        [Required]
        [Display(Name = "License Plate")]
        [MaxLength(8)]
        public string LicencePlate { get; set; }
        [Required]
        [Display(Name = "Sticker Expiry")]
        [DataType(DataType.Date)]
        public DateTime LicenseStickerExpiry { get; set; }
        [Display(Name = "Primary Driver")]
        public Driver Driver { get; set; }

    }
}
