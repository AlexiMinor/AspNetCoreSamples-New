using FirstMvcApp.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstMvcApp.Models;

public class UserRegisterViewModel
{
    public UserRegisterModel Model { get; set; }
    public IEnumerable<SelectListItem>  Roles{ get; set; }
}