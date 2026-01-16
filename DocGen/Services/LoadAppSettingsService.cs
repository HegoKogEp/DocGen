using DocGen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocGen.Services
{
    public class LoadAppSettingsService
    {
        private static string SettingsFilePath => Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "settings.json");

        public AppSettings LoadAppSettings()
        {
            if (!File.Exists(SettingsFilePath))
            {
                var settings = new AppSettings();
                var systemLanguage = Windows.System.UserProfile.GlobalizationPreferences.Languages[0];
                settings.Language = systemLanguage;
                return settings;
            }

            try
            {
                var json = File.ReadAllText(SettingsFilePath);
                var settings = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();

                
                if (settings.SelectedExtensions == null || settings.SelectedExtensions.Count == 0)
                {
                    settings.SelectedExtensions = new List<string>();
                    if (settings.IncludeXaml) settings.SelectedExtensions.Add(".xaml");
                    if (settings.IncludeCs) settings.SelectedExtensions.Add(".cs");
                    if (settings.IncludeCsproj) settings.SelectedExtensions.Add(".csproj");
                    if (settings.IncludeJson) settings.SelectedExtensions.Add(".json");
                    if (settings.IncludeManifest) settings.SelectedExtensions.Add(".manifest");
                }

                return settings;
            }
            catch
            {
                return new AppSettings();
            }
        }

        public void SaveSettings(AppSettings settings)
        {
            
            settings.SelectedExtensions = new List<string>();
            if (settings.IncludeXaml) settings.SelectedExtensions.Add(".xaml");
            if (settings.IncludeCs) settings.SelectedExtensions.Add(".cs");
            if (settings.IncludeCsproj) settings.SelectedExtensions.Add(".csproj");
            if (settings.IncludeJson) settings.SelectedExtensions.Add(".json");
            if (settings.IncludeManifest) settings.SelectedExtensions.Add(".manifest");

            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFilePath, json);
        }
    }
}
