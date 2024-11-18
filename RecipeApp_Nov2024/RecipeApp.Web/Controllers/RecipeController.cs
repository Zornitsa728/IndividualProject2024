using Microsoft.AspNetCore.Mvc;

namespace RecipeApp.Web.Controllers
{
    public class RecipeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
