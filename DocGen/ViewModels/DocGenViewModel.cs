using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocGen.Models;
using DocGen.Services;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DocGen.ViewModels
{
    public partial class DocGenViewModel : ObservableObject
    {
        private readonly LanguageService _languageService;
        private readonly IWindowService _windowService;
        private readonly DocGenModel _docGenModel = new();
        private readonly LoadAppSettingsService _settingsService;

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
        #endregion

        // constructor for DocGenViewModel with autoload settings from settings file
        #region Constructor
        public DocGenViewModel(IWindowService windowService)
        {
            _languageService = new LanguageService(this);
            _windowService = windowService;
            _settingsService = new LoadAppSettingsService();

            // load settings
            var settings = _settingsService.LoadAppSettings();

            // apply extension settings
            IncludeXaml = settings.IncludeXaml;
            IncludeCs = settings.IncludeCs;
            IncludeCsproj = settings.IncludeCsproj;
            IncludeJson = settings.IncludeJson;
            IncludeManifest = settings.IncludeManifest;

            // apply language settings
            _languageService.ChangeLanguage(settings.Language);
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
            _settingsService.SaveSettings(settings);
        }

        // update properties for autosave on changed
        partial void OnIncludeXamlChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeCsChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeCsprojChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeJsonChanged(bool value) => SaveExtensionSettings();
        partial void OnIncludeManifestChanged(bool value) => SaveExtensionSettings();

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
            // passing extendions to DocGenModel
            _docGenModel.IncludeXaml = IncludeXaml;
            _docGenModel.IncludeCs = IncludeCs;
            _docGenModel.IncludeCsproj = IncludeCsproj;
            _docGenModel.IncludeJson = IncludeJson;
            _docGenModel.IncludeManifest = IncludeManifest;

            _docGenModel.GenerateDocumentation(ProjectPath, _windowService.GetXamlRoot());
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
