using Microsoft.AspNetCore.Mvc;

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
