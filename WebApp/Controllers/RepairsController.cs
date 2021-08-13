using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Models;
using WebApp.Models.Repairs;
using WebApp.Services.Repairs;

namespace WebApp.Controllers
{
    public class RepairsController : Controller
    {
        private readonly CarRepairDbContext data;
        private readonly IRepairService repairService;


        public RepairsController(
            CarRepairDbContext data,
            IRepairService repairService)
        {
            this.data = data;
            this.repairService = repairService;
        }

        //public IActionResult IndexAdmin()
        //{

        //    var repairs = data.Repairs
        //        .OrderBy(c => c.Car.Repairs.Count())
        //        .Select(c => new IndexRepairAllViewModel
        //        {
        //            Id = c.Id,
        //            Name = c.Name,
        //            Price = c.Price,
        //            StartDate = c.StartDate,
        //            CarId = c.CarId,
        //            EndDate = c.EndDate,
        //            Description = c.Description,
        //            RepairTypeId = c.RepairType.Name,
        //            CarTitle = c.Car.Make + " " + c.Car.Model
        //        }).ToList();
        //    //if (!repairs.Any())
        //    //{
        //    //    return RedirectToAction("Non");
        //    //    // return RedirectToAction("Create", "Repairs", new { id });
        //    //}
        //    return View(repairs);

        //}


        public IActionResult Non()
        {
            return View();
        }

        public IActionResult Index(string id)
        {

            var repairs = repairService.GetAllRepairsCars(id);
            if (!repairs.Any())
            {
                if (!UserIsAdmin())
                {
                    return RedirectToAction("Non");
                }
                return RedirectToAction("Create", "Repairs", new { id });
            }
            return View(repairs);

        }

        // GET: Repairs/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = this.data.Repairs
                .Where(m => m.Id == id)
                .Select(c => new DetailsRepairViewModel
                {
                    Id = c.Id,
                    CarTitle = c.Car.Make + " " + c.Car.Model + " " + c.Car.Year,
                    Description = c.Description,
                    StartDate = c.StartDate.ToString(),
                    EndDate = c.EndDate.ToString(),
                    Name = c.Name,
                    PictureUrl = c.Car.PictureUrl,
                    Price = c.Price,
                    CarId = c.CarId,
                    RepairTypeId = c.RepairType.Name,
                    Parts = new List<Part> { }

                }).ToList()
                .FirstOrDefault();
            if (repair == null)
            {
                return NotFound();
            }

            return View(repair);
        }

        // GET: Repairs/Create
        [Authorize(Roles = WebConstants.AdminRoleName)]
        public IActionResult Create([FromRoute] string id)
        {
            //if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            var repairs = new CreateRepairFormModel
            {
                CarId = id,
                RepairTypes = repairService.GetRepairTypes()
            };
            return View(repairs);
        }


        // POST: Repairs/Create
        [HttpPost]
        [Authorize(Roles = WebConstants.AdminRoleName)]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateRepairFormModel repair, string id)
        {
            if (!repairService.RepairTypesExists(repair.RepairTypeId))
            {
                this.ModelState.AddModelError(nameof(repair.RepairTypeId), "FuelType does not exist.");
            }

            if (!ModelState.IsValid)
            {
                repair.CarId = id;
                repair.RepairTypes = repairService.GetRepairTypes();
                return View(repair);
            }

            this.repairService.CreateRepairs(repair, id);

            return RedirectToAction("Index", "Repairs", new { id });
        }

        // GET: Repairs/Edit/5
        [Authorize(Roles = WebConstants.AdminRoleName)]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = repairService.GetRepairId(id);

            if (repair == null)
            {
                return NotFound();
            }
            var repairData = new EditRepairFormModel
            {
                RepairTypes = repairService.GetRepairTypes(),
                RepairTypeId = repair.RepairTypeId,
                Id = repair.Id,
                Name = repair.Name,
                Price = repair.Price,
                CarId = repair.CarId,
                StartDate = repair.StartDate,
                EndDate = repair.EndDate,
                Description = repair.Description,

            };
            return View(repairData);
        }

        // POST: Repairs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WebConstants.AdminRoleName)]
        public IActionResult Edit(string id, EditRepairFormModel repair)
        {
            if (id != repair.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var repairData = new Repair
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
                    data.Update(repairData);
                    data.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!repairService.RepairExists(repair.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                id = repair.CarId;
                return RedirectToAction("Index", new { id });
            }
            return View(repair);
        }

        // GET: Repairs/Delete/5
        [Authorize(Roles = WebConstants.AdminRoleName)]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = data.Repairs
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
            if (repair == null)
            {
                return NotFound();
            }

            return View(repair);
        }

        // POST: Repairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = WebConstants.AdminRoleName)]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {

            var repair = data.Repairs.Find(id);
            id = repair.CarId;
            data.Repairs.Remove(repair);
            data.SaveChanges();
            return RedirectToAction("Index", "Repairs", new { id });
        }

        private bool UserIsAdmin()
        {
            var userInRole = this.User.IsInRole(WebConstants.AdminRoleName);

            return userInRole;
        }
    }
}
