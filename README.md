# Cyberia-Storage

**Cyberia Storage** is a self-hosted file storage system built with ASP.NET Core, PostgreSQL, and Docker. It is designed to run on a Raspberry Pi, home server, VPS, or private infrastructure.

The MVP provides user accounts, configurable registration, storage quotas, metadata management, and file upload/download. The final MVP target is a client-side encrypted storage flow where files are encrypted before upload and decrypted only after download on the client device.

Encryption and decryption are planned to run on the client through a WebAssembly module written in C++. The server should never receive plaintext file contents or encryption keys. It only stores encrypted file data and the minimum metadata required for authentication, quota enforcement, and file management.

Future versions will extend this design with chunked storage and Git-inspired content-addressed file storage, allowing files to be stored as encrypted chunks and prepared for more efficient synchronization and versioning.

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
