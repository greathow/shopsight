namespace PaperID.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact the ShopSight team at the email address below.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
