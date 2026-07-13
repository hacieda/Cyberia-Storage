## Registration Modes

Cyberia Storage supports configurable registration modes. This allows the same backend to be used in different self-hosted scenarios: a private personal instance, an invite-only instance, or a closed instance where new public registrations are disabled.

The registration mode is controlled by configuration:

```json
"Registration": {
  "Mode": "Open",
  "DefaultStorageLimitGb": 1
}
```

The same values can also be provided through environment variables:

```yaml
Registration__Mode: "Open"
Registration__DefaultStorageLimitGb: "1"
```

### Open

In `Open` mode, users can register without an activation token.

This mode is useful for a personal or private self-hosted installation where the instance owner wants to create accounts directly without managing invite tokens.

When a user registers in this mode, the initial storage limit is taken from:

```text
Registration.DefaultStorageLimitGb
```

No activation token is attached to the user account.

### InviteOnly

In `InviteOnly` mode, users must provide a valid activation token during registration.

Activation tokens are stored in the database and can define the initial storage limit for the new account. When a token is used successfully, the created user account is linked to that token and the token is marked as used.

This mode is useful when the instance owner wants to control who can create accounts.

In this mode:

```text
Registration requires an activation token.
The token must exist in the database.
The token must not be expired.
The token must not already be used.
The user storage limit is copied from the token.
```

### Disabled

In `Disabled` mode, public registration is turned off.

This mode is useful when the instance owner wants to close new account creation completely, while keeping existing users able to use the system.

In this mode, registration requests are rejected by the backend.

### Current MVP Behavior

The initial MVP focuses on backend-controlled registration. The registration mode is configured on the server side and applied during user creation.

Supported modes:

```text
Open        - registration without activation token
InviteOnly  - registration requires a valid activation token
Disabled    - registration endpoint rejects new users
```
