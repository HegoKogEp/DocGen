# DocGen - Документация по проекту

## Содержание
1. [Docgen](#docgen)
   1. [App.xaml](#appxaml)
   2. [App.xaml.cs](#appxamlcs)
   3. [DocGen.csproj](#docgencsproj)
   4. [MainWindow.xaml](#mainwindowxaml)
   5. [MainWindow.xaml.cs](#mainwindowxamlcs)
2. [Docgen/Models](#docgen-models)
   1. [AppSettings.cs](#appsettingscs)
   2. [DocGenModel.cs](#docgenmodelcs)
3. [Docgen/Services](#docgen-services)
   1. [IWindowService.cs](#iwindowservicecs)
   2. [LanguageService.cs](#languageservicecs)
   3. [LoadAppSettingsService.cs](#loadappsettingsservicecs)
   4. [ResourceService.cs](#resourceservicecs)
4. [Docgen/Viewmodels](#docgen-viewmodels)
   1. [DocGenViewModel.cs](#docgenviewmodelcs)

## FILE 1: App.xaml

<a id='appxaml'></a>

```xml
<?xml version="1.0" encoding="utf-8"?>
<Application
    x:Class="DocGen.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:DocGen">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls" />
                <!-- Other merged dictionaries here -->
            </ResourceDictionary.MergedDictionaries>
            <!-- Other app resources here -->
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

---

## FILE 2: App.xaml.cs

<a id='appxamlcs'></a>

```csharp
using Microsoft.UI.Xaml;
using Microsoft.Windows.Globalization;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DocGen
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window? _window;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            var userLanguage = Windows.System.UserProfile.GlobalizationPreferences.Languages[0];
            ApplicationLanguages.PrimaryLanguageOverride = userLanguage;
        }

        /// <summary>   
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();
            _window.Activate();
        }
    }
}
```

---

## FILE 3: DocGen.csproj

<a id='docgencsproj'></a>

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
    <TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>
    <RootNamespace>DocGen</RootNamespace>
    <ApplicationManifest>app.manifest</ApplicationManifest>
    <Platforms>x86;x64;ARM64</Platforms>
    <RuntimeIdentifiers>win-x86;win-x64;win-arm64</RuntimeIdentifiers>
    <PublishProfile>win-$(Platform).pubxml</PublishProfile>
    <UseWinUI>true</UseWinUI>
    <WinUISDKReferences>false</WinUISDKReferences>
    <EnableMsixTooling>true</EnableMsixTooling>
    <Nullable>enable</Nullable>
	<WindowsPackageType>None</WindowsPackageType>
	<WindowsAppSDKSelfContained>true</WindowsAppSDKSelfContained>
  </PropertyGroup>

  <ItemGroup>
    <Content Include="Assets\SplashScreen.scale-200.png" />
    <Content Include="Assets\LockScreenLogo.scale-200.png" />
    <Content Include="Assets\Square150x150Logo.scale-200.png" />
    <Content Include="Assets\Square44x44Logo.scale-200.png" />
    <Content Include="Assets\Square44x44Logo.targetsize-24_altform-unplated.png" />
    <Content Include="Assets\StoreLogo.png" />
    <Content Include="Assets\Wide310x150Logo.scale-200.png" />
  </ItemGroup>

  <ItemGroup>
    <Manifest Include="$(ApplicationManifest)" />
  </ItemGroup>

  <!--
    Defining the "Msix" ProjectCapability here allows the Single-project MSIX Packaging
    Tools extension to be activated for this project even if the Windows App SDK Nuget
    package has not yet been restored.
  -->
  <ItemGroup Condition="'$(DisableMsixProjectCapabilityAddedByProject)'!='true' and '$(EnableMsixTooling)'=='true'">
    <ProjectCapability Include="Msix" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="CommunityToolkit.Mvvm" Version="8.4.0" />
    <PackageReference Include="Microsoft.Windows.SDK.BuildTools" Version="10.0.26100.7175" />
    <PackageReference Include="Microsoft.WindowsAppSDK" Version="1.8.251106002" />
  </ItemGroup>

  <!--
    Defining the "HasPackageAndPublishMenuAddedByProject" property here allows the Solution
    Explorer "Package and Publish" context menu entry to be enabled for this project even if
    the Windows App SDK Nuget package has not yet been restored.
  -->
  <PropertyGroup Condition="'$(DisableHasPackageAndPublishMenuAddedByProject)'!='true' and '$(EnableMsixTooling)'=='true'">
    <HasPackageAndPublishMenu>true</HasPackageAndPublishMenu>
  </PropertyGroup>

  <!-- Publish Properties -->
  <PropertyGroup>
    <PublishReadyToRun Condition="'$(Configuration)' == 'Debug'">False</PublishReadyToRun>
    <PublishReadyToRun Condition="'$(Configuration)' != 'Debug'">True</PublishReadyToRun>
    <PublishTrimmed Condition="'$(Configuration)' == 'Debug'">False</PublishTrimmed>
    <PublishTrimmed Condition="'$(Configuration)' != 'Debug'">True</PublishTrimmed>
    <GenerateAppInstallerFile>False</GenerateAppInstallerFile>
    <AppxPackageSigningEnabled>False</AppxPackageSigningEnabled>
    <AppxPackageSigningTimestampDigestAlgorithm>SHA256</AppxPackageSigningTimestampDigestAlgorithm>
    <AppxAutoIncrementPackageRevision>True</AppxAutoIncrementPackageRevision>
    <GenerateTestArtifacts>True</GenerateTestArtifacts>
    <AppxBundle>Never</AppxBundle>
    <HoursBetweenUpdateChecks>0</HoursBetweenUpdateChecks>
  </PropertyGroup>
</Project>
```

---

## FILE 4: MainWindow.xaml

<a id='mainwindowxaml'></a>

```xml
<?xml version="1.0" encoding="utf-8"?>
<Window
    x:Class="DocGen.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:DocGen"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    Title="DocGen">

    <Window.SystemBackdrop>
        <MicaBackdrop />
    </Window.SystemBackdrop>

    <Grid DataContext="{x:Bind ViewModel}">
        <Grid.RowDefinitions>
            <RowDefinition Height="40"/>
            <RowDefinition Height="80"/>
            <RowDefinition Height="80"/>
            <RowDefinition Height="80"/>
        </Grid.RowDefinitions>

        <MenuBar Grid.Row="0">
            <MenuBarItem Title="{Binding SettingsMenuTitle}">
                <MenuFlyoutSubItem Text="{Binding ExtensionsMenuItemTitle}">
                    <ToggleMenuFlyoutItem Text=".py" IsChecked="{x:Bind ViewModel.IncludePy, Mode=TwoWay}" />
                    <ToggleMenuFlyoutItem Text=".xaml" IsChecked="{x:Bind ViewModel.IncludeXaml, Mode=TwoWay}" />
                    <ToggleMenuFlyoutItem Text=".cs" IsChecked="{x:Bind ViewModel.IncludeCs, Mode=TwoWay}" />
                    <ToggleMenuFlyoutItem Text=".csproj" IsChecked="{x:Bind ViewModel.IncludeCsproj, Mode=TwoWay}" />
                    <ToggleMenuFlyoutItem Text=".json" IsChecked="{x:Bind ViewModel.IncludeJson, Mode=TwoWay}" />
                    <ToggleMenuFlyoutItem Text=".manifest" IsChecked="{x:Bind ViewModel.IncludeManifest, Mode=TwoWay}" />
                    <ToggleMenuFlyoutItem Text=".kt" IsChecked="{x:Bind ViewModel.IncludeKt, Mode=TwoWay}" />
                    <ToggleMenuFlyoutItem Text=".gradle" IsChecked="{x:Bind ViewModel.IncludeGradle, Mode=TwoWay}" />
                    <ToggleMenuFlyoutItem Text=".kts" IsChecked="{x:Bind ViewModel.IncludeKts, Mode=TwoWay}" />
                    <ToggleMenuFlyoutItem Text=".xml" IsChecked="{x:Bind ViewModel.IncludeXml, Mode=TwoWay}" />
                    <ToggleMenuFlyoutItem Text=".properties" IsChecked="{x:Bind ViewModel.IncludeProperties, Mode=TwoWay}" />
                    <ToggleMenuFlyoutItem Text=".java" IsChecked="{x:Bind ViewModel.IncludeJava, Mode=TwoWay}" />
                    <ToggleMenuFlyoutItem Text=".jar" IsChecked="{x:Bind ViewModel.IncludeJar, Mode=TwoWay}" />
                    <ToggleMenuFlyoutItem Text=".aar" IsChecked="{x:Bind ViewModel.IncludeAar, Mode=TwoWay}" />
                    <ToggleMenuFlyoutItem Text=".toml" IsChecked="{x:Bind ViewModel.IncludeToml, Mode=TwoWay}" />
                </MenuFlyoutSubItem>
                <MenuFlyoutSubItem Text="{Binding LanguageMenuItemTitle}">
                    <MenuFlyoutSubItem.Items>
                        <MenuFlyoutItem Text="Russian" Command="{Binding SwitchLanguageCommand}" CommandParameter="ru-RU"/>
                        <MenuFlyoutItem Text="English" Command="{Binding SwitchLanguageCommand}" CommandParameter="en-US"/>
                    </MenuFlyoutSubItem.Items>
                </MenuFlyoutSubItem>
            </MenuBarItem>
        </MenuBar>

        <TextBox Grid.Row="1"
                 Height="60"
                 Margin="8"
                 Text="{Binding ProjectPath, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
                 Header="{Binding ProjectPathHeader}"/>

        <Button HorizontalAlignment="Right"
                Margin="0 40 10 16"
                Grid.Row="1"
                Command="{Binding BrowseCommand}"
                Content="..." FontSize="10"/>

        <TextBox Grid.Row="2"
                 Height="60"
                 Margin="8"
                 Text="{Binding DocTitle, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
                 Header="{Binding DocTitleHeader}"/>

        <Button x:Name="GenerateButton"
                Grid.Row="3"
                Margin="8"
                Command="{Binding GenerateDocumentationCommand}"
                HorizontalAlignment="Right"
                Content="{Binding GenerateButtonTitle}"/>
    </Grid>
</Window>
```

---

## FILE 5: MainWindow.xaml.cs

<a id='mainwindowxamlcs'></a>

```csharp
using DocGen.Services;
using DocGen.ViewModels;
using Microsoft.UI.Xaml;
using System;

namespace DocGen
{
    public sealed partial class MainWindow : Window, IWindowService
    {
        public DocGenViewModel ViewModel;
        public XamlRoot GetXamlRoot() => Content.XamlRoot;
        public IntPtr GetWindowHandle() => WinRT.Interop.WindowNative.GetWindowHandle(this);

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new DocGenViewModel(this);

            var appWindow = this.AppWindow;
            appWindow.Resize(new Windows.Graphics.SizeInt32(450, 300));

        }

    }
}
```

---

## FILE 6: AppSettings.cs

<a id='appsettingscs'></a>

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Models
{
    public class AppSettings
    {
        public string Language { get; set; } = null;
        public List<string> SelectedExtensions { get; set; } = new() { ".cs", ".xaml", ".csproj" };

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
    }
}
```

---

## FILE 7: DocGenModel.cs

<a id='docgenmodelcs'></a>

```csharp
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
```

---

## FILE 8: IWindowService.cs

<a id='iwindowservicecs'></a>

```csharp
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Services
{
    public interface IWindowService
    {
        XamlRoot GetXamlRoot();
        IntPtr GetWindowHandle();
    }
}
```

---

## FILE 9: LanguageService.cs

<a id='languageservicecs'></a>

```csharp
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
```

---

## FILE 10: LoadAppSettingsService.cs

<a id='loadappsettingsservicecs'></a>

```csharp
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
                    if (settings.IncludeKt) settings.SelectedExtensions.Add(".kt");
                    if (settings.IncludeGradle) settings.SelectedExtensions.Add(".gradle");
                    if (settings.IncludeKts) settings.SelectedExtensions.Add(".kts");
                    if (settings.IncludeXml) settings.SelectedExtensions.Add(".xml");
                    if (settings.IncludeProperties) settings.SelectedExtensions.Add(".properties");
                    if (settings.IncludeJava) settings.SelectedExtensions.Add(".java");
                    if (settings.IncludeJar) settings.SelectedExtensions.Add(".jar");
                    if (settings.IncludeAar) settings.SelectedExtensions.Add(".aar");
                    if (settings.IncludePy) settings.SelectedExtensions.Add(".py");
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
            if (settings.IncludeKt) settings.SelectedExtensions.Add(".kt");
            if (settings.IncludeGradle) settings.SelectedExtensions.Add(".gradle");
            if (settings.IncludeKts) settings.SelectedExtensions.Add(".kts");
            if (settings.IncludeXml) settings.SelectedExtensions.Add(".xml");
            if (settings.IncludeProperties) settings.SelectedExtensions.Add(".properties");
            if (settings.IncludeJava) settings.SelectedExtensions.Add(".java");
            if (settings.IncludeJar) settings.SelectedExtensions.Add(".jar");
            if (settings.IncludeAar) settings.SelectedExtensions.Add(".aar");
            if (settings.IncludePy) settings.SelectedExtensions.Add(".py");

            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFilePath, json);
        }
    }
}
```

---

## FILE 11: ResourceService.cs

<a id='resourceservicecs'></a>

```csharp
using Windows.ApplicationModel.Resources;

namespace DocGen.Services
{
    public class ResourceService
    {
        private ResourceLoader _resourceLoader = new ResourceLoader();
        public string GetLocalizedResource(string resourceKey)
        {
            try
            {
                var value = _resourceLoader.GetString(resourceKey);
                return string.IsNullOrEmpty(value) ? resourceKey : value;
            }
            catch
            {
                return resourceKey;
            }
        }
    }
}
```

---

## FILE 12: DocGenViewModel.cs

<a id='docgenviewmodelcs'></a>

```csharp
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
```

---

