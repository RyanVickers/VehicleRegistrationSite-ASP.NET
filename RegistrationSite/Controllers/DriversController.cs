using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegistrationSite.Data;
using RegistrationSite.Models;

namespace RegistrationSite.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class DriversController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DriversController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Drivers
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Drivers.ToListAsync());
        }

        //Gets file from drivers folder and inputs into database
        [HttpPost]
        public async Task<ActionResult> IndexAsync(IFormFile files)
        {
            if (files != null)
            {
                try
                {
                    var FilePath = Path.GetTempFileName();
                    var fileName = Guid.NewGuid() + "-" + files.FileName;
                    var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\temp\\drivers\\" + fileName;
                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await files.CopyToAsync(stream);
                    }

                    {
                        string Data = System.IO.File.ReadAllText(uploadPath);
                        foreach (string row in Data.Split('\n'))
                        {
                            if (!string.IsNullOrEmpty(row))
                            {
                                Driver driver = new Driver()
                                {
                                    FirstName = row.Split(',')[0],
                                    LastName = row.Split(',')[1],
                                    DateOfBirth = Convert.ToDateTime(row.Split(',')[2]),
                                    Address = row.Split(',')[3],
                                    City = row.Split(',')[4],
                                    Province = row.Split(',')[5],
                                    PostalCode = row.Split(',')[6],
                                    LicenseNumber = row.Split(',')[7],
                                    LicenseExpiry = Convert.ToDateTime(row.Split(',')[8]),
                                    DriverPhoto = row.Split(',')[9],
                                };
                                _context.Drivers.Add(driver);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else { return RedirectToAction(nameof(Index)); }

            return View();
        }


        // GET: Drivers/Details/5
    
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // GET: Drivers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DateOfBirth,Address,City,Province,PostalCode,LicenseNumber,LicenseExpiry")] Driver driver, IFormFile DriverPhoto)
        {
            if (ModelState.IsValid)
            {
                if (DriverPhoto.Length > 0)
                {
                    var filePath = Path.GetTempFileName();
                    var fileName = Guid.NewGuid() + "-" + DriverPhoto.FileName;
                    var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\img\\drivers\\" + fileName;
                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await DriverPhoto.CopyToAsync(stream);
                    }
                    driver.DriverPhoto = fileName;
                }
                _context.Add(driver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(driver);
        }

        // GET: Drivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateOfBirth,Address,City,Province,PostalCode,LicenseNumber,LicenseExpiry,DriverPhoto")] Driver driver)
        {
            if (id != driver.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(driver);
        }

        // GET: Drivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.Id == id);
        }
    }
}
