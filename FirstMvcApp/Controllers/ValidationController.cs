using FirstMvcApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstMvcApp.Controllers
{
    public class ValidationController : Controller
    {
        private readonly NewsAggregatorContext _db;

        public ValidationController(NewsAggregatorContext db)
        {
            _db = db;
        }
        [AcceptVerbs("Get","Post")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            if (email.Equals("admin@test.com") || 
                             await _db.Users.AnyAsync(user => user.Email.Equals(email)))
            {
                return Json(false);
            }

            return Json(true);
        }
    }
}
