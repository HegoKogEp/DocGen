using DocGen.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Models
{
    public class DocGenModel
    {
        private readonly ResourceService _resourceService = new();

        public bool IncludeXaml { get; set; } = true;
        public bool IncludeCs { get; set; } = true;
        public bool IncludeCsproj { get; set; } = true;
        public bool IncludeJson { get; set; } = false;
        public bool IncludeManifest { get; set; } = false;

        public async void GenerateDocumentation(string projectPath, XamlRoot xamlRoot)
        {
            if (string.IsNullOrEmpty(projectPath) || !Directory.Exists(projectPath))
            {
                var dialog = new ContentDialog
                {
                    Title = _resourceService.GetLocalizedResource("Error"),
                    Content = _resourceService.GetLocalizedResource("PathErrorMesseage"),
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot
                };
                await dialog.ShowAsync();

                return;
            }

            var allowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // load extension settings
            if (IncludeXaml) allowedExtensions.Add(".xaml");
            if (IncludeCs) allowedExtensions.Add(".cs");
            if (IncludeCsproj) allowedExtensions.Add(".csproj");
            if (IncludeJson) allowedExtensions.Add(".json");
            if (IncludeManifest) allowedExtensions.Add(".manifest");

            // use default extensions if nothing is used
            if (allowedExtensions.Count == 0)
            {
                allowedExtensions.Add(".cs");
                allowedExtensions.Add(".xaml");
                allowedExtensions.Add(".csproj");
            }

            var documentationContent = new StringBuilder();

            try
            {
                foreach (var filePath in Directory.EnumerateFiles(projectPath, "*.*", SearchOption.AllDirectories))
                {
                    var extension = Path.GetExtension(filePath);
                    if (allowedExtensions.Contains(extension))
                    {
                        documentationContent.AppendLine($"--- File: {filePath} ---");
                        documentationContent.AppendLine();

                        string fileContent = await File.ReadAllTextAsync(filePath);

                        documentationContent.AppendLine(fileContent);
                        documentationContent.AppendLine();
                    }
                }

                string outputFilePath = Path.Combine(projectPath, "readme.md");
                await File.WriteAllTextAsync(outputFilePath, documentationContent.ToString());

                var dialog = new ContentDialog
                {
                    Title = _resourceService.GetLocalizedResource("Success"),
                    Content = $"{_resourceService.GetLocalizedResource("SuccessMesseage")}:\n {outputFilePath}",
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot
                };
                await dialog.ShowAsync();
            }
            catch (Exception ex)
            {
                var dialog = new ContentDialog
                {
                    Title = _resourceService.GetLocalizedResource("Error"),
                    Content = $"{_resourceService.GetLocalizedResource("Error")}: {ex.Message}",
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot
                };
                await dialog.ShowAsync();
            }
        }
    }
}
