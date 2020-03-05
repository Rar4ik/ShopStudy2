using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _Logger;

        public HomeController(ILogger<HomeController> Logger)
        {
            _Logger = Logger;
        }
        public IActionResult Index()
        {
            _Logger.LogInformation("Запрос главной страницы");
            return View();
        }

        public IActionResult Blog() => View();
        public IActionResult BlogSingle() => View();
        public IActionResult Cart() => View();
        public IActionResult CheckOut() => View();
        public IActionResult ContactUs() => View();
        public IActionResult Login() => View();
        public IActionResult ProductDetails() => View();
        public IActionResult Shop() => View();
        public IActionResult Error404() => View();
    }
}