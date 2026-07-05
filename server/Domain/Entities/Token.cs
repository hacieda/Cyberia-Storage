namespace Domain.Entities;

/*
SQL fragment:

CREATE TABLE IF NOT EXISTS tokens (
    token_id UUID PRIMARY KEY,
    token TEXT UNIQUE NOT NULL,
    storage_limit_gb INT NOT NULL DEFAULT 1,
    expires_at TIMESTAMP NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    is_public BOOLEAN NOT NULL DEFAULT FALSE,
    is_used BOOLEAN NOT NULL DEFAULT FALSE
);
*/

public class Token
{
    public Guid TokenId { get; set; }
    public string TokenValue { get; set; } = "";
    public int StorageLimitGb { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsPublic { get; set; }
    public bool IsUsed { get; set; }
}
