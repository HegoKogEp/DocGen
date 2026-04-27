using DocGen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DocGen.Services
{
    public class LoadAppSettingsService
    {
        private static string SettingsFilePath
        {
            get
            {
                var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var appFolder = Path.Combine(appData, "DocGen");
                if (!Directory.Exists(appFolder))
                    Directory.CreateDirectory(appFolder);
                return Path.Combine(appFolder, "settings.json");
            }
        }

        public AppSettings LoadAppSettings()
        {
            if (!File.Exists(SettingsFilePath))
            {
                var settings = new AppSettings();
                // Запоминаем язык как null – потом инициализируем из системы
                settings.Language = null;
                return settings;
            }

            try
            {
                var json = File.ReadAllText(SettingsFilePath);
                var settings = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
                return settings;
            }
            catch
            {
                return new AppSettings();
            }
        }

        public void SaveSettings(AppSettings settings)
        {
            // Удаляем SelectedExtensions – он не нужен, так как у нас есть отдельные булевы флаги
            settings.SelectedExtensions = null;
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFilePath, json);
        }
    }
}