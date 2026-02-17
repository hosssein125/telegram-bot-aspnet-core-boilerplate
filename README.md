# Telegram Bot Clean Architecture Boilerplate

This is a boilerplate for a Telegram Bot using ASP.NET Core, Clean Architecture, MediatR, and EF Core with PostgreSQL.

## Prerequisites

- .NET 9.0 SDK
- PostgreSQL
- Telegram Bot Token

## Getting Started

1.  **Configure Database and Bot Token**:
    Update `src/TelegramBot.Api/appsettings.json` with your PostgreSQL connection string and Telegram Bot Token.

2.  **Apply Migrations**:
    Open a terminal in the solution root and run:
    ```bash
    dotnet tool install --global dotnet-ef
    dotnet ef migrations add InitialCreate -p src/TelegramBot.Infrastructure -s src/TelegramBot.Api
    dotnet ef database update -p src/TelegramBot.Infrastructure -s src/TelegramBot.Api
    ```

3.  **Run the Application**:
    ```bash
    dotnet run --project src/TelegramBot.Api
    ```

4.  **Set WebHook**:
    You need to set the WebHook for your bot to point to your deployed URL (e.g., using ngrok for local development).
    The endpoint is `POST /api/telegram/update`.

## Structure

- **TelegramBot.Domain**: Entities and enterprise logic.
- **TelegramBot.Application**: Use cases, MediatR commands/queries, interfaces.
- **TelegramBot.Infrastructure**: Persistence (EF Core), External Services (Telegram.Bot).
- **TelegramBot.Api**: Web API entry point, Controllers.
