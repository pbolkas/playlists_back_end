# playlists back end using asp.net core

This is the back-end of the playlists application where we implement all the API endpoints.

You can clone this repository locally and create branches to develop new features.
You can open and work with this project using visual studio or visual studio code.

Build and run commands
===
In order to build the project run the following commands:
- dotnet publish -c Release -o out
- docker build -t pbolkas/back_end:0.0.1 . (builds new image, note the tag)

Migration Scripts
===
- dotnet ef migrations add <MIGRATION_NAME>
- dotnet ef database update <MIGRATION_NAME_OPTIONAL>
- dotnet ef migrations script -o "migration_file.sql"