using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TelegramBot.Infrastructure.Services
{
    public class PoFileStringLocalizer : IStringLocalizer
    {
        private readonly string _resourcesPath;
        private readonly string _baseName;
        private static readonly ConcurrentDictionary<string, Dictionary<string, string>> _cache = new();

        public PoFileStringLocalizer(string baseName, string resourcesPath)
        {
            _resourcesPath = resourcesPath;
            _baseName = baseName;
        }

        private Dictionary<string, string> LoadPoFile(string culture)
        {
            var translations = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // 1. Construct the path to the subfolder (e.g., Localization/fa)
            var culturePath = Path.Combine(_resourcesPath, culture);

            if (!Directory.Exists(culturePath))
            {
                // Try parent culture (e.g., if 'fa-IR' fails, try 'fa')
                var parentCulture = CultureInfo.GetCultureInfo(culture).Parent.Name;
                if (!string.IsNullOrEmpty(parentCulture) && parentCulture != culture)
                {
                    culturePath = Path.Combine(_resourcesPath, parentCulture);
                }
            }

            if (Directory.Exists(culturePath))
            {
                // 2. Get ALL .po files inside that folder
                var files = Directory.GetFiles(culturePath, "*.po");

                foreach (var file in files)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file); // e.g., "messages"
                    var localTranslations = ParsePoFile(file);

                    foreach (var kvp in localTranslations)
                    {
                        // If using wildcard "*", prefix with filename: "messages.MyKey"
                        // Otherwise, just use the key: "MyKey"
                        var key = (_baseName == "*") ? $"{fileName}.{kvp.Key}" : kvp.Key;
                        translations[key] = kvp.Value;
                    }
                }
            }

            return translations;
        }
        private Dictionary<string, string> ParsePoFile(string path)
        {
            var translations = new Dictionary<string, string>();
            string[] lines = File.ReadAllLines(path);
            string msgid = null;
            string msgstr = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("msgid "))
                {
                    msgid = ExtractString(line);
                }
                else if (line.StartsWith("msgstr "))
                {
                    msgstr = ExtractString(line);

                    if (msgid != null && msgstr != null)
                    {
                        translations[msgid] = msgstr;
                        msgid = null;
                        msgstr = null;
                    }
                }
            }

            return translations;
        }

        private string ExtractString(string poLine)
        {
            var match = Regex.Match(poLine, "^msg(?:id|str)\\s+\"(.*)\"$");
            if (match.Success)
            {
                // Unescape the literal \n into a real newline character
                return match.Groups[1].Value.Replace("\\n", "\n");
            }
            return null;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var culture = CultureInfo.CurrentUICulture.Name;
                var dict = _cache.GetOrAdd(culture, _ => LoadPoFile(culture));
                var value = dict.TryGetValue(name, out var result) ? result : name;
                return new LocalizedString(name, value, resourceNotFound: value == name);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                // Replace null or empty arguments with translated "unknown"
                var processedArgs = arguments.Select(arg =>
                {
                    if (arg == null || (arg is string str && string.IsNullOrWhiteSpace(str)))
                    {
                        return this["unknown"].Value;
                    }
                    return arg;
                }).ToArray();

                return new LocalizedString(name, string.Format(this[name].Value, processedArgs), this[name].ResourceNotFound);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var dict = _cache.GetOrAdd(culture, _ => LoadPoFile(culture));
            return dict.Select(kvp => new LocalizedString(kvp.Key, kvp.Value, false));
        }
    }

}
