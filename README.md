# Cyberia-Storage

**Cyberia Storage** is a self-hosted encrypted file storage system built with ASP.NET Core, PostgreSQL, and Docker. It is designed to run on a personal computer, Raspberry Pi, home server, VPS, or private infrastructure.

The initial MVP focuses on personal self-hosted storage. A user should be able to install and configure Cyberia Storage, run it locally or on private infrastructure, and store files in an encrypted form. The goal is to make stored data inaccessible without the user's secret.

The MVP provides user accounts, authentication, configurable registration, storage quotas, metadata management, and basic file upload/download functionality.

The long-term goal is to move toward a client-side encrypted storage flow where files are encrypted before upload and decrypted only after download on the client device. In this model, the server should never receive plaintext file contents or plaintext encryption keys. It should only store encrypted file data and the minimum metadata required for authentication, quota enforcement, and file management.

Encryption and decryption may be implemented on the client through a WebAssembly module written in Rust.

Future versions may extend this design with public server deployment, multi-user access, encrypted file sharing, chunked storage, and Git-inspired content-addressed file storage. This would allow Cyberia Storage to evolve from a personal encrypted storage system into a zero-knowledge file storage and file exchange platform.

More technical details are available in the `docs/` directory:

- `docs/architecture.md` — architecture and MVP scope
- `docs/configuration.md` — server configuration and registration modes
- `docs/errors.md` — structured error reference
- `docs/security.md` — security model and trust boundaries
