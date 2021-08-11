using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Contacts()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }

        
        public IActionResult Error()
        {
            return View();
        }
    }
}
