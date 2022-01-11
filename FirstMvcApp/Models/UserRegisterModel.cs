namespace FirstMvcApp.Models;

public class UserRegisterModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }

    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }

    public DateTime BirthDate { get; set; }
    public string Country { get; set; }
    public string AddressLine1{ get; set; }
    public string AddressLine2{ get; set; }
    
    public Guid RoleId { get; set; }


}