using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.Cars;

namespace WebApp.Controllers.Api
{
    [ApiController]
    [Route("api/cars")]
    public class CarsApiController : ControllerBase
    {
        private readonly CarRepairDbContext data;

        public CarsApiController(CarRepairDbContext data)
        {
            this.data = data;
        }
        
        public IActionResult Index()
        {
            var car = this.data.Cars
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

            return Ok(car);

        }













        //[HttpGet]
        //public ActionResult<Car> Cars()
        //{
        //    var cars= this.data.Cars.ToList();
        //    if (!cars.Any())
        //    {
        //        return NotFound();
        //    }

        //    return Ok(cars);
        //}

        //[HttpGet]
        //[Route("{id}")]
        //public ActionResult<Car> GetDetails(string id)
        //{
        //    var car = this.data.Cars.Find(id);
        //    if (car==null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(car);
        //}








        //[HttpPost]
        //public IActionResult SaveCar(Car car)
        //{

        //}






        //private readonly ICarService cars;

        //public CarsApiController(ICarService cars) 
        //    => this.cars = cars;

        //[HttpGet]
        //public CarQueryServiceModel All([FromQuery] AllCarsApiRequestModel query) 
        //    => this.cars.All(
        //        query.Brand,
        //        query.SearchTerm,
        //        query.Sorting,
        //        query.CurrentPage,
        //        query.CarsPerPage);
    }
}
