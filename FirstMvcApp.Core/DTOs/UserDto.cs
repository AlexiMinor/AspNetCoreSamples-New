namespace FirstMvcApp.Core.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string? NormalizedEmail { get; set; }

    public string? PasswordHash { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public string[] RoleNames { get; set; }
}