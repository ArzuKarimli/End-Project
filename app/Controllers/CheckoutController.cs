using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
