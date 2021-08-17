using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Infrastructure;
using WebApp.Models.Cars;
using WebApp.Services.Cars;
using WebApp.Services.Clients;

namespace WebApp.Controllers
{
    public class CarsController : Controller
    {

        private readonly ICarService carService;
        private readonly IClientService clientService;

        public CarsController(ICarService carService, IClientService clientService)
        {
            this.carService = carService;
            this.clientService = clientService;
        }
        
        [Route("Cars/Index")]
        public IActionResult Index([FromQuery] string search)

        {
            var carsQuery = this.carService.CarsQuery();
            if (!string.IsNullOrEmpty(search))
            {
                carsQuery = carsQuery.Where(c =>
                c.Make.ToLower().Contains(search.ToLower()) ||
                c.Model.ToLower().Contains(search.ToLower()) ||
                c.Year.ToString().Contains(search.ToLower()) ||
                c.PlateNumber.ToLower().Contains(search.ToLower()));
            }

            if (!this.clientService.IsClient(this.User.GetId()) && !this.User.IsAdmin())
            {
                return RedirectToAction("Index", "Clients");
            }

            if (this.clientService.IsClient(this.User.GetId()))
            {
                var clientId = this.carService.ClientId(this.User.GetId());
                carsQuery = carsQuery.Where(c => c.ClientId == clientId);
            }

            var car = this.carService.AllCars(carsQuery);

            return View(new AllCarsViewModel
            {
                CarsList = car.ToList(),
                SearchList = search
            });

        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!this.clientService.IsClient(this.User.GetId()) && !this.User.IsAdmin())
            {
                return RedirectToAction("Index", "Cars");
            }

            var car = this.carService.DetailsCar(id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [Authorize]
        public IActionResult Create()
        {
            if (!this.clientService.IsClient(User.GetId()) && !this.User.IsAdmin())
            {
                return RedirectToAction("Index", "Clients");
            }
            return View(new CreateCarFormModel
            {
                FuelTypes = this.carService.GetFuelTypes()
            });

        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCarFormModel car)
        {
            if (!this.carService.AnyFuelType(car.FuelTypeId))
            {
                this.ModelState.AddModelError(nameof(car.FuelTypeId), "FuelType does not exist.");
            }

            if (!ModelState.IsValid)
            {
                car.FuelTypes = this.carService.GetFuelTypes();
                return View(car);
            }

            var clientId = this.carService.ClientId(this.User.GetId());

            this.carService.CreateCars(car, clientId);

            return RedirectToAction("Index", "Cars");
        }

        [Authorize] //Client and Admin!!!
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = this.carService.Car(id);

            if (car == null)
            {
                return NotFound();
            }
            return View(new EditCarFormModel
            {
                FuelTypes = this.carService.GetFuelTypes(),
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
                ClientId = car.ClientId
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

            if (!ModelState.IsValid)
            {
                car.FuelTypes = this.carService.GetFuelTypes();
                return View(car);
            }

            try
            {
                this.carService.EditCars(id, car);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.carService.CarExists(car.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = this.carService.DeleteCar(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            if (this.carService.AnyReairs(id))
            {
                return RedirectToAction("Error");

            }
            this.carService.DeleteConfirmed(id);
            return RedirectToAction("Index");
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
        public IActionResult Error()
        {
            return View();
        }
    }
}