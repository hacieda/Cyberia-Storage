CREATE TABLE IF NOT EXISTS tokens (
    token_id UUID PRIMARY KEY,
    token TEXT UNIQUE NOT NULL,
    storage_limit_gb INT NOT NULL DEFAULT 1,
    expires_at TIMESTAMP NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    is_public BOOLEAN NOT NULL DEFAULT FALSE,
    is_used BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE IF NOT EXISTS users (
    user_id UUID PRIMARY KEY,
    username TEXT UNIQUE NOT NULL,
    email TEXT UNIQUE NOT NULL,
    password_hash TEXT NOT NULL,

    -- Temporary
    role TEXT NOT NULL DEFAULT 'user' CHECK (role IN ('user', 'admin')),

    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    activation_token_id UUID NULL REFERENCES tokens(token_id),
    storage_limit_gb INT NOT NULL DEFAULT 1,
    storage_used_mb BIGINT NOT NULL DEFAULT 0
);

INSERT INTO tokens (
    token_id,
    token,
    storage_limit_gb,
    expires_at,
    is_public,
    is_used
)
VALUES
('729bbc8d-fe36-4795-b460-5c0516581daa', 'PUBLIC-1GB-01', 1, NULL, TRUE, FALSE),
('c15a0df0-7af0-439f-ad46-080cff3ef155', 'PUBLIC-1GB-02', 1, NULL, TRUE, FALSE),
('f3bd81f7-e1d9-4f04-8a32-18493e755fda', 'PUBLIC-1GB-03', 1, NULL, TRUE, FALSE),
('69f0cd1f-b25e-4877-952f-31e0ddf399ec', 'PUBLIC-1GB-04', 1, NULL, TRUE, FALSE),
('aa4456a0-270e-4adf-83ae-dc89df9f62f2', 'PUBLIC-1GB-05', 1, NULL, TRUE, FALSE),
('50f97307-0b11-41c6-8857-b3efd623ad8c', 'PUBLIC-1GB-06', 1, NULL, TRUE, FALSE),
('b0d8133b-5bd1-47f7-81b3-26841388f2d0', 'PUBLIC-1GB-07', 1, NULL, TRUE, FALSE),
('d7532cfc-d71b-4b21-9b7e-5ae5cb132c64', 'PUBLIC-1GB-08', 1, NULL, TRUE, FALSE),
('91d67a95-b9bf-472c-b760-156f6115ebd0', 'PUBLIC-1GB-09', 1, NULL, TRUE, FALSE),
('4494ee4b-501c-400f-a1b2-bd1859a6588a', 'PUBLIC-1GB-10', 1, NULL, TRUE, FALSE)
ON CONFLICT (token) DO NOTHING;
