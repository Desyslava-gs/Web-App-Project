using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Data.Models;
using WebApp.Infrastructure;
using WebApp.Models.Clients;

namespace WebApp.Controllers
{
    public class ClientsController : Controller
    {
        private readonly CarRepairDbContext data;

        public ClientsController(CarRepairDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Index(ClientFormModel client)
        {
            var userId = this.User.GetId();

            var userIdAlreadyDealer = this.data
                .Clients
                .Any(d => d.UserId == userId);

            if (userIdAlreadyDealer)
            {
                return BadRequest("Вече сте Клиент!");
            }

            if (!ModelState.IsValid)
            {
                return View(client);
            }

            var clientData = new Client
            {
                Name = client.Name,
                PhoneNumber = client.PhoneNumber,
                UserId = userId
            };

            this.data.Clients.Add(clientData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Cars");
        }


    }
}
