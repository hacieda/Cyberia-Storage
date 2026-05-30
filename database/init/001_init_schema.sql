CREATE EXTENSION IF NOT EXISTS pgcrypto;

CREATE TABLE IF NOT EXISTS tokens (
    token_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),

    token TEXT UNIQUE NOT NULL,

    storage_limit_gb INT NOT NULL,

    expires_at TIMESTAMP NULL,

    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    is_public BOOLEAN NOT NULL DEFAULT FALSE,

    is_used BOOLEAN NOT NULL DEFAULT FALSE,

    activated_at TIMESTAMP NULL,

    activated_by_user_id UUID NULL
);

CREATE TABLE IF NOT EXISTS users (
    user_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),

    username TEXT UNIQUE NOT NULL,

    email TEXT UNIQUE NOT NULL,

    password_hash TEXT NOT NULL,

    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    activation_token_id UUID NULL REFERENCES tokens(token_id),

    storage_limit_gb INT NOT NULL DEFAULT 0,

    storage_used_mb BIGINT NOT NULL DEFAULT 0
);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM pg_constraint
        WHERE conname = 'fk_token_user'
    ) THEN
        ALTER TABLE tokens
        ADD CONSTRAINT fk_token_user
        FOREIGN KEY (activated_by_user_id) REFERENCES users(user_id);
    END IF;
END $$;

INSERT INTO tokens (token, storage_limit_gb, expires_at, is_public, is_used)
VALUES
('PUBLIC-1GB-01', 1, NULL, TRUE, FALSE),
('PUBLIC-1GB-02', 1, NULL, TRUE, FALSE),
('PUBLIC-1GB-03', 1, NULL, TRUE, FALSE),
('PUBLIC-1GB-04', 1, NULL, TRUE, FALSE),
('PUBLIC-1GB-05', 1, NULL, TRUE, FALSE),
('PUBLIC-1GB-06', 1, NULL, TRUE, FALSE),
('PUBLIC-1GB-07', 1, NULL, TRUE, FALSE),
('PUBLIC-1GB-08', 1, NULL, TRUE, FALSE),
('PUBLIC-1GB-09', 1, NULL, TRUE, FALSE),
('PUBLIC-1GB-10', 1, NULL, TRUE, FALSE)
ON CONFLICT (token) DO NOTHING;
