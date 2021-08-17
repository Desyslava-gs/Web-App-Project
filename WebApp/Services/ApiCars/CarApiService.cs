using System.Collections.Generic;
using System.Linq;
using WebApp.Data;

namespace WebApp.Services.ApiCars
{
    public class CarApiService: ICarApiService
    {  
        private readonly CarRepairDbContext data;

        public CarApiService(CarRepairDbContext data)
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
