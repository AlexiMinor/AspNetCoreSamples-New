using FirstMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace FirstMvcApp.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        public async Task Test2Async()
        {
            HttpContext.Response.StatusCode = 500;
            await HttpContext.Response.WriteAsync("Hello world");
            await HttpContext.Response.CompleteAsync();
        }


        [HttpPost]
        public string Generate(WelcomeMessageRequestModel request)
        {
            var smth = HttpContext;

            Request.Form.TryGetValue("isApproved", out StringValues value);

            if (value.ToString().Equals("on"))
            {
                request.IsApproved = true;
            }

            return $"Hello {request.Id}-{request.Name} {request.IsApproved}!";
        }

        [HttpGet]
        public IActionResult Test()
        {
            //return Content("<!DOCTYPE html><html><body><h1>My First Heading</h1><p>My first paragraph.</p></body></html>");

            //return new EmptyResult(); // public void Test()

            //return NoContent(); //204

            //return new FileResult(); - //abstract class
            //return File();
            //return new VirtualFileResult()
            //return new PhysicalFileResult()

            //return new FileStreamResult();

            //return new ObjectResult()

            //return StatusCode()

            //return Unauthorized("");

            //return NotFound();
            //return new NotFoundObjectResult();

            //return BadRequest();
            //return new BadRequestObjectResult();

            return Ok();
            //return Json();
        }

        //[HttpGet]
        //public string Index(WelcomeMessageRequestModel request)
        //{
        //    return $"Hello {request.Id}-{request.Name} {request.IsApproved}!";
        //}

        [HttpGet]
        public int Sum(int[] array)
        {
            var smth = HttpContext;

            return array.Sum();
        }

        [HttpGet]
        public IActionResult Welcome(WelcomeMessageRequestModel[] requests)
        {
            return Ok(requests
                .Select(model => $"Hello {model.Id}-{model.Name} {model.IsApproved}!")
                .ToList());
        }

        [HttpGet]
        public IActionResult BadPractices()
        {
            ViewData["TestData1"] = "Don't use it like this";

            ViewBag.Message = "Don't use it like this";
            ViewBag.Messages = new List<string>()
            {
                "Message A",
                "Message B",
                "Message C",
                "Message D",
            };

            return View();
        }
    }
}
