using Microsoft.EntityFrameworkCore;
using Serilog;
using TelegramBot.Application;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Settings;
using TelegramBot.Domain.Enums;
using TelegramBot.Infrastructure;
using TelegramBot.Infrastructure.Persistence;

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

    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync();

    var settings = scope.ServiceProvider.GetRequiredService<ISettingsService>();
    var lang = await settings.GetAsync(SettingsKeys.LanguageCode) ?? "";
    if (string.IsNullOrWhiteSpace(lang))
        await settings.SetAsync(SettingsKeys.LanguageCode, "fa");

    var adminId = await settings.GetAsync(SettingsKeys.AdminTelegramId) ?? "";
    if (string.IsNullOrWhiteSpace(adminId))
        await settings.SetAsync(SettingsKeys.AdminTelegramId, "");

    var referralBonusAmount = await settings.GetAsync(SettingsKeys.ReferralBonusAmount) ?? "";
    if (string.IsNullOrWhiteSpace(referralBonusAmount))
        await settings.SetAsync(SettingsKeys.ReferralBonusAmount, "0");

    var referralBonusType = await settings.GetAsync(SettingsKeys.ReferralBonusType) ?? "";
    if (string.IsNullOrWhiteSpace(referralBonusType))
        await settings.SetAsync(SettingsKeys.ReferralBonusType, ReferralBonusType.Constant.ToString());
}


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
