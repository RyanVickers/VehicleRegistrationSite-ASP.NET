using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistrationSite.Controllers;
using RegistrationSite.Data;
using RegistrationSite.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrationSiteTest
{
    [TestClass]
    public class VehicleControllerTest
    {
        private ApplicationDbContext _context;
        List<Vehicle> vehicles = new List<Vehicle>();
        VehiclesController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            // create mock data inside the in-memory db
            var Driver = new Driver { Id = 10, FirstName = "Ryan", LastName = "Vickers", DateOfBirth = new DateTime(2001, 7, 05), Address = "Sideroad 25", City = "Meaford", Province = "Ontario", PostalCode = "N461W3", LicenseNumber = "V1000G7DHF44VVD", LicenseExpiry = System.DateTime.Now, DriverPhoto = "Bruh.png" };

            vehicles.Add(new Vehicle { Id = 1, VINNumber = "A560G315818ER492W", Manufacturer = "Dodge", Model = "Ram 1500", Colour = "Black", Year = 2019, LicencePlate = "B69LMDS", LicenseStickerExpiry = System.DateTime.Today, DriverId = 10 });
            vehicles.Add(new Vehicle { Id = 2, VINNumber = "3SDFS315818ER492W", Manufacturer = "Ford", Model = "Crown Victoria", Colour = "White", Year = 1987, LicencePlate = "JDKAKSD", LicenseStickerExpiry = new DateTime(2022, 5, 30), DriverId = 10 });
            vehicles.Add(new Vehicle { Id = 3, VINNumber = "DFDFS315818ER492W", Manufacturer = "Chrysler", Model = "Pacifica", Colour = "Yellow", Year = 2005, LicencePlate = "SJA4JSD", LicenseStickerExpiry = new DateTime(2021, 6, 23), DriverId = 10 });

            foreach (var p in vehicles)
            {
                _context.Vehicles.Add(p);
            }

            _context.SaveChanges();

            // instantiate the products controller and pass it the mock db object (dependency injection)
            controller = new VehiclesController(_context);
        }
    }
}