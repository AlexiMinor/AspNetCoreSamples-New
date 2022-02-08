using FirstMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Data;
using FirstMvcApp.Filters;
using Serilog;

namespace FirstMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NewsAggregatorContext _db;

        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, 
            NewsAggregatorContext db, IEmailSender emailSender)
        {
            _logger = logger;
            _db = db;
            _emailSender = emailSender;
        }

        //[CustomNotFoundFilter]
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Home page");
                await _emailSender.SendEmailAsync("1", "2", "3");
                var topRatedNews = _db.Articles
                    .OrderByDescending(article => article.PositivityRate)
                    .Select(article => new TopRatedNewsHomeScreenViewModel()
                    {
                        Id = article.Id,
                        Title = article.Title
                    });

                throw new ArgumentNullException();
            }
            catch (Exception e)
            {
                Log.Fatal(e, $"{e.Message} \n Stack trace: {e.StackTrace}");

            }

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