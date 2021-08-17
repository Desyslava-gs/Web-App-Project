using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Data.Models;
using WebApp.Infrastructure;
using WebApp.Models.Clients;
using WebApp.Services.Clients;

namespace WebApp.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService clientService;

        public ClientsController(IClientService clientService )
        {
            this.clientService =clientService;
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

            var alreadyClient = this.clientService.IsClient(userId);

            if (alreadyClient)
            {
                return BadRequest("Вече сте Клиент!");
            }

            if (!ModelState.IsValid)
            {
                return View(client);
            }

            this.clientService.CreateClient(client,userId);

            return RedirectToAction("Index", "Cars");
        }


    }
}
