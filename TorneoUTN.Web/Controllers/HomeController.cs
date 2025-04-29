using Microsoft.AspNetCore.Mvc;

namespace TorneoUTN.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}