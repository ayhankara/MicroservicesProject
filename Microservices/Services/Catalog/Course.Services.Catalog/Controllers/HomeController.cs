using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Catalog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
