using FirstMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstMvcApp.Controllers
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

        public IActionResult About()
        {
            var model = new AboutModel()
            {
                Email = "test@email.com",
                Phone = "+123 456 789",
                Address = "Blalblbalbalbalabllab"
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult List()
        {
            var list = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                list.Add(i);
            }
            return View(list);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}