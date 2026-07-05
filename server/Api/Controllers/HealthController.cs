using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    private readonly AppDbContext _db;

    public HealthController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("db")]
    public async Task<IActionResult> CheckDataBase()
    {
        var canConnect = await _db.Database.CanConnectAsync();

        if (!canConnect)
        {
            return StatusCode(503, new
            {
                database = "unavailable"
            });
        }

        var usersCount = await _db.Users.CountAsync();
        var tokensCount = await _db.Tokens.CountAsync();

        return Ok(new
        {
            database = "ok",
            usersCount,
            tokensCount
        });
    }
}
