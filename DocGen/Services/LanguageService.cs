using DocGen.Models;
using DocGen.ViewModels;
using Microsoft.UI.Dispatching;

namespace DocGen.Services
{
    public class LanguageService
    {
        private readonly DocGenViewModel _viewModel;
        private readonly LoadAppSettingsService _settingsService = new();

        public LanguageService(DocGenViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void ChangeLanguage(string languageCode)
        {
            // Меняем язык в ResourceService
            ResourceService.ChangeLanguage(languageCode);

            // Сохраняем в настройках
            var settings = _settingsService.LoadAppSettings();
            settings.Language = languageCode;
            _settingsService.SaveSettings(settings);

            // Обновляем UI
            UpdateLocalizedStrings();
        }

        private void UpdateLocalizedStrings()
        {
            // Получаем актуальные строки из ResourceService
            _viewModel.ProjectPathHeader = ResourceService.GetLocalizedResource("ProjectPathTBX");
            _viewModel.DocTitleHeader = ResourceService.GetLocalizedResource("DocTitileHeaderBTN");
            _viewModel.GenerateButtonTitle = ResourceService.GetLocalizedResource("GenerateDocumentationBTN");
            _viewModel.SettingsMenuTitle = ResourceService.GetLocalizedResource("SettingsMenu");
            _viewModel.ExtensionsMenuItemTitle = ResourceService.GetLocalizedResource("ExtentionSettings");
            _viewModel.LanguageMenuItemTitle = ResourceService.GetLocalizedResource("LanguagesSettings");
        }
    }
}