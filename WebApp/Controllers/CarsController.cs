using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Models;
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

        // GET: Cars1
        public IActionResult Index()
        {
            var car =this.data.Cars
                .OrderBy(c=>c.Repairs.Count())
                .Select(c => new IndexCarAllViewModel
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                PictureUrl = c.PictureUrl,
                PlateNumber = c.PlateNumber,
                Year = c.Year
            }).ToList();

            return View(car);
            // return View(data.Cars.OrderBy(c=>c.Repairs.Count()).ToList());
        }

        //// GET: Cars1/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
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
                    FuelType = c.FuelType.Name,//c.FuelTypeId,
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
        public IActionResult Create()
        {
            return View(new CreateCarFormModel
            {
                FuelTypes = this.GetFuelTypes()
            });
        }

        [HttpPost]
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
            var carData = new Car
            {
               // Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Color = car.Color,
                Description = car.Description,
                FuelTypeId = car.FuelTypeId,
                PictureUrl = car.PictureUrl,
                PlateNumber = car.PlateNumber,
                VinNumber = car.VinNumber,
                Year = car.Year,
                Repairs = new List<Repair>()
            };
           
            this.data.Cars.Add(carData);
            this.data.SaveChanges();
            return RedirectToAction("Index", "Cars");

        }
       

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car =  data.Cars.Find(id);
           
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
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car =  data.Cars
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