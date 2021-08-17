using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Data;
using WebApp.Data.Models;
using WebApp.Models.Cars;

namespace WebApp.Services.Cars
{
    public class CarService : ICarService
    {
        private readonly CarRepairDbContext data;

        public CarService(CarRepairDbContext data)
        {
            this.data = data;
        }

        public List<IndexCarAllViewModel> AllCars(IQueryable<Car> carsQuery)
        {
            return carsQuery
                .OrderByDescending(c => c.Repairs.Count())
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
        }

        public IQueryable<Car> CarsQuery()
        {
            return this.data.Cars.AsQueryable();
        }

        public void CreateCars(CreateCarFormModel car, string clientId)
        {

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

        public void EditCars(string id, EditCarFormModel car)
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
                ClientId = car.ClientId
            };

            this.data.Update(carData);
            this.data.SaveChanges();
        }

        public DeleteCarViewModel DeleteCar(string id)
        {
            return data.Cars
                .Select(c => new DeleteCarViewModel
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
                .FirstOrDefault(m => m.Id == id);
        }

        public void DeleteConfirmed(string id)
        {

            var car = this.Car(id);
            this.data.Cars.Remove(car);
            this.data.SaveChanges();
        }

        public string ClientId(string userId)
        {
            return this.data
                .Clients
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();
        }

        public bool AnyFuelType(string id)
        {
            return data.FuelTypes.Any(c => c.Id == id);
        }

        public bool AnyReairs(string id)
        {
            return data.Repairs.Any(c => c.CarId == id);
        }

        public Car Car(string id)
        {
            return this.data.Cars.Find(id);
        }

        public bool CarExists(string id)
        {
            return data.Cars.Any(e => e.Id == id);
        }

        public IEnumerable<FuelTypeViewModel> GetFuelTypes()
        {
            return data
                .FuelTypes
                .Select(ft => new FuelTypeViewModel
                {
                    Id = ft.Id,
                    Name = ft.Name,

                }).ToList();
        }

        //public bool IsClient(string userId)
        //{
        //    return this.data
        //        .Clients
        //        .Any(u => u.UserId == userId);
        //}

    }
}
