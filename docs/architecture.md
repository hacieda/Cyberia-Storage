## Architecture and MVP Scope

### MVP Scope

Cyberia Storage MVP is focused on the backend API, self-hosted deployment, authentication, storage quotas, metadata management, and file upload/download flow.

A graphical web interface is not included in the initial MVP. The first version is intended to be API-first and CLI-friendly, so the core storage and security logic can be built and tested before adding a UI layer.

A web dashboard may be added in a future version. Possible options include ASP.NET Core Razor/Blazor or a separate frontend application. The UI layer should remain separate from the core backend logic.

### Database Choice

The MVP uses PostgreSQL as the primary database. This provides a production-like environment for authentication, user management, storage quotas, metadata, and future multi-user deployment.

Although Cyberia Storage is designed as a self-hosted project, SQLite may be added later as a lightweight option for single-user or small private installations. The current architecture keeps database-specific logic inside the Infrastructure layer and uses EF Core to reduce coupling between the application logic and a specific database provider.

The project avoids PostgreSQL-specific database features where possible, such as database-generated UUIDs or provider-specific SQL functions. IDs are intended to be generated in application code, which should make future SQLite support easier to add.
