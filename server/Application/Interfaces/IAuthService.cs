namespace Application.Interfaces;

using Application.DTO.Auth;

public interface IAuthService
{
    Task<Guid> RegisterUserAsync(RegisterUserDto dto);
}
