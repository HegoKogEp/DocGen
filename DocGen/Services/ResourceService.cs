using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DocGen.Services
{
    public static class ResourceService
    {
        private static Dictionary<string, string> _strings = new();
        private static string _currentLanguage = "en-US";

        public static void Initialize(string? languageCode = null)
        {
            // Определяем язык: если передан, берём его, иначе системный язык
            var langToUse = languageCode;
            if (string.IsNullOrEmpty(langToUse))
            {
                langToUse = Windows.System.UserProfile.GlobalizationPreferences.Languages[0];
                // Нормализуем: возможен "ru", превращаем в "ru-RU"
                if (langToUse.StartsWith("ru")) langToUse = "ru-RU";
                else if (langToUse.StartsWith("en")) langToUse = "en-US";
                else langToUse = "en-US";
            }

            _currentLanguage = langToUse;
            LoadResources(langToUse);

            // НЕ вызываем ApplicationLanguages.PrimaryLanguageOverride в unpackaged!
            // Эта строка ЗАКОММЕНТИРОВАНА:
            // ApplicationLanguages.PrimaryLanguageOverride = langToUse;
        }

        private static void LoadResources(string languageCode)
        {
            var fileName = $"Strings.{languageCode}.json";
            var resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", fileName);

            if (!File.Exists(resourcesPath))
            {
                // Fallback на английский
                resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Strings.en-US.json");
            }

            try
            {
                var json = File.ReadAllText(resourcesPath);
                _strings = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new();
            }
            catch
            {
                _strings = new();
            }
        }

        public static string GetLocalizedResource(string resourceKey)
        {
            return _strings.TryGetValue(resourceKey, out var value) ? value : resourceKey;
        }

        public static void ChangeLanguage(string languageCode)
        {
            if (_currentLanguage == languageCode) return;
            Initialize(languageCode);
        }
    }
}