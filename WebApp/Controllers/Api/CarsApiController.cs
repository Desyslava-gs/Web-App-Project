using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.Api.Cars;
using WebApp.Services.Cars;
using WebApp.Services.Statistics;

namespace WebApp.Controllers.Api
{
    [ApiController]
    [Route("api/cars")]
    public class CarsApiController : ControllerBase
    {
       
        private readonly ICarService carService;


        public CarsApiController(ICarService carService)
        {
            this.carService = carService;
        }

        public IActionResult Index()
        {
            var car = this.carService.All();

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
