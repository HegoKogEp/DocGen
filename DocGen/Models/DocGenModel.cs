using DocGen.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Models
{
    public class DocGenModel
    {
        private readonly ResourceService _resourceService = new();

        private readonly HashSet<string> _excludedFolders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        #region ExcludedFolders
        {
            // Python
            "__pycache__",
            ".venv",
            "venv",
            "env",
            "virtualenv",
            ".pytest_cache",
            ".mypy_cache",
            ".tox",
            "site-packages",
            "dist-packages",
            
            // .NET / C#
            "bin",
            "obj",
            ".vs",
            "packages",
            ".nuget",
            "AppPackages",
            "BundleArtifacts",
            
            // Java / Kotlin / Gradle
            ".gradle",
            "build",
            "target",
            ".idea",
            "gradle",
            "out",
            "libs",
            "classes",
            
            // Node.js / JavaScript / TypeScript
            "node_modules",
            ".npm",
            ".yarn",
            "dist",
            "build-dist",
            ".cache",
            ".parcel-cache",
            ".next",
            ".nuxt",
            ".output",
            
            // Git / Version Control
            ".git",
            ".svn",
            ".hg",
            ".github",
            ".gitlab",
            ".vscode",
            
            // IDE folders
            ".vscode",
            ".idea",
            ".vs",
            ".settings",
            "Debug",
            "Release",
            "x64",
            "x86",
            "AnyCPU",
            
            // General cache and temp folders
            ".cache",
            "cache",
            "temp",
            "tmp",
            ".temp",
            ".tmp",
            "logs",
            ".logs",
            
            // Mobile development
            "build",
            "libs",
            "generated",
            "flutter",
            ".pub-cache",
            
            // Documentation output
            "docs",
            "Documentation",
            "doc",
            "apidoc",
            
            // Test related
            "coverage",
            ".coverage",
            "TestResults",
            "__test__",
            "tests-output",
            
            // Package managers
            "packages",
            ".packages",
            "Library",
            "Frameworks"
        };
        #endregion

        public bool IncludeXaml { get; set; } = true;
        public bool IncludeCs { get; set; } = true;
        public bool IncludeCsproj { get; set; } = true;
        public bool IncludeJson { get; set; } = false;
        public bool IncludeManifest { get; set; } = false;
        public bool IncludeKt { get; set; } = false;
        public bool IncludeGradle { get; set; } = false;
        public bool IncludeKts { get; set; } = false;
        public bool IncludeXml { get; set; } = false;
        public bool IncludeProperties { get; set; } = false;
        public bool IncludeJava { get; set; } = false;
        public bool IncludeJar { get; set; } = false;
        public bool IncludeAar { get; set; } = false;
        public bool IncludePy { get; set; } = false;
        public bool IncludeToml { get; set; } = false;

        public async void GenerateDocumentation(string projectPath, string docTitle, XamlRoot xamlRoot)
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
            if (IncludeKt) allowedExtensions.Add(".kt");
            if (IncludeGradle) allowedExtensions.Add(".gradle");
            if (IncludeKts) allowedExtensions.Add(".kts");
            if (IncludeXml) allowedExtensions.Add(".xml");
            if (IncludeProperties) allowedExtensions.Add(".properties");
            if (IncludeJava) allowedExtensions.Add(".java");
            if (IncludeJar) allowedExtensions.Add(".jar");
            if (IncludeAar) allowedExtensions.Add(".aar");
            if (IncludePy) allowedExtensions.Add(".py");

            // use default extensions if nothing is used
            if (allowedExtensions.Count == 0)
            {
                allowedExtensions.Add(".cs");
                allowedExtensions.Add(".xaml");
                allowedExtensions.Add(".csproj");
            }

            try
            {
                // Collect all files first
                var files = new List<string>();
                foreach (var filePath in Directory.EnumerateFiles(projectPath, "*.*", SearchOption.AllDirectories))
                {
                    if (IsPathInExcludedFolder(filePath, projectPath))
                    {
                        continue;
                    }


                    var extension = Path.GetExtension(filePath);
                    if (allowedExtensions.Contains(extension))
                    {
                        files.Add(filePath);
                    }
                }

                var documentationContent = new StringBuilder();

                // Add document header
                documentationContent.AppendLine($"# {(docTitle != null ? docTitle : Path.GetFileName(projectPath))} - Project Documentation");
                documentationContent.AppendLine();
                documentationContent.AppendLine("## Content");
                documentationContent.AppendLine();

                // Group files by directory for hierarchical structure
                var filesByDirectory = new Dictionary<string, List<string>>();
                foreach (var filePath in files)
                {
                    var relativePath = GetRelativePath(projectPath, filePath);
                    var directory = Path.GetDirectoryName(relativePath) ?? "";
                    if (!filesByDirectory.ContainsKey(directory))
                    {
                        filesByDirectory[directory] = new List<string>();
                    }
                    filesByDirectory[directory].Add(filePath);
                }

                // Generate hierarchical table of contents
                int mainIndex = 1;
                var tocEntries = new List<string>();

                // Root level
                var rootFiles = filesByDirectory.ContainsKey("") ? filesByDirectory[""] : new List<string>();
                if (rootFiles.Any())
                {
                    tocEntries.Add($"1. [{Path.GetFileName(projectPath)}](#{GenerateAnchorId(Path.GetFileName(projectPath))})");
                    int subIndex = 1;
                    foreach (var filePath in rootFiles)
                    {
                        var relativePath = GetRelativePath(projectPath, filePath);
                        var fileName = Path.GetFileName(filePath);
                        var anchorId = GenerateAnchorId(fileName);
                        tocEntries.Add($"   {subIndex}. [{fileName}](#{anchorId})");
                        subIndex++;
                    }
                    mainIndex = 2;
                }

                // Subdirectories
                foreach (var kvp in filesByDirectory.Where(x => !string.IsNullOrEmpty(x.Key)).OrderBy(x => x.Key))
                {
                    var directory = kvp.Key;
                    var dirName = Path.GetFileName(directory);
                    tocEntries.Add($"{mainIndex}. [{directory}](#{GenerateAnchorId(directory)})");

                    int subIndex = 1;
                    foreach (var filePath in kvp.Value.OrderBy(f => Path.GetFileName(f)))
                    {
                        var relativePath = GetRelativePath(projectPath, filePath);
                        var fileName = Path.GetFileName(filePath);
                        var anchorId = GenerateAnchorId(fileName);
                        tocEntries.Add($"   {subIndex}. [{fileName}](#{anchorId})");
                        subIndex++;
                    }
                    mainIndex++;
                }

                foreach (var entry in tocEntries)
                {
                    documentationContent.AppendLine(entry);
                }

                documentationContent.AppendLine();
                documentationContent.AppendLine("## FILE 1: Project Root");
                documentationContent.AppendLine();

                // Generate documentation for each file with proper anchors
                int fileCounter = 1;

                // Root level files first
                if (rootFiles.Any())
                {
                    documentationContent.AppendLine($"<a id='{GenerateAnchorId(Path.GetFileName(projectPath))}'></a>");
                    documentationContent.AppendLine();

                    foreach (var filePath in rootFiles)
                    {
                        fileCounter = await AppendFileDocumentation(documentationContent, filePath, projectPath, fileCounter);
                    }
                }

                // Then files in subdirectories
                foreach (var kvp in filesByDirectory.Where(x => !string.IsNullOrEmpty(x.Key)).OrderBy(x => x.Key))
                {
                    var directory = kvp.Key;
                    documentationContent.AppendLine($"## {directory}");
                    documentationContent.AppendLine();
                    documentationContent.AppendLine($"<a id='{GenerateAnchorId(directory)}'></a>");
                    documentationContent.AppendLine();

                    foreach (var filePath in kvp.Value.OrderBy(f => Path.GetFileName(f)))
                    {
                        fileCounter = await AppendFileDocumentation(documentationContent, filePath, projectPath, fileCounter);
                    }
                }

                string outputFilePath = Path.Combine(projectPath, "README.md");
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

        private async Task<int> AppendFileDocumentation(StringBuilder documentationContent, string filePath, string projectPath, int fileCounter)
        {
            var relativePath = GetRelativePath(projectPath, filePath);
            var fileName = Path.GetFileName(filePath);
            var extension = Path.GetExtension(filePath);
            var language = GetLanguageForExtension(extension);
            var anchorId = GenerateAnchorId(fileName);

            documentationContent.AppendLine($"## FILE {fileCounter}: {fileName}");
            documentationContent.AppendLine();
            documentationContent.AppendLine($"<a id='{anchorId}'></a>");
            documentationContent.AppendLine();

            string fileContent = await File.ReadAllTextAsync(filePath);

            // Add code block with appropriate language formatting
            documentationContent.AppendLine($"```{language}");
            documentationContent.AppendLine(fileContent);
            documentationContent.AppendLine("```");
            documentationContent.AppendLine();
            documentationContent.AppendLine("---");
            documentationContent.AppendLine();

            return fileCounter + 1;
        }

        private string GetRelativePath(string basePath, string fullPath)
        {
            if (!basePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                basePath += Path.DirectorySeparatorChar;

            var relativePath = Path.GetRelativePath(basePath, fullPath);
            return relativePath.Replace('\\', '/');
        }

        private string GenerateAnchorId(string fileName)
        {
            // Generate anchor ID like in example: "fileitemcs" (without extension, all lowercase)
            var withoutExtension = Path.GetFileNameWithoutExtension(fileName);
            // Remove dots and special characters, keep only letters and numbers
            var anchorId = new string(withoutExtension.Where(c => char.IsLetterOrDigit(c)).ToArray());
            return anchorId.ToLower();
        }

        private bool IsPathInExcludedFolder(string filePath, string projectPath)
        {
            var relativePath = GetRelativePath(projectPath, filePath);
            var pathParts = relativePath.Split('/', '\\');

            // Check each part of the path against excluded folders
            for (int i = 0; i < pathParts.Length - 1; i++) // Exclude the filename itself
            {
                if (_excludedFolders.Contains(pathParts[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private string GetLanguageForExtension(string extension)
        {
            return extension.ToLower() switch
            {
                ".xaml" => "xml",
                ".xml" => "xml",
                ".cs" => "csharp",
                ".csproj" => "xml",
                ".json" => "json",
                ".manifest" => "xml",
                ".kt" => "kotlin",
                ".gradle" => "gradle",
                ".kts" => "kotlin",
                ".properties" => "properties",
                ".java" => "java",
                ".jar" => "",
                ".aar" => "",
                ".py" => "python",
                _ => ""
            };
        }
    }
}
