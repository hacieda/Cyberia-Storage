using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/*
Database mapping summary:

SQL tables:

CREATE TABLE users (
    user_id UUID PRIMARY KEY,
    username TEXT UNIQUE NOT NULL,
    email TEXT UNIQUE NOT NULL,
    password_hash TEXT NOT NULL,
    role TEXT NOT NULL DEFAULT 'user',
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    activation_token_id UUID NULL REFERENCES tokens(token_id),
    storage_limit_gb INT NOT NULL DEFAULT 1,
    storage_used_mb BIGINT NOT NULL DEFAULT 0
);

CREATE TABLE tokens (
    token_id UUID PRIMARY KEY,
    token TEXT UNIQUE NOT NULL,
    storage_limit_gb INT NOT NULL DEFAULT 1,
    expires_at TIMESTAMP NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    is_public BOOLEAN NOT NULL DEFAULT FALSE,
    is_used BOOLEAN NOT NULL DEFAULT FALSE
);
*/

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Token> Tokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /*
        C# User property      SQL column
        --------------------------------
        UserId                user_id
        PasswordHash          password_hash
        ActivationTokenId     activation_token_id
        StorageLimitGb        storage_limit_gb
        StorageUsedMb         storage_used_mb
        */

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(e => e.UserId);

            entity.Property(e => e.UserId)
                .HasColumnName("user_id");

            entity.Property(e => e.Username)
                .HasColumnName("username");

            entity.Property(e => e.Email)
                .HasColumnName("email");

            entity.Property(e => e.PasswordHash)
                .HasColumnName("password_hash");

            entity.Property(e => e.Role)
                .HasColumnName("role");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            entity.Property(e => e.ActivationTokenId)
                .HasColumnName("activation_token_id");

            entity.Property(e => e.StorageLimitGb)
                .HasColumnName("storage_limit_gb");

            entity.Property(e => e.StorageUsedMb)
                .HasColumnName("storage_used_mb");
        });

        /*
        C# Token property     SQL column
        --------------------------------
        TokenId               token_id
        TokenValue            token
        StorageLimitGb        storage_limit_gb
        ExpiresAt             expires_at
        CreatedAt             created_at
        IsPublic              is_public
        IsUsed                is_used
        */

        modelBuilder.Entity<Token>(entity =>
        {
            entity.ToTable("tokens");

            entity.HasKey(e => e.TokenId);

            entity.Property(e => e.TokenId)
                .HasColumnName("token_id");

            entity.Property(e => e.TokenValue)
                .HasColumnName("token");

            entity.Property(e => e.StorageLimitGb)
                .HasColumnName("storage_limit_gb");

            entity.Property(e => e.ExpiresAt)
                .HasColumnName("expires_at");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            entity.Property(e => e.IsPublic)
                .HasColumnName("is_public");

            entity.Property(e => e.IsUsed)
                .HasColumnName("is_used");
        });
    }
}
