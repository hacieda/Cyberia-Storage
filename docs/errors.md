# Error Reference

Cyberia Storage uses structured application errors.

Each known application error should have:

- Stable error code
- Human-readable message
- Source area
- Possible causes
- Related configuration
- Suggested action

## Auth Errors

### AUTH_REGISTRATION_DISABLED

Registration is disabled by server configuration.

Source:
- `Infrastructure/Services/AuthService.cs`
- `RegisterUserAsync`

Related configuration:
- `Registration.Mode = Disabled`

Suggested action:
- Change `Registration.Mode` to `Open` or `InviteOnly` if registration should be allowed.
