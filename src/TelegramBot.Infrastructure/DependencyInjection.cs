using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Interfaces.Repositories;
using TelegramBot.Infrastructure.Persistence;
using TelegramBot.Infrastructure.Persistence.Repositories;
using TelegramBot.Infrastructure.Services;

namespace TelegramBot.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            // database Configs
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            // Telegram Configs
            services.AddSingleton<ITelegramBotClient>(provider =>
            {
                var token = configuration["Telegram:Token"];

                if (string.IsNullOrWhiteSpace(token))
                    throw new System.InvalidOperationException("Telegram bot token not configured. Set 'TelegramToken' in appsettings or TELEGRAM_BOT_TOKEN env var.");

                return new TelegramBotClient(token);
            });

            // Cache service
            services.AddMemoryCache();

            // Custom services
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddTransient<ITelegramService, TelegramService>();

            //Repositories
            services.AddScoped<IDbUnitOfWork, DbUnitOfWork>();
            services.AddScoped<IEfTelegramUserRepository, EfTelegramUserRepository>();


            // Http Clients
            services.AddHttpClient<IThreeXUiService, ThreeXUiService>()
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    UseCookies = true,
                    CookieContainer = new System.Net.CookieContainer()
                });

            services.AddHttpClient<IIbsNgService, IbsNgService>(client =>
            {
                // Base address can be overridden via IbsNgService constructor parameter if needed
            });

            return services;
        }
    }
}
