# Cyberia-Storage

**Cyberia Storage** is a self-hosted file storage system built with ASP.NET Core, PostgreSQL, and Docker. It is designed to run on a Raspberry Pi, home server, VPS, or private infrastructure.

The MVP provides user accounts, configurable registration, storage quotas, metadata management, and file upload/download. The final MVP target is a client-side encrypted storage flow where files are encrypted before upload and decrypted only after download on the client device.

Encryption and decryption are planned to run on the client through a WebAssembly module written in C++. The server should never receive plaintext file contents or encryption keys. It only stores encrypted file data and the minimum metadata required for authentication, quota enforcement, and file management.

Future versions will extend this design with chunked storage and Git-inspired content-addressed file storage, allowing files to be stored as encrypted chunks and prepared for more efficient synchronization and versioning.
