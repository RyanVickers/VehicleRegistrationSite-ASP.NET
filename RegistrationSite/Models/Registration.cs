using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationSite.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public int DriverId { get; set; }
        public int VehicleId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Expiry { get; set; }

        public Driver Driver { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
