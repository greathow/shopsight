using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PaperID.Controllers
{
    public class RackDisplayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}