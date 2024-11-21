using Microsoft.AspNetCore.Mvc;

namespace RecipeApp.Web.Controllers
{
    public class FavoritesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
