using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Api;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    public class WebApiTestController : Controller
    {
        private readonly IValueService _ValueService;

        public WebApiTestController(IValueService valueService)
        {
            _ValueService = valueService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var value = _ValueService.Get();
            return View(value);
        }
    }
}
