using System.ComponentModel.DataAnnotations;
using FirstMvcApp.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstMvcApp.Models;

public class UserRegisterModel
{

    [Required(AllowEmptyStrings = false, ErrorMessage = "hello")]
    [EmailAddress]
    [Remote("CheckEmail", "Validation", ErrorMessage = "that email is already used")]
    [RegisterLogin(new string[]{"1","2"}, ErrorMessage = "Invalid name")]
    public string Email { get; set; }

    [Range(18, 130, ErrorMessage = "You are too young to see content")]
    public int Age { get; set; }

    //[StringLength(256,MinimumLength = 15)]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Compare("Password", ErrorMessage = "Passwords not equal")]
    [Display(Name = "Password Confirmation")]
    [DataType(DataType.Password)]
    public string PasswordConfirmation { get; set; }
    [Required]
    public Guid RoleId { get; set; }

    public IEnumerable<SelectListItem> AvailableRoles { get; set; }
}