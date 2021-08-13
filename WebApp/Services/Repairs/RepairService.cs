using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models.Repairs;
using WebApp.Data;
using WebApp.Data.Models;

namespace WebApp.Services.Repairs
{
    public class RepairService : IRepairService
    {
        private readonly CarRepairDbContext data;

        public RepairService(CarRepairDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<IndexRepairAllViewModel>AllRepairsForCar(string id)
        {
            return data.Repairs
                .OrderBy(c=>c.Car.Repairs.Count())
                .Where(c => c.Car.Id == id)
                .Select(c => new IndexRepairAllViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Price = c.Price,
                    StartDate = c.StartDate,
                    CarId = c.CarId,
                    EndDate = c.EndDate,
                    Description = c.Description,
                    RepairTypeId = c.RepairType.Name,
                    CarTitle = c.Car.Make+" "+c.Car.Model
                }).ToList();
        }

        public Repair GetRepairId(string id)
        {
            return this. data.Repairs.Where(r => r.Id == id).FirstOrDefault();
        }

        public void CreateRepair(CreateRepairFormModel repair, string id)
        {
            var repairData = new Repair()
            {
                //Id = repair.Id,
                Name = repair.Name,
                Price = repair.Price,
                StartDate = repair.StartDate,
                EndDate = repair.EndDate,
                Description = repair.Description,
                CarId =id,
                RepairTypeId = repair.RepairTypeId,
                Parts = new List<Part>(),
            };

            this.data.Repairs.Add(repairData);
            this.data.SaveChanges();
        }
        //public bool EditRepairs(string id)
        //{
        //    var repair= new EditRepairFormModel
        //    {
        //        RepairTypes = this.GetRepairTypes(),
        //        RepairTypeId = repair.RepairTypeId,
        //        Id = repair.Id,
        //        Name = repair.Name,
        //        Price = repair.Price,
        //        CarId = repair.CarId,
        //        StartDate = repair.StartDate,
        //        EndDate = repair.EndDate,
        //        Description = repair.Description,

        //    };
        //}


        public IEnumerable<RepairTypeViewModel> GetRepairTypes()
        {
            return data
                .RepairTypes
                .Select(ft => new RepairTypeViewModel
                {
                    Id = ft.Id,
                    Name = ft.Name,

                }).ToList();
        }

        public bool RepairTypesExists(string RepairTypeId)
        {
            return this.data.RepairTypes.Any(c => c.Id == RepairTypeId);
        }
        public bool RepairExists(string id)
        {
            return data.Repairs.Any(e => e.Id == id);
        }
    }

   
}
