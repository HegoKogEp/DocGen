using DocGen.Models;
using DocGen.ViewModels;
using Microsoft.UI.Dispatching;

namespace DocGen.Services
{
    public class LanguageService
    {
        private readonly DocGenViewModel? _viewModel;
        private ResourceService _resourceService = new ResourceService();
        private readonly LoadAppSettingsService _settingsService = new();

        public LanguageService() { }
        public LanguageService(DocGenViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void ChangeLanguage(string languageCode)
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = languageCode;
            var dispatcherQueue = DispatcherQueue.GetForCurrentThread();

            if (dispatcherQueue != null)
            {
                dispatcherQueue.TryEnqueue(() =>
                {
                    UpdateLocalizedStrings();
                });
            }
            else
            {
                UpdateLocalizedStrings();
            }

            var settings = _settingsService.LoadAppSettings();
            settings.Language = languageCode;
            _settingsService.SaveSettings(settings);
        }

        public string GetSystemLanguage()
        {
            return Windows.System.UserProfile.GlobalizationPreferences.Languages[0];
        }

        private void UpdateLocalizedStrings()
        {
            _viewModel.ProjectPathHeader = _resourceService.GetLocalizedResource("ProjectPathTBX");
            _viewModel.DocTitleHeader = _resourceService.GetLocalizedResource("DocTitileHeaderBTN");
            _viewModel.GenerateButtonTitle = _resourceService.GetLocalizedResource("GenerateDocumentationBTN");
            _viewModel.SettingsMenuTitle = _resourceService.GetLocalizedResource("SettingsMenu");
            _viewModel.ExtensionsMenuItemTitle = _resourceService.GetLocalizedResource("ExtentionSettings");
            _viewModel.LanguageMenuItemTitle = _resourceService.GetLocalizedResource("LanguagesSettings");
        }
    }
}
