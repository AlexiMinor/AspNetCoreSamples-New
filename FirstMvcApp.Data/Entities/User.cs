namespace FirstMvcApp.Data.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string? NormalizedEmail { get; set; }
    
    public string? PasswordHash { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
}