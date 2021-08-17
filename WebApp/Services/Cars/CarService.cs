using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models.Cars;
using WebApp.Models.Repairs;

namespace WebApp.Services.Cars
{
    public class CarService: ICarService
    {
        private readonly CarRepairDbContext data;
     
        
        public CarService(CarRepairDbContext data)
        {
            this.data = data;
        }

        public DetailsCarViewModel DetailsCar(string id)
        {
            return this.data.Cars
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
        }




    }
}
