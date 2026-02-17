using Microsoft.EntityFrameworkCore;
using Serilog;
using TelegramBot.Application;
using TelegramBot.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog(Log.Logger, dispose: true);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the bot client as a hosted service if we wanted polling, but here we use WebHook.
// For WebHook, we just need the Controller.

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// migrate and seed
using (var scope = app.Services.CreateScope())
{

    var db = scope.ServiceProvider.GetRequiredService<TelegramBot.Infrastructure.Persistence.ApplicationDbContext>();
    await db.Database.MigrateAsync();

    var settings = scope.ServiceProvider.GetRequiredService<TelegramBot.Application.Common.Interfaces.ISettingsService>();
    var lang = await settings.GetAsync(TelegramBot.Application.Common.Settings.SettingsKeys.LanguageCode) ?? "";
    if (string.IsNullOrWhiteSpace(lang))
        await settings.SetAsync(TelegramBot.Application.Common.Settings.SettingsKeys.LanguageCode, "fa");

    var adminId = await settings.GetAsync(TelegramBot.Application.Common.Settings.SettingsKeys.AdminTelegramId) ?? "";
    if (string.IsNullOrWhiteSpace(adminId))
        await settings.SetAsync(TelegramBot.Application.Common.Settings.SettingsKeys.AdminTelegramId, "");
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
