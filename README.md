# Cyberia-Storage

**Cyberia Storage** is a self-hosted encrypted file storage system built with ASP.NET Core, PostgreSQL, and Docker. It is designed to run on a personal computer, Raspberry Pi, home server, VPS, or private infrastructure.

The initial MVP focuses on personal self-hosted storage. A user should be able to install and configure Cyberia Storage, run it locally or on private infrastructure, and store files in an encrypted form. The goal is to make stored data inaccessible without the user's secret.

The MVP provides user accounts, authentication, configurable registration, storage quotas, metadata management, and basic file upload/download functionality.

The long-term goal is to move toward a client-side encrypted storage flow where files are encrypted before upload and decrypted only after download on the client device. In this model, the server should never receive plaintext file contents or plaintext encryption keys. It should only store encrypted file data and the minimum metadata required for authentication, quota enforcement, and file management.

Encryption and decryption may be implemented on the client through a WebAssembly module written in Rust.

Future versions may extend this design with public server deployment, multi-user access, encrypted file sharing, chunked storage, and Git-inspired content-addressed file storage. This would allow Cyberia Storage to evolve from a personal encrypted storage system into a zero-knowledge file storage and file exchange platform.


---

## Architecture and MVP Scope

### MVP Scope

Cyberia Storage MVP is focused on the backend API, self-hosted deployment, authentication, storage quotas, metadata management, and file upload/download flow.

A graphical web interface is not included in the initial MVP. The first version is intended to be API-first and CLI-friendly, so the core storage and security logic can be built and tested before adding a UI layer.

A web dashboard may be added in a future version. Possible options include ASP.NET Core Razor/Blazor or a separate frontend application. The UI layer should remain separate from the core backend logic.

### Database Choice

The MVP uses PostgreSQL as the primary database. This provides a production-like environment for authentication, user management, storage quotas, metadata, and future multi-user deployment.

Although Cyberia Storage is designed as a self-hosted project, SQLite may be added later as a lightweight option for single-user or small private installations. The current architecture keeps database-specific logic inside the Infrastructure layer and uses EF Core to reduce coupling between the application logic and a specific database provider.

The project avoids PostgreSQL-specific database features where possible, such as database-generated UUIDs or provider-specific SQL functions. IDs are intended to be generated in application code, which should make future SQLite support easier to add.
