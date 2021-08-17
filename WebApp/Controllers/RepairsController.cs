using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Infrastructure;
using WebApp.Models.Repairs;
using WebApp.Services.Repairs;
using static WebApp.WebConstants;
namespace WebApp.Controllers
{
    public class RepairsController : Controller
    {

        private readonly IRepairService repairService;

        public RepairsController(IRepairService repairService)
        {
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

            var repairs = repairService.AllRepairsForCar(id);
            if (!repairs.Any())
            {
                return !this.User.IsAdmin() ? RedirectToAction("Non") : RedirectToAction("Create", "Repairs", new { id });
            }
            return View(repairs);

        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = repairService.DetailsRepair(id);

            if (repair == null)
            {
                return NotFound();
            }

            return View(repair);
        }

        [Authorize(Roles = AdminRoleName)]
        public IActionResult Create([FromRoute] string id)
        {
            var repairs = new CreateRepairFormModel
            {
                CarId = id,
                RepairTypes = repairService.GetRepairTypes()
            };
            return View(repairs);
        }

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
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

        [Authorize(Roles = AdminRoleName)]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WebConstants.AdminRoleName)]
        public IActionResult Edit(string id, EditRepairFormModel repair)
        {
            if (id != repair.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                repair.CarId = id;
                repair.RepairTypes = repairService.GetRepairTypes();
                return View(repair);
            }
            try
            {
                this.repairService.EditRepairs(id, repair);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!repairService.RepairExists(repair.Id))
                {
                    return NotFound();
                }

            }
            id = repair.CarId;
            return RedirectToAction("Index", new { id });
        }

        [Authorize(Roles = AdminRoleName)]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = this.repairService.DeleteRepair(id);

            if (repair == null)
            {
                return NotFound();
            }

            return View(repair);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = AdminRoleName)]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            if(repairService.AnyPart(id))
            {
                return   RedirectToAction("Error");
            }

            var repair = this.repairService.Repair(id);
            this.repairService.DeleteConfirmed(id);
            id = repair.CarId;
            return RedirectToAction("Index", "Repairs", new { id });
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
