namespace Domain.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = default!
    public string Email { get; set; } = default!
    public string PasswordHash { get; set; } = default!
    public string ActivationToken { get; set; } = default!
    public DateTime CreatedAt { get; set; }
}
