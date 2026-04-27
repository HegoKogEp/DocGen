using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocGen.Models;
using DocGen.Services;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DocGen.ViewModels
{
    public partial class DocGenViewModel : ObservableObject
    {
        private readonly LanguageService _languageService;
        private readonly IWindowService _windowService;
        private readonly LoadAppSettingsService _settingsService;
        private DocGenModel? _docGenModel;

        #region Propertiees for interface fields
        [ObservableProperty]
        private string? projectPath;
        [ObservableProperty]
        private string? docTitle;
        [ObservableProperty]
        private string? projectPathHeader;
        [ObservableProperty]
        private string? docTitleHeader;
        [ObservableProperty]
        private string? settingsMenuTitle;
        [ObservableProperty]
        private string? extensionsMenuItemTitle;
        [ObservableProperty]
        private string? languageMenuItemTitle;
        [ObservableProperty]
        private string? generateButtonTitle;
        #endregion

        #region Properties for extensions
        [ObservableProperty]
        private bool includeXaml;
        [ObservableProperty]
        private bool includeCs;
        [ObservableProperty]
        private bool includeCsproj;
        [ObservableProperty]
        private bool includeJson;
        [ObservableProperty]
        private bool includeManifest;
        [ObservableProperty]
        private bool includeKt;
        [ObservableProperty]
        private bool includeGradle;
        [ObservableProperty]
        private bool includeKts;
        [ObservableProperty]
        private bool includeXml;
        [ObservableProperty]
        private bool includeProperties;
        [ObservableProperty]
        private bool includeJava;
        [ObservableProperty]
        private bool includeJar;
        [ObservableProperty]
        private bool includeAar;
        [ObservableProperty]
        private bool includePy;
        [ObservableProperty]
        private bool includeToml;
        #endregion

        // constructor for DocGenViewModel with autoload settings from settings file
        #region Constructor
        public DocGenViewModel(IWindowService windowService)
        {
            _windowService = windowService;
            _settingsService = new LoadAppSettingsService();

            // 1. Загружаем настройки
            var settings = _settingsService.LoadAppSettings();

            // 2. Инициализируем ResourceService (устанавливаем язык)
            string langToUse = settings.Language;
            if (string.IsNullOrEmpty(langToUse))
            {
                langToUse = Windows.System.UserProfile.GlobalizationPreferences.Languages[0];
                // нормализуем
                if (langToUse.StartsWith("ru")) langToUse = "ru-RU";
                else if (langToUse.StartsWith("en")) langToUse = "en-US";
                else langToUse = "en-US";
            }
            ResourceService.Initialize(langToUse);

            // 3. Теперь создаём LanguageService (он использует ResourceService)
            _languageService = new LanguageService(this);

            // 4. Применяем настройки расширений
            IncludeXaml = settings.IncludeXaml;
            IncludeCs = settings.IncludeCs;
            IncludeCsproj = settings.IncludeCsproj;
            IncludeJson = settings.IncludeJson;
            IncludeManifest = settings.IncludeManifest;
            IncludeKt = settings.IncludeKt;
            IncludeToml = settings.IncludeToml;
            IncludePy = settings.IncludePy;
            IncludeGradle = settings.IncludeGradle;
            IncludeKts = settings.IncludeKts;
            IncludeJava = settings.IncludeJava;
            IncludeJar = settings.IncludeJar;
            IncludeProperties = settings.IncludeProperties;
            IncludeXml = settings.IncludeXml;
            IncludeAar = settings.IncludeAar;

            // 5. Обновляем UI (чтобы заголовки отобразились)
            _languageService.ChangeLanguage(langToUse); // вызовет UpdateLocalizedStrings
        }
        #endregion

        // method for saving settings
        private void SaveExtensionSettings()
        {
            var settings = _settingsService.LoadAppSettings();
            settings.IncludeXaml = IncludeXaml;
            settings.IncludeCs = IncludeCs;
            settings.IncludeCsproj = IncludeCsproj;
            settings.IncludeJson = IncludeJson;
            settings.IncludeManifest = IncludeManifest;
            settings.IncludeKt = IncludeKt;
            settings.IncludeToml = IncludeToml;
            settings.IncludePy = IncludePy;
            settings.IncludeGradle = IncludeGradle;
            settings.IncludeKts = IncludeKts;
            settings.IncludeJava = IncludeJava;
            settings.IncludeJar = IncludeJar;
            settings.IncludeProperties = IncludeProperties;
            settings.IncludeXml = IncludeXml;
            settings.IncludeAar = IncludeAar;
            _settingsService.SaveSettings(settings);
        }

        // update properties for autosave on changed
        partial void OnIncludeXamlChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeCsChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeCsprojChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeJsonChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeManifestChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeKtChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeGradleChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeKtsChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeXmlChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludePropertiesChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeJavaChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeJarChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeAarChanged (bool value) => SaveExtensionSettings();
        partial void OnIncludePyChanged (bool value) => SaveExtensionSettings();
        partial void OnIncludeTomlChanged (bool value) => SaveExtensionSettings();

        [RelayCommand]
        public void SwitchLanguage(string languageCode)
        {
            switch (languageCode)
            {
                case "ru-RU": _languageService.ChangeLanguage(languageCode); break;
                case "en-US": _languageService.ChangeLanguage(languageCode); break;
            }
        }

        [RelayCommand]
        private async Task GenerateDocumentation()
        {
            _docGenModel ??= new DocGenModel();

            // passing extendions to DocGenModel
            _docGenModel.IncludeXaml = IncludeXaml;
            _docGenModel.IncludeCs = IncludeCs;
            _docGenModel.IncludeCsproj = IncludeCsproj;
            _docGenModel.IncludeJson = IncludeJson;
            _docGenModel.IncludeManifest = IncludeManifest;
            _docGenModel.IncludeKt = IncludeKt;
            _docGenModel.IncludeGradle = IncludeGradle;
            _docGenModel.IncludeXml = IncludeXml;
            _docGenModel.IncludeProperties = IncludeProperties;
            _docGenModel.IncludeJava = IncludeJava;
            _docGenModel.IncludeJar = IncludeJar;
            _docGenModel.IncludePy = IncludePy;
            _docGenModel.IncludeToml = IncludeToml;

            _docGenModel.GenerateDocumentation(ProjectPath, DocTitle, _windowService.GetXamlRoot());
        }

        [RelayCommand]
        private async Task Browse()
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            var hwnd = _windowService.GetWindowHandle();
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");

            var folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                ProjectPath = folder.Path;
            }
        }
    }
}
