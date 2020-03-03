using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _Logger;

        public HomeController(ILogger<HomeController> Logger) => _Logger = Logger;

        public IActionResult Index()
        {
            _Logger.LogInformation("Запрос главной страницы!");
            return View();
        }

        public IActionResult ThrowError(string id) => throw new ApplicationException(id);

        public IActionResult Blog() => View();
        public IActionResult BlogSingle() => View();
        public IActionResult ContactUs() => View();
        public IActionResult Error404() => View();

        public IActionResult ErrorStatus(string Id)
        {
            switch (Id)
            {
                default: return Content($"Статусный код {Id}");
                case "404":
                    return RedirectToAction(nameof(Error404));
            }
        }
    }
}