using FirstMvcApp.Domain.Services;
using FirstMvcApp.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FirstMvcApp.Controllers
{
    //[SampleResourceFilter]
    public class SpyController : Controller
    {
        //[SampleResourceFilter]
        [ServiceFilter(typeof(CustomFilter))]
        [SampleResultFilter]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [SampleActionFilter]
        public IActionResult TestId(int id)
        {
            return Ok(id);
        }

        [SampleExceptionFilter]
        public IActionResult Exc()
        {
            int x = 0;
            int y = 1/x;
            return Ok(y);
        }
    }
}

