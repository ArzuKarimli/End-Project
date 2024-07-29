
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace app.Controllers
{
    public class HomeController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }
    }
}
