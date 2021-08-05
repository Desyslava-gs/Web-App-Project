using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Data.Models;

namespace WebApp.Controllers.Api
{
    [ApiController]
    [Route("api/cars")]
    public class CarsApiController : ControllerBase
    {
        private readonly CarRepairDbContext data;

        public CarsApiController(CarRepairDbContext data)
            => this.data = data;

        [HttpGet]
        public ActionResult<Car> Cars()
        {
            var cars= this.data.Cars.ToList();
            if (!cars.Any())
            {
                return NotFound();
            }

            return Ok(cars);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Car> GetDetails(string id)
        {
            var car = this.data.Cars.Find(id);
            if (car==null)
            {
                return NotFound();
            }

            return Ok(car);
        }

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
