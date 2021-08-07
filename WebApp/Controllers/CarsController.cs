using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Models;
using WebApp.Infrastructure;
using WebApp.Models.Cars;

namespace WebApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarRepairDbContext data;

        public CarsController(CarRepairDbContext data)
        {
            this.data = data;
        }
        public bool IsClient(string userId)
        {
            return  this.data
                .Clients
                .Any(u => u.UserId == userId);
           
        }
        // GET: Cars1
        public IActionResult Index()
        {
            if (!IsClient(this.User.GetId()))
            {
                return RedirectToAction("Index", "Clients");
            }
            var userId = this.ClientId(this.User.GetId());
            var car = this.data.Cars 
                .Where(c => c.ClientId == userId)
                .OrderBy(c => c.Repairs.Count())
                .Select(c => new IndexCarAllViewModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    PictureUrl = c.PictureUrl,
                    PlateNumber = c.PlateNumber,
                    Year = c.Year,
                    FinishedRepairs = this.data.Repairs.Count(r => r.EndDate < DateTime.UtcNow),
                    AllCars = this.data.Cars.Count(),
                    AllClients = this.data.Clients.Count()
                }).ToList();

            return View(car);
            
        }

        //// GET: Cars1/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!UserIsClient())
            {
                return RedirectToAction("Index", "Cars");
            }
            var car = this.data.Cars
                .Where(m => m.Id == id)
                .Select(c => new DetailsCarViewModel
                {
                    Id = c.Id,
                    PictureUrl = c.PictureUrl,
                    Make = c.Make,
                    Model = c.Model,
                    Color = c.Color,
                    Description = c.Description,
                    Year = c.Year,
                    FuelType = c.FuelType.Name,
                    PlateNumber = c.PlateNumber,
                    VinNumber = c.VinNumber,

                }).ToList()
                .FirstOrDefault();

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        [Authorize]
        public IActionResult Create()
        {
            if (!this.UserIsClient())
            {
                return RedirectToAction("Index", "Clients");
            }
            return View(new CreateCarFormModel
            {
                FuelTypes = this.GetFuelTypes()
            });

        }

        private bool UserIsClient()
        {
            var userId = this.User.GetId();
            var userIsClient = this.data.Clients.Any(c => c.UserId == userId);
            return userIsClient;

        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCarFormModel car)
        {
            if (!this.data.FuelTypes.Any(c => c.Id == car.FuelTypeId))
            {
                this.ModelState.AddModelError(nameof(car.FuelTypeId), "FuelType does not exist.");
            }

            if (!ModelState.IsValid)
            {
                car.FuelTypes = this.GetFuelTypes();
                return View(car);
            }
            var clientId = this.ClientId(this.User.GetId());

            var carData = new Car
            {

                Make = car.Make,
                Model = car.Model,
                Color = car.Color,
                Description = car.Description,
                FuelTypeId = car.FuelTypeId,
                PictureUrl = car.PictureUrl,
                PlateNumber = car.PlateNumber,
                VinNumber = car.VinNumber,
                Year = car.Year,
                Repairs = new List<Repair>(),
                ClientId = clientId
              
            };

            this.data.Cars.Add(carData);
            this.data.SaveChanges();
            return RedirectToAction("Index", "Cars");

        } 
        public string ClientId(string userId)
        { 
            return this.data
                .Clients
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = data.Cars.Find(id);

            if (car == null)
            {
                return NotFound();
            }
            return View(new EditCarFormModel
            {
                FuelTypes = this.GetFuelTypes(),
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Color = car.Color,
                Description = car.Description,
                FuelTypeId = car.FuelTypeId,
                PictureUrl = car.PictureUrl,
                PlateNumber = car.PlateNumber,
                VinNumber = car.VinNumber,
                Year = car.Year,
            });
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(string id, EditCarFormModel car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var carData = new Car
                    {
                        Id = car.Id,
                        Make = car.Make,
                        Model = car.Model,
                        Color = car.Color,
                        Description = car.Description,
                        FuelTypeId = car.FuelTypeId,
                        PictureUrl = car.PictureUrl,
                        PlateNumber = car.PlateNumber,
                        VinNumber = car.VinNumber,
                        Year = car.Year,
                    };

                    this.data.Update(carData);
                    this.data.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Cars1/Delete/5
        [Authorize]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = data.Cars
                .Select(c => new DeleteCarViewModel
                {
                    Id = c.Id,
                    PictureUrl = c.PictureUrl,
                    Make = c.Make,
                    Model = c.Model,
                    Color = c.Color,
                    Description = c.Description,
                    Year = c.Year,
                    FuelType = c.FuelType.Name,//c.FuelTypeId,
                    PlateNumber = c.PlateNumber,
                    VinNumber = c.VinNumber,

                }).ToList()
                .FirstOrDefault(m => m.Id == id);
            ;
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars1/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var car = data.Cars.Find(id);
            data.Cars.Remove(car);
            data.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool CarExists(string id)
        {
            return data.Cars.Any(e => e.Id == id);
        }

        private IEnumerable<FuelTypeViewModel> GetFuelTypes()
        {
            return data
                .FuelTypes
                .Select(ft => new FuelTypeViewModel
                {
                    Id = ft.Id,
                    Name = ft.Name,

                }).ToList();


        }

        //private string ProcessUploadedFile(SpeakerViewModel model)
        //{
        //    string uniqueFileName = null;

        //    if (model.SpeakerPicture != null)
        //    {
        //        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.SpeakerPicture.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            model.SpeakerPicture.CopyTo(fileStream);
        //        }
        //    }

        //    return uniqueFileName;
        //}
    }
}