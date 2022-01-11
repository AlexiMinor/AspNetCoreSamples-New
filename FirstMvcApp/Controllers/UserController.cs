using FirstMvcApp.Data;
using FirstMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstMvcApp.Controllers
{
    public class UserController : Controller
    {
        private readonly NewsAggregatorContext _db;

        public UserController(NewsAggregatorContext db)
        {
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginModel model)
        {
            return RedirectToAction("Index","Home");
        } 
        
        public IActionResult Register()
        {
            var roles = _db.Roles.ToList();
            var model = new UserRegisterViewModel()
            {
                Roles = roles.Select(role => new SelectListItem(role.Name, role.Id.ToString()))
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Register(UserRegisterModel model)
        {
            return RedirectToAction("Index","Home");
        }

        public IActionResult UserDetail(UserRegisterModel model)
        {
            return View();
        }
    }
}
