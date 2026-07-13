using Application.DTO.Auth;
using Application.Errors;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        try
        {
            var userId = await _authService.RegisterUserAsync(dto);

            return Ok(new
            {
                userId
            });
        }
        catch (AppException ex)
        {
            return BadRequest(new
            {
                code = ex.Code.ToString(),
                              message = ex.Message
            });
        }
    }
}
