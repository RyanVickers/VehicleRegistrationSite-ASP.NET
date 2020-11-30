using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistrationSite.Controllers;
using RegistrationSite.Data;
using RegistrationSite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RegistrationSiteTest
{
    [TestClass]
    public class DriverControllerTest
    {
        private ApplicationDbContext _context;
        List<Driver> drivers = new List<Driver>();
        DriversController controller;
        IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file")), 0, 0, "Data", "file.txt");
        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            drivers.Add(new Driver { Id = 10, FirstName = "Ryan", LastName = "Vickers", DateOfBirth = new DateTime(2001, 07, 05), Address = "Sideroad 25", City = "Meaford", Province = "Ontario", PostalCode = "N461W3", LicenseNumber = "V1000G7DHF44VVD", LicenseExpiry = System.DateTime.Now, DriverPhoto = "Bruh.png" });
            drivers.Add(new Driver { Id = 11, FirstName = "Jake", LastName = "Hoppers", DateOfBirth = new DateTime(1987, 11, 07), Address = "Lakeview Dirve", City = "Collingwood", Province = "Ontario", PostalCode = "H1WDW9", LicenseNumber = "HDJAKG7DHF44VVD", LicenseExpiry = new DateTime(2021, 12, 15), DriverPhoto = "Bruh.png" });
            drivers.Add(new Driver { Id = 12, FirstName = "Nathan", LastName = "Long", DateOfBirth = new DateTime(1991, 09, 12), Address = "MapleStreet", City = "Toronto", Province = "Ontario", PostalCode = "DJA3MD", LicenseNumber = "IWM3WG7DHF44VVD", LicenseExpiry = new DateTime(2023, 11, 07), DriverPhoto = "Bruh.png" });

            foreach (var p in drivers)
            {
                _context.Drivers.Add(p);
            }

            _context.SaveChanges();

            controller = new DriversController(_context);
        }
        [TestMethod]
        public void IndexViewLoads()
        {
            var result = controller.Index();
            var viewResult = (ViewResult)result.Result;
            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void IndexReturnsVehicleData()
        {
            var result = controller.Index();
            var viewResult = (ViewResult)result.Result;
            List<Driver> model = (List<Driver>)viewResult.Model;
            CollectionAssert.AreEqual(drivers, model);
        }
        //Details
        [TestMethod]
        public void DetailsMissingId()
        {
            var result = controller.Details(null).Result;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void DetailsInvalidId()
        {
            var result = controller.Details(400).Result;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void DetailsValidIdLoadsDriver()
        {
            var result = controller.Details(10).Result;
            var viewResult = (ViewResult)result;
            Assert.AreEqual(drivers[0], viewResult.Model);
        }
        //Create
        [TestMethod]
        public void CreateLoadsView()
        {
            ViewResult result = (ViewResult)controller.Create();
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void CreatePostInvalidDriver()
        {

            Driver invalidDriver = new Driver();
            controller.ModelState.AddModelError("Error", "Fake Error");
            var result = controller.Create(invalidDriver, file);
            var viewResult = (ViewResult)result.Result;
            Assert.AreEqual("Create", viewResult.ViewName);
        }

        [TestMethod]
        public void CreatePostAddsDriver()
        {
            var driver1 = new Driver { Id = 120, FirstName = "Ryan", LastName = "Vickers", DateOfBirth = new DateTime(2001, 07, 05), Address = "Sideroad 25", City = "Meaford", Province = "Ontario", PostalCode = "N461W3", LicenseNumber = "V1000G7DHF44VVD", LicenseExpiry = System.DateTime.Now };

            var result = controller.Create(driver1, file);
            Assert.AreEqual(_context.Drivers.Last(), driver1);
        }

        [TestMethod]
        public void CreatePostRedirectsToIndex()
        {

            var driver1 = new Driver { Id = 120, FirstName = "Ryan", LastName = "Vickers", DateOfBirth = new DateTime(2001, 07, 05), Address = "Sideroad 25", City = "Meaford", Province = "Ontario", PostalCode = "N461W3", LicenseNumber = "V1000G7DHF44VVD", LicenseExpiry = System.DateTime.Now };

            var result = controller.Create(driver1, file);
            var redirectResult = (RedirectToActionResult)result.Result;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        //Edit
        [TestMethod]
        public void EditNoId()
        {
            var result = controller.Edit(null).Result;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void EditIdMisMatch()
        {
            var result = controller.Edit(1,drivers[1]).Result;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void EditValidId()
        {
            var result = controller.Edit(10);
            var viewResult = (ViewResult)result.Result;
            Assert.AreEqual("Edit", viewResult.ViewName);
        }
        [TestMethod]
        public void EditInvalidId()
        {
            var result = controller.Edit(400).Result;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
       
        [TestMethod]
        public void EditSaveValid()
        {
            var result = controller.Edit(10,drivers[0]);
            var redirectResult = (RedirectToActionResult)result.Result;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }
      
        [TestMethod]
        public void EditSaveInvalidLoadsEdit()
        {
            controller.ModelState.AddModelError("Error", "Fake Error");
            var result = controller.Edit(10,drivers[0]);
            var viewResult = (ViewResult)result.Result;
            Assert.AreEqual("Edit", viewResult.ViewName);

        }

        //Delete
        [TestMethod]
        public void DeleteNoId()
        {
            var result = controller.Delete(null).Result;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void DeleteValidId()
        {
            var result = controller.Delete(10);
            var viewResult = (ViewResult)result.Result;
            Assert.AreEqual("Delete", viewResult.ViewName);
        }
        [TestMethod]
        public void DeleteInvalidId()
        {
            var result = controller.Delete(400).Result;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

      //Delete confirmed
  
        [TestMethod]
        public void DeleteConfirmedValidId()
        {
            var result = controller.DeleteConfirmed(10).Result;
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void DriverExistsReturnsBool()
        {
            var result = controller.DriverExists(0);
            Assert.IsInstanceOfType(result, typeof(bool));
        }

    }
}
