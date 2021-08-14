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
            return this.data.Repairs
                .OrderBy(c=>c.Car.Repairs.Count())
                .Where(c => c.Car.Id == id)
                .Select(c => new IndexRepairAllViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Price = c.Price,
                    CarId = c.CarId,
                    StartDate = c.StartDate.Value,
                    EndDate = c.EndDate.Value,
                    Description = c.Description,
                    RepairTypeId = c.RepairType.Name,
                    CarTitle = c.Car.Make+" "+c.Car.Model
                }).ToList();
        }

        public DetailsRepairViewModel DetailsRepair(string id)
        {
            return this.data.Repairs
                .Where(m => m.Id == id)
                .Select(c => new DetailsRepairViewModel
                {
                    Id = c.Id,
                    CarTitle = c.Car.Make + " " + c.Car.Model + " " + c.Car.Year,
                    Description = c.Description,
                    StartDate = c.StartDate.Value,
                    EndDate = c.EndDate.Value,
                    Name = c.Name,
                    PictureUrl = c.Car.PictureUrl,
                    Price = c.Price,
                    CarId = c.CarId,
                    RepairTypeId = c.RepairType.Name,
                    PartName = "",
                    Parts = new List<Part>(),

                }).ToList()
                .FirstOrDefault();
        }

        

        public void CreateRepairs(CreateRepairFormModel repair, string id)
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
        public void EditRepairs(string id, EditRepairFormModel repair)
        {
            var repairData =  new Repair
            {
                Id = repair.Id,
                Name = repair.Name,
                Price = repair.Price,
                StartDate = repair.StartDate,
                EndDate = repair.EndDate,
                Description = repair.Description,
                CarId = repair.CarId,
                RepairTypeId = repair.RepairTypeId,
                Parts = new List<Part>(),
            };
            this.data.Update(repairData);
            this.data.SaveChanges();
        }

        public DeleteRepairViewModel DeleteRepair(string id)
        {
           return this.data.Repairs
                .Select(r => new DeleteRepairViewModel
                {
                    Name = r.Name,
                    Price = r.Price,
                    StartDate = r.StartDate.ToString(),
                    EndDate = r.EndDate.ToString(),
                    Description = r.Description,
                    CarId = r.CarId,
                    Id = r.Id

                }).ToList()
                .FirstOrDefault(m => m.Id == id);
        }

        public void DeleteConfirmed(string id)
        {
           var repair = this.data.Repairs.Find(id);
            this.data.Repairs.Remove(repair);
            this.data.SaveChanges(); 
            //id = repair.CarId;
        }

        public Repair GetRepairId(string id)
        {
            return this. data.Repairs.FirstOrDefault(r => r.Id == id);
        }
        public Repair Repair(string id)
        {
            return this.data.Repairs.Find(id);
        }
        public IEnumerable<RepairTypeViewModel> GetRepairTypes()
        {
            return this.data
                .RepairTypes
                .Select(ft => new RepairTypeViewModel
                {
                    Id = ft.Id,
                    Name = ft.Name,

                }).ToList();
        }

        public bool RepairTypesExists(string rtId)
        {
            return this.data.RepairTypes.Any(c => c.Id == rtId);
        }
        public bool RepairExists(string id)
        {
            return this.data.Repairs.Any(e => e.Id == id);
        }
    }

   
}
