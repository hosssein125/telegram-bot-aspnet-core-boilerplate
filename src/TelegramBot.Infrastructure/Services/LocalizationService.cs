using System.Globalization;
using TelegramBot.Application.Common.Interfaces;

namespace TelegramBot.Infrastructure.Services;

public sealed class LocalizationService : ILocalizationService
{
    private readonly ISettingsService _settings;
    private readonly string _resourcesPath;

    public LocalizationService(ISettingsService settings)
    {
        _settings = settings;
        // Point this to where your .po files are stored
        _resourcesPath = Path.Combine(AppContext.BaseDirectory, "Localization");
    }

    public async Task<string> TranslateAsync(string key)
    {
        var lang = await _settings.GetLanguageCodeAsync();

        // 1. Set the culture so the localizer knows which file to load
        var culture = new CultureInfo(lang);
        CultureInfo.CurrentUICulture = culture;
        CultureInfo.CurrentCulture = culture;

        // 2. Instantiate your custom localizer
        // Using "*" as baseName to match your wildcard logic in PoFileStringLocalizer
        var localizer = new PoFileStringLocalizer("", _resourcesPath);

        // 3. Get the translation
        var localizedString = localizer[key];

        return localizedString.Value;
    }

    public async Task<string> TranslateAsync(string key, params object[] args)
    {
        var lang = await _settings.GetLanguageCodeAsync();

        var culture = new CultureInfo(lang);
        CultureInfo.CurrentUICulture = culture;

        var localizer = new PoFileStringLocalizer("*", _resourcesPath);

        // Use the built-in formatter logic of your localizer
        return localizer[key, args].Value;
    }
}