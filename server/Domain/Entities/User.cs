namespace Domain.Entities;

/*
SQL fragment:

CREATE TABLE IF NOT EXISTS users (
    user_id UUID PRIMARY KEY,
    username TEXT UNIQUE NOT NULL,
    email TEXT UNIQUE NOT NULL,
    password_hash TEXT NOT NULL,

    role TEXT NOT NULL DEFAULT 'user'
        CHECK (role IN ('user', 'admin')),

    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    activation_token_id UUID NULL REFERENCES tokens(token_id),

    storage_limit_gb INT NOT NULL DEFAULT 1,
    storage_used_mb BIGINT NOT NULL DEFAULT 0
);
*/

public class User
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string Role { get; set; } = "user";
    public DateTime CreatedAt { get; set; }
    public Guid? ActivationTokenId { get; set; }
    public int StorageLimitGb { get; set; }
    public long StorageUsedMb { get; set; }
}
