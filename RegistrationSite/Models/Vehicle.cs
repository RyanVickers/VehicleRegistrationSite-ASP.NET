﻿using System;
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
        public int DriverId { get; set; }
        [Required]
        [Display(Name = "VIN Number")]
        public string VINNumber { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Colour { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        [Display(Name = "License Plate")]
        public string LicencePlate { get; set; }
        [Required]
        [Display(Name = "Sticker Expiry")]
        [DataType(DataType.Date)]
        public DateTime LicenseStickerExpiry { get; set; }
        [Display(Name = "Primary Driver")]
        public Driver Driver { get; set; }

    }
}
