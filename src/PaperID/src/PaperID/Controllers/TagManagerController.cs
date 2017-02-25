using Microsoft.AspNetCore.Mvc;

namespace PaperID.Controllers
{
    public class TagManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}