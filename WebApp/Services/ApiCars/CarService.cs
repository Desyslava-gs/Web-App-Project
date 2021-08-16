using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models.Api.Cars;

namespace WebApp.Services.Cars
{
    public class CarService:ICarService
    {      
        
        private readonly CarRepairDbContext data;

        public CarService(CarRepairDbContext data)
        {
            this.data = data;
        }
        
        
        public IList<ApiCarsModel> All()
        {
            return this.data.Cars
                .OrderBy(c => c.Repairs.Count())
                .Select(c => new ApiCarsModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year,
                    Color = c.Color,
                    FuelType = c.FuelType.Name
                }).ToList();
        }

    }
}
