using Application.DTO.Auth;
using Application.Errors;
using Application.Interfaces;
using Application.Options;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly RegistrationOptions _registrationOptions;

    public AuthService(
        AppDbContext db,
        IOptions<RegistrationOptions> registrationOptions)
    {
        _db = db;
        _registrationOptions = registrationOptions.Value;
    }

    public async Task<Guid> RegisterUserAsync(RegisterUserDto dto)
    {

        // Warning: Registration in the `api/appsettings.json` file is "disabled"
        if (_registrationOptions.Mode == RegistrationMode.Disabled)
        {
            throw new AppException(
                ErrorCode.AUTH_REGISTRATION_DISABLED,
                "Registration is disabled.");

            /*
                "AllowedHosts": "*",
                "Registration": {
                    "Mode": "Disabled",
                    "DefaultStorageLimitGb": 1
                }
            */
        }

        // The "IsNullOrWhiteSpace" function returns true if the field is empty or contains spaces
        if (string.IsNullOrWhiteSpace(dto.Username))
        {
            throw new AppException(
                ErrorCode.AUTH_USERNAME_REQUIRED,
                "Username is required.");
        }

        if (string.IsNullOrWhiteSpace(dto.Email))
        {
            throw new AppException(
                ErrorCode.AUTH_EMAIL_REQUIRED,
                "Email is required.");
        }

        if (string.IsNullOrWhiteSpace(dto.Password))
        {
            throw new AppException(
                ErrorCode.AUTH_PASSWORD_REQUIRED,
                "Password is required.");
        }

        // The “Password” and “ConfirmPassword” fields in Application/DTO/Auth/RegisterUserDto.cs do not match
        if (dto.Password != dto.ConfirmPassword)
        {
            throw new AppException(
                ErrorCode.AUTH_PASSWORDS_DO_NOT_MATCH,
                "Passwords do not match.");
        }

        // "dto" is the concrete registration request object created from the client's JSON body
        // "u" represents each User row considered by EF Core
        // The lambda expression checks whether a user's Email matches the Email from the DTO
        // EF Core translates this expression into a SQL query against the users table
        var emailExists = await _db.Users.AnyAsync(u => u.Email == dto.Email);
        if (emailExists)
        {
            throw new AppException(
                ErrorCode.AUTH_EMAIL_ALREADY_REGISTERED,
                "Email is already registered.");
        }

        var usernameExists = await _db.Users.AnyAsync(u => u.Username == dto.Username);
        if (usernameExists)
        {
            throw new AppException(
                ErrorCode.AUTH_USERNAME_ALREADY_TAKEN,
                "Username is already taken.");
        }

        // Retrieves the value for "storageLimitGb" from "DefaultStorageLimitGb" in api/appsettings.json
        var storageLimitGb = _registrationOptions.DefaultStorageLimitGb;
        Guid? activationTokenId = null;

        // if the “Mode” parameter in Api/appsettings.json is set to "InviteOnly"
        if (_registrationOptions.Mode == RegistrationMode.InviteOnly)
        {
            if (string.IsNullOrWhiteSpace(dto.ActivationToken))
            {
                throw new AppException(
                    ErrorCode.AUTH_ACTIVATION_TOKEN_REQUIRED,
                    "Activation token is required.");
            }


            var token = await _db.Tokens
            .FirstOrDefaultAsync(t => t.TokenValue == dto.ActivationToken);

            if (token is null)
            {
                throw new AppException(
                    ErrorCode.AUTH_ACTIVATION_TOKEN_INVALID,
                    "Invalid activation token.");
            }

            if (token.IsUsed)
            {
                throw new AppException(
                    ErrorCode.AUTH_ACTIVATION_TOKEN_ALREADY_USED,
                    "Activation token is already used.");
            }

            if (token.ExpiresAt.HasValue && token.ExpiresAt.Value < DateTime.UtcNow)
            {throw new AppException(
                ErrorCode.AUTH_ACTIVATION_TOKEN_EXPIRED,
                "Activation token has expired.");
            }

            storageLimitGb = token.StorageLimitGb;
            activationTokenId = token.TokenId;
            token.IsUsed = true;
        }

        // BCrypt hashes the password and embeds the salt into the hash string.
        // Only the resulting hash is stored in the database.
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        // Create a new user entity using validated client data and server-controlled values.
        var user = new User
        {
            // Generate the user id in application code instead of relying on database-specific UUID functions.
            // This keeps entity creation more portable if another database provider, such as SQLite, is added later.
            UserId = Guid.NewGuid(),

            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = passwordHash,

            // New users created through public registration must always start with the regular user role.
            // Admin accounts should be created through a separate bootstrap/admin flow.
            Role = "user",

            CreatedAt = DateTime.UtcNow,
            ActivationTokenId = activationTokenId,
            StorageLimitGb = storageLimitGb,

            // A newly registered user has not uploaded any files yet.
            StorageUsedMb = 0
        };

        _db.Users.Add(user);

        await _db.SaveChangesAsync();

        return user.UserId;
    }
}
