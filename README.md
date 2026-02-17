# TelegramBot — Clean Architecture (simple)

A minimal, clean-architecture Telegram bot built with ASP.NET Core, MediatR, and EF Core (PostgreSQL).

## Quick overview

- Language: C# / .NET 9
- Database: PostgreSQL
- Entry point: `src/TelegramBot.Api`
- Webhook endpoint: `POST /api/telegram/update`

## Quick start

1. Clone the repo:

   ```bash
   git clone <repo-url>
   cd TelegramBot
   ```

2. Configure settings:

   - Edit `src/TelegramBot.Api/appsettings.json` (or `appsettings.Development.json`) and set `ConnectionStrings` and `Telegram:BotToken`.

3. Database migrations (automatic)

   - The application applies any pending EF Core migrations automatically at startup (`db.Database.MigrateAsync()` in `src/TelegramBot.Api/Program.cs`). The repository already contains the initial migration under `src/TelegramBot.Infrastructure/Persistence/Migrations/` so the database will be created/updated when you run the API.

   - When you should run the EF CLI manually:
     - To *create* a new migration after changing domain/models:
       ```bash
       dotnet tool install --global dotnet-ef
       dotnet ef migrations add <Name> -p src/TelegramBot.Infrastructure -s src/TelegramBot.Api
       ```
     - To *apply* migrations manually (e.g., in CI/CD or if you disable auto-migrate in production):
       ```bash
       dotnet ef database update -p src/TelegramBot.Infrastructure -s src/TelegramBot.Api
       ```
     - To troubleshoot migration errors separately before runtime.

   - For normal local development: just run the API (`dotnet run --project src/TelegramBot.Api`) and migrations will be applied automatically.

4. Run the API locally:

   ```bash
   dotnet run --project src/TelegramBot.Api
   ```

5. For local webhook testing use `ngrok` and point your bot webhook to `https://<ngrok-id>.ngrok.io/api/telegram/update`.

## Project layout

- `TelegramBot.Domain` — entities & enums
- `TelegramBot.Application` — use cases, DTOs, interfaces
- `TelegramBot.Infrastructure` — EF Core, persistence, external services
- `TelegramBot.Api` — controllers, configuration, startup

## Contributing

Issues and PRs are welcome — keep changes small and focused.

## Notes

- Ensure `.NET 9 SDK` and `PostgreSQL` are installed before running.

