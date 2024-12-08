using Microsoft.AspNetCore.Mvc;

namespace RecipeApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(int? statusCode = null)
        {
            //todo: add other pages
            if (!statusCode.HasValue)
            {
                return this.View();
            }

            if (statusCode == 404)
            {
                return this.View("Error404");
            }
            else if (statusCode == 401 || statusCode == 403)
            {
                return this.View("Error403");
            }

            return this.View("Error500");
        }
    }
}
