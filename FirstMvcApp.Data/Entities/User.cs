using FirstMvcApp.Data.Entities;

namespace FirstMvcApp.Data;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public DateTime RegistrationDate { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}