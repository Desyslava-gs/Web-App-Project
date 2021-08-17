using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Models;
using WebApp.Models.Cars;

namespace WebApp.Services.Cars
{
    public interface ICarService
    {
        public DetailsCarViewModel DetailsCar(string id);
        public void CreateCars(CreateCarFormModel car, string clientId);
        public string ClientId(string userId);
        public bool AnyFuelType(string id);
        public IQueryable<Car> CarsQuery();
        public List<IndexCarAllViewModel> AllCars(IQueryable<Car> carsQuery);
        public Car Car(string id);
        public void EditCars(string id, EditCarFormModel car);
        public DeleteCarViewModel DeleteCar(string id);
        public void DeleteConfirmed(string id);
        public bool AnyReairs(string id);
        public bool CarExists(string id);
        public IEnumerable<FuelTypeViewModel> GetFuelTypes();
        //public bool IsClient(string userId);
    } 
}
