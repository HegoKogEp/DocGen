# DocGen - Project Documentation

## Content

1. [DocGen](#docgen)
   1. [App.xaml](#app)
   2. [App.xaml.cs](#appxaml)
   3. [DocGen.csproj](#docgen)
   4. [MainWindow.xaml](#mainwindow)
   5. [MainWindow.xaml.cs](#mainwindowxaml)
2. [DocGen\Models](#models)
   1. [AppSettings.cs](#appsettings)
   2. [DocGenModel.cs](#docgenmodel)
3. [DocGen\obj\x64\Debug\net8.0-windows10.0.19041.0](#net80windows10019041)
   1. [.NETCoreApp,Version=v8.0.AssemblyAttributes.cs](#netcoreappversionv80assemblyattributes)
   2. [App.g.cs](#appg)
   3. [App.g.i.cs](#appgi)
   4. [App.xaml](#app)
   5. [DocGen.AssemblyInfo.cs](#docgenassemblyinfo)
   6. [MainWindow.g.cs](#mainwindowg)
   7. [MainWindow.g.i.cs](#mainwindowgi)
   8. [MainWindow.xaml](#mainwindow)
   9. [XamlTypeInfo.g.cs](#xamltypeinfog)
4. [DocGen\Services](#services)
   1. [IWindowService.cs](#iwindowservice)
   2. [LanguageService.cs](#languageservice)
   3. [LoadAppSettingsService.cs](#loadappsettingsservice)
   4. [ResourceService.cs](#resourceservice)
5. [DocGen\ViewModels](#viewmodels)
   1. [DocGenViewModel.cs](#docgenviewmodel)

## FILE 1: Project Root

## DocGen

<a id='docgen'></a>

## FILE 1: App.xaml

<a id='app'></a>

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

<a id='appxaml'></a>

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

<a id='docgen'></a>

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
  </PropertyGroup>
</Project>
```

---

## FILE 4: MainWindow.xaml

<a id='mainwindow'></a>

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
                 Text="{Binding ProjectPath}"
                 Header="{Binding ProjectPathHeader}"/>

        <Button HorizontalAlignment="Right"
                Margin="0 40 10 16"
                Grid.Row="1"
                Command="{Binding BrowseCommand}"
                Content="..." FontSize="10"/>

        <TextBox Grid.Row="2"
                 Height="60"
                 Margin="8"
                 Text="{Binding DocTitle}"
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

<a id='mainwindowxaml'></a>

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

## DocGen\Models

<a id='models'></a>

## FILE 6: AppSettings.cs

<a id='appsettings'></a>

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

<a id='docgenmodel'></a>

```csharp
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
                    var extension = Path.GetExtension(filePath);
                    if (allowedExtensions.Contains(extension))
                    {
                        files.Add(filePath);
                    }
                }

                var documentationContent = new StringBuilder();

                // Add document header
                documentationContent.AppendLine($"# {Path.GetFileName(projectPath)} - Project Documentation");
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

        //public async void GenerateDocumentation(string projectPath, XamlRoot xamlRoot)
        //{
        //    if (string.IsNullOrEmpty(projectPath) || !Directory.Exists(projectPath))
        //    {
        //        var dialog = new ContentDialog
        //        {
        //            Title = _resourceService.GetLocalizedResource("Error"),
        //            Content = _resourceService.GetLocalizedResource("PathErrorMesseage"),
        //            CloseButtonText = "OK",
        //            XamlRoot = xamlRoot
        //        };
        //        await dialog.ShowAsync();

        //        return;
        //    }

        //    var allowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    // load extension settings
        //    if (IncludeXaml) allowedExtensions.Add(".xaml");
        //    if (IncludeCs) allowedExtensions.Add(".cs");
        //    if (IncludeCsproj) allowedExtensions.Add(".csproj");
        //    if (IncludeJson) allowedExtensions.Add(".json");
        //    if (IncludeManifest) allowedExtensions.Add(".manifest");
        //    if (IncludeKt) allowedExtensions.Add(".kt");
        //    if (IncludeGradle) allowedExtensions.Add(".gradle");
        //    if (IncludeKts) allowedExtensions.Add(".kts");
        //    if (IncludeXml) allowedExtensions.Add(".xml");
        //    if (IncludeProperties) allowedExtensions.Add(".properties");
        //    if (IncludeJava) allowedExtensions.Add(".java");
        //    if (IncludeJar) allowedExtensions.Add(".jar");
        //    if (IncludeAar) allowedExtensions.Add(".aar");
        //    if (IncludePy) allowedExtensions.Add(".py");

        //    // use default extensions if nothing is used
        //    if (allowedExtensions.Count == 0)
        //    {
        //        allowedExtensions.Add(".cs");
        //        allowedExtensions.Add(".xaml");
        //        allowedExtensions.Add(".csproj");
        //    }

        //    var documentationContent = new StringBuilder();

        //    try
        //    {
        //        foreach (var filePath in Directory.EnumerateFiles(projectPath, "*.*", SearchOption.AllDirectories))
        //        {
        //            var extension = Path.GetExtension(filePath);
        //            if (allowedExtensions.Contains(extension))
        //            {
        //                documentationContent.AppendLine($"--- File: {filePath} ---");
        //                documentationContent.AppendLine();

        //                string fileContent = await File.ReadAllTextAsync(filePath);

        //                documentationContent.AppendLine(fileContent);
        //                documentationContent.AppendLine();
        //            }
        //        }

        //        string outputFilePath = Path.Combine(projectPath, "readme.md");
        //        await File.WriteAllTextAsync(outputFilePath, documentationContent.ToString());

        //        var dialog = new ContentDialog
        //        {
        //            Title = _resourceService.GetLocalizedResource("Success"),
        //            Content = $"{_resourceService.GetLocalizedResource("SuccessMesseage")}:\n {outputFilePath}",
        //            CloseButtonText = "OK",
        //            XamlRoot = xamlRoot
        //        };
        //        await dialog.ShowAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        var dialog = new ContentDialog
        //        {
        //            Title = _resourceService.GetLocalizedResource("Error"),
        //            Content = $"{_resourceService.GetLocalizedResource("Error")}: {ex.Message}",
        //            CloseButtonText = "OK",
        //            XamlRoot = xamlRoot
        //        };
        //        await dialog.ShowAsync();
        //    }
        //}
    }
}

```

---

## DocGen\obj\x64\Debug\net8.0-windows10.0.19041.0

<a id='net80windows10019041'></a>

## FILE 8: .NETCoreApp,Version=v8.0.AssemblyAttributes.cs

<a id='netcoreappversionv80assemblyattributes'></a>

```csharp
// <autogenerated />
using System;
using System.Reflection;
[assembly: global::System.Runtime.Versioning.TargetFrameworkAttribute(".NETCoreApp,Version=v8.0", FrameworkDisplayName = ".NET 8.0")]

```

---

## FILE 9: App.g.cs

<a id='appg'></a>

```csharp
#pragma checksum "C:\Users\Виталий\source\repos\DocGen\DocGen\App.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4281B6E0C2C2CAD4211030F92E9CC9D6028818B031CA6B0A586484DD7B57CA2A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DocGen
{
    partial class App : global::Microsoft.UI.Xaml.Application
    {
    }
}


```

---

## FILE 10: App.g.i.cs

<a id='appgi'></a>

```csharp
#pragma checksum "C:\Users\Виталий\source\repos\DocGen\DocGen\App.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4281B6E0C2C2CAD4211030F92E9CC9D6028818B031CA6B0A586484DD7B57CA2A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace DocGen
{
#if !DISABLE_XAML_GENERATED_MAIN
    /// <summary>
    /// Program class
    /// </summary>
    public static class Program
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.STAThreadAttribute]
        static void Main(string[] args)
        {
            global::WinRT.ComWrappersSupport.InitializeComWrappers();
            global::Microsoft.UI.Xaml.Application.Start((p) => {
                var context = new global::Microsoft.UI.Dispatching.DispatcherQueueSynchronizationContext(global::Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread());
                global::System.Threading.SynchronizationContext.SetSynchronizationContext(context);
                new App();
            });
        }
    }
#endif

    partial class App : global::Microsoft.UI.Xaml.Application
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        private bool _contentLoaded;
        /// <summary>
        /// InitializeComponent()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;

            global::System.Uri resourceLocator = new global::System.Uri("ms-appx:///App.xaml");
            global::Microsoft.UI.Xaml.Application.LoadComponent(this, resourceLocator);

#if DEBUG && !DISABLE_XAML_GENERATED_BINDING_DEBUG_OUTPUT
            DebugSettings.BindingFailed += (sender, args) =>
            {
                global::System.Diagnostics.Debug.WriteLine(args.Message);
            };
#endif
#if DEBUG && !DISABLE_XAML_GENERATED_RESOURCE_REFERENCE_DEBUG_OUTPUT
            DebugSettings.XamlResourceReferenceFailed  += (sender, args) =>
            {
                global::System.Diagnostics.Debug.WriteLine(args.Message);
            };
#endif
#if DEBUG && !DISABLE_XAML_GENERATED_BREAK_ON_UNHANDLED_EXCEPTION
            UnhandledException += (sender, e) =>
            {
                if (global::System.Diagnostics.Debugger.IsAttached) global::System.Diagnostics.Debugger.Break();
            };
#endif
        }
    }
}


```

---

## FILE 11: App.xaml

<a id='app'></a>

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

## FILE 12: DocGen.AssemblyInfo.cs

<a id='docgenassemblyinfo'></a>

```csharp
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Reflection;

[assembly: System.Reflection.AssemblyCompanyAttribute("DocGen")]
[assembly: System.Reflection.AssemblyConfigurationAttribute("Debug")]
[assembly: System.Reflection.AssemblyFileVersionAttribute("1.0.0.0")]
[assembly: System.Reflection.AssemblyInformationalVersionAttribute("1.0.0+ff982c30d7ad42c6400b24d9b755cf91825ddc47")]
[assembly: System.Reflection.AssemblyProductAttribute("DocGen")]
[assembly: System.Reflection.AssemblyTitleAttribute("DocGen")]
[assembly: System.Reflection.AssemblyVersionAttribute("1.0.0.0")]
[assembly: System.Runtime.Versioning.TargetPlatformAttribute("Windows10.0.19041.0")]
[assembly: System.Runtime.Versioning.SupportedOSPlatformAttribute("Windows10.0.17763.0")]

// Создано классом WriteCodeFragment MSBuild.


```

---

## FILE 13: MainWindow.g.cs

<a id='mainwindowg'></a>

```csharp
#pragma checksum "C:\Users\Виталий\source\repos\DocGen\DocGen\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "86B85A93ABBB89CB309755F3E429E4F2086599D3A1CEB104EC05EB008620E3F0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DocGen
{
    partial class MainWindow : 
        global::Microsoft.UI.Xaml.Window, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Microsoft_UI_Xaml_FrameworkElement_DataContext(global::Microsoft.UI.Xaml.FrameworkElement obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.DataContext = value;
            }
            public static void Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj, global::System.Boolean value)
            {
                obj.IsChecked = value;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private partial class MainWindow_obj1_Bindings :
            global::Microsoft.UI.Xaml.Markup.IDataTemplateComponent,
            global::Microsoft.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Microsoft.UI.Xaml.Markup.IComponentConnector,
            IMainWindow_Bindings
        {
            private global::DocGen.MainWindow dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Microsoft.UI.Xaml.Controls.Grid obj2;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj4;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj5;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj6;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj7;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj8;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj9;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj10;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj11;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj12;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj13;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj14;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj15;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj16;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj17;
            private global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem obj18;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj2DataContextDisabled = false;
            private static bool isobj4IsCheckedDisabled = false;
            private static bool isobj5IsCheckedDisabled = false;
            private static bool isobj6IsCheckedDisabled = false;
            private static bool isobj7IsCheckedDisabled = false;
            private static bool isobj8IsCheckedDisabled = false;
            private static bool isobj9IsCheckedDisabled = false;
            private static bool isobj10IsCheckedDisabled = false;
            private static bool isobj11IsCheckedDisabled = false;
            private static bool isobj12IsCheckedDisabled = false;
            private static bool isobj13IsCheckedDisabled = false;
            private static bool isobj14IsCheckedDisabled = false;
            private static bool isobj15IsCheckedDisabled = false;
            private static bool isobj16IsCheckedDisabled = false;
            private static bool isobj17IsCheckedDisabled = false;
            private static bool isobj18IsCheckedDisabled = false;

            private MainWindow_obj1_BindingsTracking bindingsTracking;

            public MainWindow_obj1_Bindings()
            {
                this.bindingsTracking = new MainWindow_obj1_BindingsTracking(this);
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 16 && columnNumber == 11)
                {
                    isobj2DataContextDisabled = true;
                }
                else if (lineNumber == 27 && columnNumber == 54)
                {
                    isobj4IsCheckedDisabled = true;
                }
                else if (lineNumber == 28 && columnNumber == 56)
                {
                    isobj5IsCheckedDisabled = true;
                }
                else if (lineNumber == 29 && columnNumber == 54)
                {
                    isobj6IsCheckedDisabled = true;
                }
                else if (lineNumber == 30 && columnNumber == 58)
                {
                    isobj7IsCheckedDisabled = true;
                }
                else if (lineNumber == 31 && columnNumber == 56)
                {
                    isobj8IsCheckedDisabled = true;
                }
                else if (lineNumber == 32 && columnNumber == 60)
                {
                    isobj9IsCheckedDisabled = true;
                }
                else if (lineNumber == 33 && columnNumber == 54)
                {
                    isobj10IsCheckedDisabled = true;
                }
                else if (lineNumber == 34 && columnNumber == 58)
                {
                    isobj11IsCheckedDisabled = true;
                }
                else if (lineNumber == 35 && columnNumber == 55)
                {
                    isobj12IsCheckedDisabled = true;
                }
                else if (lineNumber == 36 && columnNumber == 55)
                {
                    isobj13IsCheckedDisabled = true;
                }
                else if (lineNumber == 37 && columnNumber == 62)
                {
                    isobj14IsCheckedDisabled = true;
                }
                else if (lineNumber == 38 && columnNumber == 56)
                {
                    isobj15IsCheckedDisabled = true;
                }
                else if (lineNumber == 39 && columnNumber == 55)
                {
                    isobj16IsCheckedDisabled = true;
                }
                else if (lineNumber == 40 && columnNumber == 55)
                {
                    isobj17IsCheckedDisabled = true;
                }
                else if (lineNumber == 41 && columnNumber == 56)
                {
                    isobj18IsCheckedDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 2: // MainWindow.xaml line 16
                        this.obj2 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Grid>(target);
                        break;
                    case 4: // MainWindow.xaml line 27
                        this.obj4 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_4(this.obj4);
                        break;
                    case 5: // MainWindow.xaml line 28
                        this.obj5 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_5(this.obj5);
                        break;
                    case 6: // MainWindow.xaml line 29
                        this.obj6 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_6(this.obj6);
                        break;
                    case 7: // MainWindow.xaml line 30
                        this.obj7 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_7(this.obj7);
                        break;
                    case 8: // MainWindow.xaml line 31
                        this.obj8 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_8(this.obj8);
                        break;
                    case 9: // MainWindow.xaml line 32
                        this.obj9 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_9(this.obj9);
                        break;
                    case 10: // MainWindow.xaml line 33
                        this.obj10 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_10(this.obj10);
                        break;
                    case 11: // MainWindow.xaml line 34
                        this.obj11 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_11(this.obj11);
                        break;
                    case 12: // MainWindow.xaml line 35
                        this.obj12 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_12(this.obj12);
                        break;
                    case 13: // MainWindow.xaml line 36
                        this.obj13 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_13(this.obj13);
                        break;
                    case 14: // MainWindow.xaml line 37
                        this.obj14 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_14(this.obj14);
                        break;
                    case 15: // MainWindow.xaml line 38
                        this.obj15 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_15(this.obj15);
                        break;
                    case 16: // MainWindow.xaml line 39
                        this.obj16 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_16(this.obj16);
                        break;
                    case 17: // MainWindow.xaml line 40
                        this.obj17 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_17(this.obj17);
                        break;
                    case 18: // MainWindow.xaml line 41
                        this.obj18 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                        this.bindingsTracking.RegisterTwoWayListener_18(this.obj18);
                        break;
                    default:
                        break;
                }
            }
                        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
                        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target) 
                        {
                            return null;
                        }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
            }

            public void Recycle()
            {
                return;
            }

            // IMainWindow_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                if (newDataRoot != null)
                {
                    this.dataRoot = global::WinRT.CastExtensions.As<global::DocGen.MainWindow>(newDataRoot);
                    return true;
                }
                return false;
            }

            public void Activated(object obj, global::Microsoft.UI.Xaml.WindowActivatedEventArgs data)
            {
                this.Initialize();
            }

            public void Loading(global::Microsoft.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::DocGen.MainWindow obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_ViewModel(obj.ViewModel, phase);
                    }
                }
            }
            private void Update_ViewModel(global::DocGen.ViewModels.DocGenViewModel obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_ViewModel(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_ViewModel_IncludePy(obj.IncludePy, phase);
                        this.Update_ViewModel_IncludeXaml(obj.IncludeXaml, phase);
                        this.Update_ViewModel_IncludeCs(obj.IncludeCs, phase);
                        this.Update_ViewModel_IncludeCsproj(obj.IncludeCsproj, phase);
                        this.Update_ViewModel_IncludeJson(obj.IncludeJson, phase);
                        this.Update_ViewModel_IncludeManifest(obj.IncludeManifest, phase);
                        this.Update_ViewModel_IncludeKt(obj.IncludeKt, phase);
                        this.Update_ViewModel_IncludeGradle(obj.IncludeGradle, phase);
                        this.Update_ViewModel_IncludeKts(obj.IncludeKts, phase);
                        this.Update_ViewModel_IncludeXml(obj.IncludeXml, phase);
                        this.Update_ViewModel_IncludeProperties(obj.IncludeProperties, phase);
                        this.Update_ViewModel_IncludeJava(obj.IncludeJava, phase);
                        this.Update_ViewModel_IncludeJar(obj.IncludeJar, phase);
                        this.Update_ViewModel_IncludeAar(obj.IncludeAar, phase);
                        this.Update_ViewModel_IncludeToml(obj.IncludeToml, phase);
                    }
                }
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // MainWindow.xaml line 16
                    if (!isobj2DataContextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_FrameworkElement_DataContext(this.obj2, obj, null);
                    }
                }
            }
            private void Update_ViewModel_IncludePy(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 27
                    if (!isobj4IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj4, obj);
                    }
                }
            }
            private void Update_ViewModel_IncludeXaml(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 28
                    if (!isobj5IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj5, obj);
                    }
                }
            }
            private void Update_ViewModel_IncludeCs(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 29
                    if (!isobj6IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj6, obj);
                    }
                }
            }
            private void Update_ViewModel_IncludeCsproj(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 30
                    if (!isobj7IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj7, obj);
                    }
                }
            }
            private void Update_ViewModel_IncludeJson(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 31
                    if (!isobj8IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj8, obj);
                    }
                }
            }
            private void Update_ViewModel_IncludeManifest(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 32
                    if (!isobj9IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj9, obj);
                    }
                }
            }
            private void Update_ViewModel_IncludeKt(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 33
                    if (!isobj10IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj10, obj);
                    }
                }
            }
            private void Update_ViewModel_IncludeGradle(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 34
                    if (!isobj11IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj11, obj);
                    }
                }
            }
            private void Update_ViewModel_IncludeKts(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 35
                    if (!isobj12IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj12, obj);
                    }
                }
            }
            private void Update_ViewModel_IncludeXml(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 36
                    if (!isobj13IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj13, obj);
                    }
                }
            }
            private void Update_ViewModel_IncludeProperties(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 37
                    if (!isobj14IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj14, obj);
                    }
                }
            }
            private void Update_ViewModel_IncludeJava(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 38
                    if (!isobj15IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj15, obj);
                    }
                }
            }
            private void Update_ViewModel_IncludeJar(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 39
                    if (!isobj16IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj16, obj);
                    }
                }
            }
            private void Update_ViewModel_IncludeAar(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 40
                    if (!isobj17IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj17, obj);
                    }
                }
            }
            private void Update_ViewModel_IncludeToml(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainWindow.xaml line 41
                    if (!isobj18IsCheckedDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ToggleMenuFlyoutItem_IsChecked(this.obj18, obj);
                    }
                }
            }
            private void UpdateTwoWay_4_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludePy = this.obj4.IsChecked;
                        }
                    }
                }
            }
            private void UpdateTwoWay_5_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludeXaml = this.obj5.IsChecked;
                        }
                    }
                }
            }
            private void UpdateTwoWay_6_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludeCs = this.obj6.IsChecked;
                        }
                    }
                }
            }
            private void UpdateTwoWay_7_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludeCsproj = this.obj7.IsChecked;
                        }
                    }
                }
            }
            private void UpdateTwoWay_8_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludeJson = this.obj8.IsChecked;
                        }
                    }
                }
            }
            private void UpdateTwoWay_9_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludeManifest = this.obj9.IsChecked;
                        }
                    }
                }
            }
            private void UpdateTwoWay_10_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludeKt = this.obj10.IsChecked;
                        }
                    }
                }
            }
            private void UpdateTwoWay_11_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludeGradle = this.obj11.IsChecked;
                        }
                    }
                }
            }
            private void UpdateTwoWay_12_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludeKts = this.obj12.IsChecked;
                        }
                    }
                }
            }
            private void UpdateTwoWay_13_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludeXml = this.obj13.IsChecked;
                        }
                    }
                }
            }
            private void UpdateTwoWay_14_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludeProperties = this.obj14.IsChecked;
                        }
                    }
                }
            }
            private void UpdateTwoWay_15_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludeJava = this.obj15.IsChecked;
                        }
                    }
                }
            }
            private void UpdateTwoWay_16_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludeJar = this.obj16.IsChecked;
                        }
                    }
                }
            }
            private void UpdateTwoWay_17_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludeAar = this.obj17.IsChecked;
                        }
                    }
                }
            }
            private void UpdateTwoWay_18_IsChecked()
            {
                if (this.initialized)
                {
                    if (this.dataRoot != null)
                    {
                        if (this.dataRoot.ViewModel != null)
                        {
                            this.dataRoot.ViewModel.IncludeToml = this.obj18.IsChecked;
                        }
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private class MainWindow_obj1_BindingsTracking
            {
                private global::System.WeakReference<MainWindow_obj1_Bindings> weakRefToBindingObj; 

                public MainWindow_obj1_BindingsTracking(MainWindow_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<MainWindow_obj1_Bindings>(obj);
                }

                public MainWindow_obj1_Bindings TryGetBindingObject()
                {
                    MainWindow_obj1_Bindings bindingObject = null;
                    if (weakRefToBindingObj != null)
                    {
                        weakRefToBindingObj.TryGetTarget(out bindingObject);
                        if (bindingObject == null)
                        {
                            weakRefToBindingObj = null;
                            ReleaseAllListeners();
                        }
                    }
                    return bindingObject;
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_ViewModel(null);
                }

                public void PropertyChanged_ViewModel(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    MainWindow_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        string propName = e.PropertyName;
                        global::DocGen.ViewModels.DocGenViewModel obj = sender as global::DocGen.ViewModels.DocGenViewModel;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                            if (obj != null)
                            {
                                bindings.Update_ViewModel_IncludePy(obj.IncludePy, DATA_CHANGED);
                                bindings.Update_ViewModel_IncludeXaml(obj.IncludeXaml, DATA_CHANGED);
                                bindings.Update_ViewModel_IncludeCs(obj.IncludeCs, DATA_CHANGED);
                                bindings.Update_ViewModel_IncludeCsproj(obj.IncludeCsproj, DATA_CHANGED);
                                bindings.Update_ViewModel_IncludeJson(obj.IncludeJson, DATA_CHANGED);
                                bindings.Update_ViewModel_IncludeManifest(obj.IncludeManifest, DATA_CHANGED);
                                bindings.Update_ViewModel_IncludeKt(obj.IncludeKt, DATA_CHANGED);
                                bindings.Update_ViewModel_IncludeGradle(obj.IncludeGradle, DATA_CHANGED);
                                bindings.Update_ViewModel_IncludeKts(obj.IncludeKts, DATA_CHANGED);
                                bindings.Update_ViewModel_IncludeXml(obj.IncludeXml, DATA_CHANGED);
                                bindings.Update_ViewModel_IncludeProperties(obj.IncludeProperties, DATA_CHANGED);
                                bindings.Update_ViewModel_IncludeJava(obj.IncludeJava, DATA_CHANGED);
                                bindings.Update_ViewModel_IncludeJar(obj.IncludeJar, DATA_CHANGED);
                                bindings.Update_ViewModel_IncludeAar(obj.IncludeAar, DATA_CHANGED);
                                bindings.Update_ViewModel_IncludeToml(obj.IncludeToml, DATA_CHANGED);
                            }
                        }
                        else
                        {
                            switch (propName)
                            {
                                case "IncludePy":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludePy(obj.IncludePy, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IncludeXaml":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludeXaml(obj.IncludeXaml, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IncludeCs":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludeCs(obj.IncludeCs, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IncludeCsproj":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludeCsproj(obj.IncludeCsproj, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IncludeJson":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludeJson(obj.IncludeJson, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IncludeManifest":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludeManifest(obj.IncludeManifest, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IncludeKt":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludeKt(obj.IncludeKt, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IncludeGradle":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludeGradle(obj.IncludeGradle, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IncludeKts":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludeKts(obj.IncludeKts, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IncludeXml":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludeXml(obj.IncludeXml, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IncludeProperties":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludeProperties(obj.IncludeProperties, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IncludeJava":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludeJava(obj.IncludeJava, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IncludeJar":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludeJar(obj.IncludeJar, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IncludeAar":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludeAar(obj.IncludeAar, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "IncludeToml":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ViewModel_IncludeToml(obj.IncludeToml, DATA_CHANGED);
                                    }
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                    }
                }
                private global::DocGen.ViewModels.DocGenViewModel cache_ViewModel = null;
                public void UpdateChildListeners_ViewModel(global::DocGen.ViewModels.DocGenViewModel obj)
                {
                    if (obj != cache_ViewModel)
                    {
                        if (cache_ViewModel != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)cache_ViewModel).PropertyChanged -= PropertyChanged_ViewModel;
                            cache_ViewModel = null;
                        }
                        if (obj != null)
                        {
                            cache_ViewModel = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_ViewModel;
                        }
                    }
                }
                public void RegisterTwoWayListener_4(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_4_IsChecked();
                        }
                    });
                }
                public void RegisterTwoWayListener_5(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_5_IsChecked();
                        }
                    });
                }
                public void RegisterTwoWayListener_6(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_6_IsChecked();
                        }
                    });
                }
                public void RegisterTwoWayListener_7(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_7_IsChecked();
                        }
                    });
                }
                public void RegisterTwoWayListener_8(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_8_IsChecked();
                        }
                    });
                }
                public void RegisterTwoWayListener_9(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_9_IsChecked();
                        }
                    });
                }
                public void RegisterTwoWayListener_10(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_10_IsChecked();
                        }
                    });
                }
                public void RegisterTwoWayListener_11(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_11_IsChecked();
                        }
                    });
                }
                public void RegisterTwoWayListener_12(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_12_IsChecked();
                        }
                    });
                }
                public void RegisterTwoWayListener_13(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_13_IsChecked();
                        }
                    });
                }
                public void RegisterTwoWayListener_14(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_14_IsChecked();
                        }
                    });
                }
                public void RegisterTwoWayListener_15(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_15_IsChecked();
                        }
                    });
                }
                public void RegisterTwoWayListener_16(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_16_IsChecked();
                        }
                    });
                }
                public void RegisterTwoWayListener_17(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_17_IsChecked();
                        }
                    });
                }
                public void RegisterTwoWayListener_18(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem.IsCheckedProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_18_IsChecked();
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 3: // MainWindow.xaml line 70
                {
                    this.GenerateButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }


        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1: // MainWindow.xaml line 2
                {                    
                    global::Microsoft.UI.Xaml.Window element1 = (global::Microsoft.UI.Xaml.Window)target;
                    MainWindow_obj1_Bindings bindings = new MainWindow_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Activated += bindings.Activated;
                }
                break;
            }
            return returnValue;
        }
    }
}


```

---

## FILE 14: MainWindow.g.i.cs

<a id='mainwindowgi'></a>

```csharp
#pragma checksum "C:\Users\Виталий\source\repos\DocGen\DocGen\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "86B85A93ABBB89CB309755F3E429E4F2086599D3A1CEB104EC05EB008620E3F0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DocGen
{
    partial class MainWindow : global::Microsoft.UI.Xaml.Window
    {


#pragma warning disable 0169    //  Proactively suppress unused/uninitialized field warning in case they aren't used, for things like x:Name
#pragma warning disable 0649
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        private global::Microsoft.UI.Xaml.Controls.Button GenerateButton; 
#pragma warning restore 0649
#pragma warning restore 0169
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;

            global::System.Uri resourceLocator = new global::System.Uri("ms-appx:///MainWindow.xaml");
            global::Microsoft.UI.Xaml.Application.LoadComponent(this, resourceLocator, global::Microsoft.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
        }

        partial void UnloadObject(global::Microsoft.UI.Xaml.DependencyObject unloadableObject);

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        private interface IMainWindow_Bindings
        {
            void Initialize();
            void Update();
            void StopTracking();
            void DisconnectUnloadedObject(int connectionId);
        }

        private interface IMainWindow_BindingsScopeConnector
        {
            global::System.WeakReference Parent { get; set; }
            bool ContainsElement(int connectionId);
            void RegisterForElementConnection(int connectionId, global::Microsoft.UI.Xaml.Markup.IComponentConnector connector);
        }
#pragma warning disable 0169    //  Proactively suppress unused field warning in case Bindings is not used.
#pragma warning disable 0649
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        private IMainWindow_Bindings Bindings;
#pragma warning restore 0649
#pragma warning restore 0169
    }
}



```

---

## FILE 15: MainWindow.xaml

<a id='mainwindow'></a>

```xml
<?xml version="1.0" encoding="utf-8"?>
<Window x:ConnectionId='1'
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

    <Grid x:ConnectionId='2'                                 >
        <Grid.RowDefinitions>
            <RowDefinition Height="40"/>
            <RowDefinition Height="80"/>
            <RowDefinition Height="80"/>
            <RowDefinition Height="80"/>
        </Grid.RowDefinitions>

        <MenuBar Grid.Row="0">
            <MenuBarItem Title="{Binding SettingsMenuTitle}">
                <MenuFlyoutSubItem Text="{Binding ExtensionsMenuItemTitle}">
                    <ToggleMenuFlyoutItem x:ConnectionId='4' Text=".py"                                                       />
                    <ToggleMenuFlyoutItem x:ConnectionId='5' Text=".xaml"                                                         />
                    <ToggleMenuFlyoutItem x:ConnectionId='6' Text=".cs"                                                       />
                    <ToggleMenuFlyoutItem x:ConnectionId='7' Text=".csproj"                                                           />
                    <ToggleMenuFlyoutItem x:ConnectionId='8' Text=".json"                                                         />
                    <ToggleMenuFlyoutItem x:ConnectionId='9' Text=".manifest"                                                             />
                    <ToggleMenuFlyoutItem x:ConnectionId='10' Text=".kt"                                                       />
                    <ToggleMenuFlyoutItem x:ConnectionId='11' Text=".gradle"                                                           />
                    <ToggleMenuFlyoutItem x:ConnectionId='12' Text=".kts"                                                        />
                    <ToggleMenuFlyoutItem x:ConnectionId='13' Text=".xml"                                                        />
                    <ToggleMenuFlyoutItem x:ConnectionId='14' Text=".properties"                                                               />
                    <ToggleMenuFlyoutItem x:ConnectionId='15' Text=".java"                                                         />
                    <ToggleMenuFlyoutItem x:ConnectionId='16' Text=".jar"                                                        />
                    <ToggleMenuFlyoutItem x:ConnectionId='17' Text=".aar"                                                        />
                    <ToggleMenuFlyoutItem x:ConnectionId='18' Text=".toml"                                                         />
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
                 Text="{Binding ProjectPath}"
                 Header="{Binding ProjectPathHeader}"/>

        <Button HorizontalAlignment="Right"
                Margin="0 40 10 16"
                Grid.Row="1"
                Command="{Binding BrowseCommand}"
                Content="..." FontSize="10"/>

        <TextBox Grid.Row="2"
                 Height="60"
                 Margin="8"
                 Text="{Binding DocTitle}"
                 Header="{Binding DocTitleHeader}"/>

        <Button x:ConnectionId='3' x:Name="GenerateButton"
                Grid.Row="3"
                Margin="8"
                Command="{Binding GenerateDocumentationCommand}"
                HorizontalAlignment="Right"
                Content="{Binding GenerateButtonTitle}"/>
    </Grid>
</Window>


```

---

## FILE 16: XamlTypeInfo.g.cs

<a id='xamltypeinfog'></a>

```csharp
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;


namespace DocGen
{
    public partial class App : global::Microsoft.UI.Xaml.Markup.IXamlMetadataProvider
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        private global::DocGen.DocGen_XamlTypeInfo.XamlMetaDataProvider __appProvider;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::DocGen.DocGen_XamlTypeInfo.XamlMetaDataProvider _AppProvider
        {
            get
            {
                if (__appProvider == null)
                {
                    __appProvider = new global::DocGen.DocGen_XamlTypeInfo.XamlMetaDataProvider();
                }
                return __appProvider;
            }
        }

        /// <summary>
        /// GetXamlType(Type)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IXamlType GetXamlType(global::System.Type type)
        {
            return _AppProvider.GetXamlType(type);
        }

        /// <summary>
        /// GetXamlType(String)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IXamlType GetXamlType(string fullName)
        {
            return _AppProvider.GetXamlType(fullName);
        }

        /// <summary>
        /// GetXmlnsDefinitions()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return _AppProvider.GetXmlnsDefinitions();
        }
    }
}

namespace DocGen.DocGen_XamlTypeInfo
{
    /// <summary>
    /// Main class for providing metadata for the app or library
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public sealed partial class XamlMetaDataProvider : global::Microsoft.UI.Xaml.Markup.IXamlMetadataProvider
    {
        private global::DocGen.DocGen_XamlTypeInfo.XamlTypeInfoProvider _provider = null;

        private global::DocGen.DocGen_XamlTypeInfo.XamlTypeInfoProvider Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new global::DocGen.DocGen_XamlTypeInfo.XamlTypeInfoProvider();
                }
                return _provider;
            }
        }

        /// <summary>
        /// GetXamlType(Type)
        /// </summary>
        [global::Windows.Foundation.Metadata.DefaultOverload]
        public global::Microsoft.UI.Xaml.Markup.IXamlType GetXamlType(global::System.Type type)
        {
            return Provider.GetXamlTypeByType(type);
        }

        /// <summary>
        /// GetXamlType(String)
        /// </summary>
        public global::Microsoft.UI.Xaml.Markup.IXamlType GetXamlType(string fullName)
        {
            return Provider.GetXamlTypeByName(fullName);
        }

        /// <summary>
        /// GetXmlnsDefinitions()
        /// </summary>
        public global::Microsoft.UI.Xaml.Markup.XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return new global::Microsoft.UI.Xaml.Markup.XmlnsDefinition[0];
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal partial class XamlTypeInfoProvider
    {
        public global::Microsoft.UI.Xaml.Markup.IXamlType GetXamlTypeByType(global::System.Type type)
        {
            global::Microsoft.UI.Xaml.Markup.IXamlType xamlType;
            lock (_xamlTypeCacheByType) 
            { 
                if (_xamlTypeCacheByType.TryGetValue(type, out xamlType))
                {
                    return xamlType;
                }
                int typeIndex = LookupTypeIndexByType(type);
                if(typeIndex != -1)
                {
                    xamlType = CreateXamlType(typeIndex);
                }
                var userXamlType = xamlType as global::DocGen.DocGen_XamlTypeInfo.XamlUserType;
                if(xamlType == null || (userXamlType != null && userXamlType.IsReturnTypeStub && !userXamlType.IsLocalType))
                {
                    global::Microsoft.UI.Xaml.Markup.IXamlType libXamlType = CheckOtherMetadataProvidersForType(type);
                    if (libXamlType != null)
                    {
                        if(libXamlType.IsConstructible || xamlType == null)
                        {
                            xamlType = libXamlType;
                        }
                    }
                }
                if (xamlType != null)
                {
                    _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                    _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
                }
            }
            return xamlType;
        }

        public global::Microsoft.UI.Xaml.Markup.IXamlType GetXamlTypeByName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }
            global::Microsoft.UI.Xaml.Markup.IXamlType xamlType;
            lock (_xamlTypeCacheByType)
            {
                if (_xamlTypeCacheByName.TryGetValue(typeName, out xamlType))
                {
                    return xamlType;
                }
                int typeIndex = LookupTypeIndexByName(typeName);
                if(typeIndex != -1)
                {
                    xamlType = CreateXamlType(typeIndex);
                }
                var userXamlType = xamlType as global::DocGen.DocGen_XamlTypeInfo.XamlUserType;
                if(xamlType == null || (userXamlType != null && userXamlType.IsReturnTypeStub && !userXamlType.IsLocalType))
                {
                    global::Microsoft.UI.Xaml.Markup.IXamlType libXamlType = CheckOtherMetadataProvidersForName(typeName);
                    if (libXamlType != null)
                    {
                        if(libXamlType.IsConstructible || xamlType == null)
                        {
                            xamlType = libXamlType;
                        }
                    }
                }
                if (xamlType != null)
                {
                    _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                    _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
                }
            }
            return xamlType;
        }

        public global::Microsoft.UI.Xaml.Markup.IXamlMember GetMemberByLongName(string longMemberName)
        {
            if (string.IsNullOrEmpty(longMemberName))
            {
                return null;
            }
            global::Microsoft.UI.Xaml.Markup.IXamlMember xamlMember;
            lock (_xamlMembers)
            {
                if (_xamlMembers.TryGetValue(longMemberName, out xamlMember))
                {
                    return xamlMember;
                }
                xamlMember = CreateXamlMember(longMemberName);
                if (xamlMember != null)
                {
                    _xamlMembers.Add(longMemberName, xamlMember);
                }
            }
            return xamlMember;
        }

        global::System.Collections.Generic.Dictionary<string, global::Microsoft.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByName = new global::System.Collections.Generic.Dictionary<string, global::Microsoft.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<global::System.Type, global::Microsoft.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByType = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Microsoft.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<string, global::Microsoft.UI.Xaml.Markup.IXamlMember>
                _xamlMembers = new global::System.Collections.Generic.Dictionary<string, global::Microsoft.UI.Xaml.Markup.IXamlMember>();

        string[] _typeNameTable = null;
        global::System.Type[] _typeTable = null;
        
        private void InitTypeTables()
        {
            _typeNameTable = new string[22];
            _typeNameTable[0] = "Microsoft.UI.Xaml.Controls.XamlControlsResources";
            _typeNameTable[1] = "Microsoft.UI.Xaml.ResourceDictionary";
            _typeNameTable[2] = "Object";
            _typeNameTable[3] = "Boolean";
            _typeNameTable[4] = "Microsoft.UI.Xaml.Media.MicaBackdrop";
            _typeNameTable[5] = "Microsoft.UI.Xaml.Media.SystemBackdrop";
            _typeNameTable[6] = "Microsoft.UI.Composition.SystemBackdrops.MicaKind";
            _typeNameTable[7] = "System.Enum";
            _typeNameTable[8] = "System.ValueType";
            _typeNameTable[9] = "Microsoft.UI.Xaml.Controls.MenuBar";
            _typeNameTable[10] = "Microsoft.UI.Xaml.Controls.Control";
            _typeNameTable[11] = "System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.MenuBarItem>";
            _typeNameTable[12] = "Microsoft.UI.Xaml.Controls.MenuBarItem";
            _typeNameTable[13] = "System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase>";
            _typeNameTable[14] = "Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase";
            _typeNameTable[15] = "String";
            _typeNameTable[16] = "DocGen.MainWindow";
            _typeNameTable[17] = "Microsoft.UI.Xaml.Window";
            _typeNameTable[18] = "Microsoft.UI.Xaml.Controls.TreeViewNode";
            _typeNameTable[19] = "Microsoft.UI.Xaml.DependencyObject";
            _typeNameTable[20] = "System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.TreeViewNode>";
            _typeNameTable[21] = "Int32";

            _typeTable = new global::System.Type[22];
            _typeTable[0] = typeof(global::Microsoft.UI.Xaml.Controls.XamlControlsResources);
            _typeTable[1] = typeof(global::Microsoft.UI.Xaml.ResourceDictionary);
            _typeTable[2] = typeof(global::System.Object);
            _typeTable[3] = typeof(global::System.Boolean);
            _typeTable[4] = typeof(global::Microsoft.UI.Xaml.Media.MicaBackdrop);
            _typeTable[5] = typeof(global::Microsoft.UI.Xaml.Media.SystemBackdrop);
            _typeTable[6] = typeof(global::Microsoft.UI.Composition.SystemBackdrops.MicaKind);
            _typeTable[7] = typeof(global::System.Enum);
            _typeTable[8] = typeof(global::System.ValueType);
            _typeTable[9] = typeof(global::Microsoft.UI.Xaml.Controls.MenuBar);
            _typeTable[10] = typeof(global::Microsoft.UI.Xaml.Controls.Control);
            _typeTable[11] = typeof(global::System.Collections.Generic.IList<global::Microsoft.UI.Xaml.Controls.MenuBarItem>);
            _typeTable[12] = typeof(global::Microsoft.UI.Xaml.Controls.MenuBarItem);
            _typeTable[13] = typeof(global::System.Collections.Generic.IList<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase>);
            _typeTable[14] = typeof(global::Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase);
            _typeTable[15] = typeof(global::System.String);
            _typeTable[16] = typeof(global::DocGen.MainWindow);
            _typeTable[17] = typeof(global::Microsoft.UI.Xaml.Window);
            _typeTable[18] = typeof(global::Microsoft.UI.Xaml.Controls.TreeViewNode);
            _typeTable[19] = typeof(global::Microsoft.UI.Xaml.DependencyObject);
            _typeTable[20] = typeof(global::System.Collections.Generic.IList<global::Microsoft.UI.Xaml.Controls.TreeViewNode>);
            _typeTable[21] = typeof(global::System.Int32);
        }

        private int LookupTypeIndexByName(string typeName)
        {
            if (_typeNameTable == null)
            {
                InitTypeTables();
            }
            for (int i=0; i<_typeNameTable.Length; i++)
            {
                if(0 == string.CompareOrdinal(_typeNameTable[i], typeName))
                {
                    return i;
                }
            }
            return -1;
        }

        private int LookupTypeIndexByType(global::System.Type type)
        {
            if (_typeTable == null)
            {
                InitTypeTables();
            }
            for(int i=0; i<_typeTable.Length; i++)
            {
                if(type == _typeTable[i])
                {
                    return i;
                }
            }
            return -1;
        }

        private object Activate_0_XamlControlsResources() { return new global::Microsoft.UI.Xaml.Controls.XamlControlsResources(); }
        private object Activate_4_MicaBackdrop() { return new global::Microsoft.UI.Xaml.Media.MicaBackdrop(); }
        private object Activate_9_MenuBar() { return new global::Microsoft.UI.Xaml.Controls.MenuBar(); }
        private object Activate_12_MenuBarItem() { return new global::Microsoft.UI.Xaml.Controls.MenuBarItem(); }
        private object Activate_16_MainWindow() { return new global::DocGen.MainWindow(); }
        private object Activate_18_TreeViewNode() { return new global::Microsoft.UI.Xaml.Controls.TreeViewNode(); }
        private void StaticInitializer_0_XamlControlsResources() => global::System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(global::Microsoft.UI.Xaml.Controls.XamlControlsResources).TypeHandle);
        private void StaticInitializer_4_MicaBackdrop() => global::System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(global::Microsoft.UI.Xaml.Media.MicaBackdrop).TypeHandle);
        private void StaticInitializer_6_MicaKind() => global::System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(global::Microsoft.UI.Composition.SystemBackdrops.MicaKind).TypeHandle);
        private void StaticInitializer_7_Enum() => global::System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(global::System.Enum).TypeHandle);
        private void StaticInitializer_8_ValueType() => global::System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(global::System.ValueType).TypeHandle);
        private void StaticInitializer_9_MenuBar() => global::System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(global::Microsoft.UI.Xaml.Controls.MenuBar).TypeHandle);
        private void StaticInitializer_11_IList() => global::System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(global::System.Collections.Generic.IList<global::Microsoft.UI.Xaml.Controls.MenuBarItem>).TypeHandle);
        private void StaticInitializer_12_MenuBarItem() => global::System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(global::Microsoft.UI.Xaml.Controls.MenuBarItem).TypeHandle);
        private void StaticInitializer_13_IList() => global::System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(global::System.Collections.Generic.IList<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase>).TypeHandle);
        private void StaticInitializer_16_MainWindow() => global::System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(global::DocGen.MainWindow).TypeHandle);
        private void StaticInitializer_18_TreeViewNode() => global::System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(global::Microsoft.UI.Xaml.Controls.TreeViewNode).TypeHandle);
        private void StaticInitializer_20_IList() => global::System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(global::System.Collections.Generic.IList<global::Microsoft.UI.Xaml.Controls.TreeViewNode>).TypeHandle);
        private void MapAdd_0_XamlControlsResources(object instance, object key, object item)
        {
            var collection = (global::System.Collections.Generic.IDictionary<global::System.Object, global::System.Object>)instance;
            var newKey = (global::System.Object)key;
            var newItem = (global::System.Object)item;
            collection.Add(newKey, newItem);
        }
        private void VectorAdd_11_IList(object instance, object item)
        {
            var collection = (global::System.Collections.Generic.ICollection<global::Microsoft.UI.Xaml.Controls.MenuBarItem>)instance;
            var newItem = (global::Microsoft.UI.Xaml.Controls.MenuBarItem)item;
            collection.Add(newItem);
        }
        private void VectorAdd_13_IList(object instance, object item)
        {
            var collection = (global::System.Collections.Generic.ICollection<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase>)instance;
            var newItem = (global::Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase)item;
            collection.Add(newItem);
        }
        private void VectorAdd_20_IList(object instance, object item)
        {
            var collection = (global::System.Collections.Generic.ICollection<global::Microsoft.UI.Xaml.Controls.TreeViewNode>)instance;
            var newItem = (global::Microsoft.UI.Xaml.Controls.TreeViewNode)item;
            collection.Add(newItem);
        }

        private global::Microsoft.UI.Xaml.Markup.IXamlType CreateXamlType(int typeIndex)
        {
            global::DocGen.DocGen_XamlTypeInfo.XamlSystemBaseType xamlType = null;
            global::DocGen.DocGen_XamlTypeInfo.XamlUserType userType;
            string typeName = _typeNameTable[typeIndex];
            global::System.Type type = _typeTable[typeIndex];

            switch (typeIndex)
            {

            case 0:   //  Microsoft.UI.Xaml.Controls.XamlControlsResources
                userType = new global::DocGen.DocGen_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Microsoft.UI.Xaml.ResourceDictionary"));
                userType.Activator = Activate_0_XamlControlsResources;
                userType.StaticInitializer = StaticInitializer_0_XamlControlsResources;
                userType.DictionaryAdd = MapAdd_0_XamlControlsResources;
                userType.AddMemberName("UseCompactResources");
                xamlType = userType;
                break;

            case 1:   //  Microsoft.UI.Xaml.ResourceDictionary
                xamlType = new global::DocGen.DocGen_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 2:   //  Object
                xamlType = new global::DocGen.DocGen_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 3:   //  Boolean
                xamlType = new global::DocGen.DocGen_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 4:   //  Microsoft.UI.Xaml.Media.MicaBackdrop
                userType = new global::DocGen.DocGen_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Media.SystemBackdrop"));
                userType.Activator = Activate_4_MicaBackdrop;
                userType.StaticInitializer = StaticInitializer_4_MicaBackdrop;
                userType.AddMemberName("Kind");
                xamlType = userType;
                break;

            case 5:   //  Microsoft.UI.Xaml.Media.SystemBackdrop
                xamlType = new global::DocGen.DocGen_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 6:   //  Microsoft.UI.Composition.SystemBackdrops.MicaKind
                userType = new global::DocGen.DocGen_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("System.Enum"));
                userType.StaticInitializer = StaticInitializer_6_MicaKind;
                userType.AddEnumValue("Base", global::Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base);
                userType.AddEnumValue("BaseAlt", global::Microsoft.UI.Composition.SystemBackdrops.MicaKind.BaseAlt);
                xamlType = userType;
                break;

            case 7:   //  System.Enum
                userType = new global::DocGen.DocGen_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("System.ValueType"));
                userType.StaticInitializer = StaticInitializer_7_Enum;
                xamlType = userType;
                break;

            case 8:   //  System.ValueType
                userType = new global::DocGen.DocGen_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.StaticInitializer = StaticInitializer_8_ValueType;
                xamlType = userType;
                break;

            case 9:   //  Microsoft.UI.Xaml.Controls.MenuBar
                userType = new global::DocGen.DocGen_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
                userType.Activator = Activate_9_MenuBar;
                userType.StaticInitializer = StaticInitializer_9_MenuBar;
                userType.SetContentPropertyName("Microsoft.UI.Xaml.Controls.MenuBar.Items");
                userType.AddMemberName("Items");
                xamlType = userType;
                break;

            case 10:   //  Microsoft.UI.Xaml.Controls.Control
                xamlType = new global::DocGen.DocGen_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 11:   //  System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.MenuBarItem>
                userType = new global::DocGen.DocGen_XamlTypeInfo.XamlUserType(this, typeName, type, null);
                userType.StaticInitializer = StaticInitializer_11_IList;
                userType.CollectionAdd = VectorAdd_11_IList;
                userType.SetIsReturnTypeStub();
                xamlType = userType;
                break;

            case 12:   //  Microsoft.UI.Xaml.Controls.MenuBarItem
                userType = new global::DocGen.DocGen_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
                userType.Activator = Activate_12_MenuBarItem;
                userType.StaticInitializer = StaticInitializer_12_MenuBarItem;
                userType.SetContentPropertyName("Microsoft.UI.Xaml.Controls.MenuBarItem.Items");
                userType.AddMemberName("Items");
                userType.AddMemberName("Title");
                xamlType = userType;
                break;

            case 13:   //  System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase>
                userType = new global::DocGen.DocGen_XamlTypeInfo.XamlUserType(this, typeName, type, null);
                userType.StaticInitializer = StaticInitializer_13_IList;
                userType.CollectionAdd = VectorAdd_13_IList;
                userType.SetIsReturnTypeStub();
                xamlType = userType;
                break;

            case 14:   //  Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase
                xamlType = new global::DocGen.DocGen_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 15:   //  String
                xamlType = new global::DocGen.DocGen_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 16:   //  DocGen.MainWindow
                userType = new global::DocGen.DocGen_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Window"));
                userType.Activator = Activate_16_MainWindow;
                userType.StaticInitializer = StaticInitializer_16_MainWindow;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 17:   //  Microsoft.UI.Xaml.Window
                xamlType = new global::DocGen.DocGen_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 18:   //  Microsoft.UI.Xaml.Controls.TreeViewNode
                userType = new global::DocGen.DocGen_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Microsoft.UI.Xaml.DependencyObject"));
                userType.Activator = Activate_18_TreeViewNode;
                userType.StaticInitializer = StaticInitializer_18_TreeViewNode;
                userType.AddMemberName("Children");
                userType.AddMemberName("Content");
                userType.AddMemberName("Depth");
                userType.AddMemberName("HasChildren");
                userType.AddMemberName("HasUnrealizedChildren");
                userType.AddMemberName("IsExpanded");
                userType.AddMemberName("Parent");
                userType.SetIsBindable();
                xamlType = userType;
                break;

            case 19:   //  Microsoft.UI.Xaml.DependencyObject
                xamlType = new global::DocGen.DocGen_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 20:   //  System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.TreeViewNode>
                userType = new global::DocGen.DocGen_XamlTypeInfo.XamlUserType(this, typeName, type, null);
                userType.StaticInitializer = StaticInitializer_20_IList;
                userType.CollectionAdd = VectorAdd_20_IList;
                userType.SetIsReturnTypeStub();
                xamlType = userType;
                break;

            case 21:   //  Int32
                xamlType = new global::DocGen.DocGen_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;
            }
            return xamlType;
        }

        private global::System.Collections.Generic.List<global::Microsoft.UI.Xaml.Markup.IXamlMetadataProvider> _otherProviders;
        private global::System.Collections.Generic.List<global::Microsoft.UI.Xaml.Markup.IXamlMetadataProvider> OtherProviders
        {
            get
            {
                if(_otherProviders == null)
                {
                    var otherProviders = new global::System.Collections.Generic.List<global::Microsoft.UI.Xaml.Markup.IXamlMetadataProvider>();
                    global::Microsoft.UI.Xaml.Markup.IXamlMetadataProvider provider;
                    provider = new global::Microsoft.UI.Xaml.XamlTypeInfo.XamlControlsXamlMetaDataProvider() as global::Microsoft.UI.Xaml.Markup.IXamlMetadataProvider;
                    otherProviders.Add(provider); 
                    _otherProviders = otherProviders;
                }
                return _otherProviders;
            }
        }

        private global::Microsoft.UI.Xaml.Markup.IXamlType CheckOtherMetadataProvidersForName(string typeName)
        {
            global::Microsoft.UI.Xaml.Markup.IXamlType xamlType = null;
            global::Microsoft.UI.Xaml.Markup.IXamlType foundXamlType = null;
            foreach(global::Microsoft.UI.Xaml.Markup.IXamlMetadataProvider xmp in OtherProviders)
            {
                xamlType = xmp.GetXamlType(typeName);
                if(xamlType != null)
                {
                    if(xamlType.IsConstructible)    // not Constructible means it might be a Return Type Stub
                    {
                        return xamlType;
                    }
                    foundXamlType = xamlType;
                }
            }
            return foundXamlType;
        }

        private global::Microsoft.UI.Xaml.Markup.IXamlType CheckOtherMetadataProvidersForType(global::System.Type type)
        {
            global::Microsoft.UI.Xaml.Markup.IXamlType xamlType = null;
            global::Microsoft.UI.Xaml.Markup.IXamlType foundXamlType = null;
            foreach(global::Microsoft.UI.Xaml.Markup.IXamlMetadataProvider xmp in OtherProviders)
            {
                xamlType = xmp.GetXamlType(type);
                if(xamlType != null)
                {
                    if(xamlType.IsConstructible)    // not Constructible means it might be a Return Type Stub
                    {
                        return xamlType;
                    }
                    foundXamlType = xamlType;
                }
            }
            return foundXamlType;
        }

        private object get_0_XamlControlsResources_UseCompactResources(object instance)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.XamlControlsResources)instance;
            return that.UseCompactResources;
        }
        private void set_0_XamlControlsResources_UseCompactResources(object instance, object Value)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.XamlControlsResources)instance;
            that.UseCompactResources = (global::System.Boolean)Value;
        }
        private object get_1_MicaBackdrop_Kind(object instance)
        {
            var that = (global::Microsoft.UI.Xaml.Media.MicaBackdrop)instance;
            return that.Kind;
        }
        private void set_1_MicaBackdrop_Kind(object instance, object Value)
        {
            var that = (global::Microsoft.UI.Xaml.Media.MicaBackdrop)instance;
            that.Kind = (global::Microsoft.UI.Composition.SystemBackdrops.MicaKind)Value;
        }
        private object get_2_MenuBar_Items(object instance)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.MenuBar)instance;
            return that.Items;
        }
        private object get_3_MenuBarItem_Items(object instance)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.MenuBarItem)instance;
            return that.Items;
        }
        private object get_4_MenuBarItem_Title(object instance)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.MenuBarItem)instance;
            return that.Title;
        }
        private void set_4_MenuBarItem_Title(object instance, object Value)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.MenuBarItem)instance;
            that.Title = (global::System.String)Value;
        }
        private object get_5_TreeViewNode_Children(object instance)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.TreeViewNode)instance;
            return that.Children;
        }
        private object get_6_TreeViewNode_Content(object instance)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.TreeViewNode)instance;
            return that.Content;
        }
        private void set_6_TreeViewNode_Content(object instance, object Value)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.TreeViewNode)instance;
            that.Content = (global::System.Object)Value;
        }
        private object get_7_TreeViewNode_Depth(object instance)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.TreeViewNode)instance;
            return that.Depth;
        }
        private object get_8_TreeViewNode_HasChildren(object instance)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.TreeViewNode)instance;
            return that.HasChildren;
        }
        private object get_9_TreeViewNode_HasUnrealizedChildren(object instance)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.TreeViewNode)instance;
            return that.HasUnrealizedChildren;
        }
        private void set_9_TreeViewNode_HasUnrealizedChildren(object instance, object Value)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.TreeViewNode)instance;
            that.HasUnrealizedChildren = (global::System.Boolean)Value;
        }
        private object get_10_TreeViewNode_IsExpanded(object instance)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.TreeViewNode)instance;
            return that.IsExpanded;
        }
        private void set_10_TreeViewNode_IsExpanded(object instance, object Value)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.TreeViewNode)instance;
            that.IsExpanded = (global::System.Boolean)Value;
        }
        private object get_11_TreeViewNode_Parent(object instance)
        {
            var that = (global::Microsoft.UI.Xaml.Controls.TreeViewNode)instance;
            return that.Parent;
        }

        private global::Microsoft.UI.Xaml.Markup.IXamlMember CreateXamlMember(string longMemberName)
        {
            global::DocGen.DocGen_XamlTypeInfo.XamlMember xamlMember = null;
            global::DocGen.DocGen_XamlTypeInfo.XamlUserType userType;

            switch (longMemberName)
            {
            case "Microsoft.UI.Xaml.Controls.XamlControlsResources.UseCompactResources":
                userType = (global::DocGen.DocGen_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.XamlControlsResources");
                xamlMember = new global::DocGen.DocGen_XamlTypeInfo.XamlMember(this, "UseCompactResources", "Boolean");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = get_0_XamlControlsResources_UseCompactResources;
                xamlMember.Setter = set_0_XamlControlsResources_UseCompactResources;
                break;
            case "Microsoft.UI.Xaml.Media.MicaBackdrop.Kind":
                userType = (global::DocGen.DocGen_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Media.MicaBackdrop");
                xamlMember = new global::DocGen.DocGen_XamlTypeInfo.XamlMember(this, "Kind", "Microsoft.UI.Composition.SystemBackdrops.MicaKind");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = get_1_MicaBackdrop_Kind;
                xamlMember.Setter = set_1_MicaBackdrop_Kind;
                break;
            case "Microsoft.UI.Xaml.Controls.MenuBar.Items":
                userType = (global::DocGen.DocGen_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.MenuBar");
                xamlMember = new global::DocGen.DocGen_XamlTypeInfo.XamlMember(this, "Items", "System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.MenuBarItem>");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = get_2_MenuBar_Items;
                xamlMember.SetIsReadOnly();
                break;
            case "Microsoft.UI.Xaml.Controls.MenuBarItem.Items":
                userType = (global::DocGen.DocGen_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.MenuBarItem");
                xamlMember = new global::DocGen.DocGen_XamlTypeInfo.XamlMember(this, "Items", "System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase>");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = get_3_MenuBarItem_Items;
                xamlMember.SetIsReadOnly();
                break;
            case "Microsoft.UI.Xaml.Controls.MenuBarItem.Title":
                userType = (global::DocGen.DocGen_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.MenuBarItem");
                xamlMember = new global::DocGen.DocGen_XamlTypeInfo.XamlMember(this, "Title", "String");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = get_4_MenuBarItem_Title;
                xamlMember.Setter = set_4_MenuBarItem_Title;
                break;
            case "Microsoft.UI.Xaml.Controls.TreeViewNode.Children":
                userType = (global::DocGen.DocGen_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
                xamlMember = new global::DocGen.DocGen_XamlTypeInfo.XamlMember(this, "Children", "System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.TreeViewNode>");
                xamlMember.Getter = get_5_TreeViewNode_Children;
                xamlMember.SetIsReadOnly();
                break;
            case "Microsoft.UI.Xaml.Controls.TreeViewNode.Content":
                userType = (global::DocGen.DocGen_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
                xamlMember = new global::DocGen.DocGen_XamlTypeInfo.XamlMember(this, "Content", "Object");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = get_6_TreeViewNode_Content;
                xamlMember.Setter = set_6_TreeViewNode_Content;
                break;
            case "Microsoft.UI.Xaml.Controls.TreeViewNode.Depth":
                userType = (global::DocGen.DocGen_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
                xamlMember = new global::DocGen.DocGen_XamlTypeInfo.XamlMember(this, "Depth", "Int32");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = get_7_TreeViewNode_Depth;
                xamlMember.SetIsReadOnly();
                break;
            case "Microsoft.UI.Xaml.Controls.TreeViewNode.HasChildren":
                userType = (global::DocGen.DocGen_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
                xamlMember = new global::DocGen.DocGen_XamlTypeInfo.XamlMember(this, "HasChildren", "Boolean");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = get_8_TreeViewNode_HasChildren;
                xamlMember.SetIsReadOnly();
                break;
            case "Microsoft.UI.Xaml.Controls.TreeViewNode.HasUnrealizedChildren":
                userType = (global::DocGen.DocGen_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
                xamlMember = new global::DocGen.DocGen_XamlTypeInfo.XamlMember(this, "HasUnrealizedChildren", "Boolean");
                xamlMember.Getter = get_9_TreeViewNode_HasUnrealizedChildren;
                xamlMember.Setter = set_9_TreeViewNode_HasUnrealizedChildren;
                break;
            case "Microsoft.UI.Xaml.Controls.TreeViewNode.IsExpanded":
                userType = (global::DocGen.DocGen_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
                xamlMember = new global::DocGen.DocGen_XamlTypeInfo.XamlMember(this, "IsExpanded", "Boolean");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = get_10_TreeViewNode_IsExpanded;
                xamlMember.Setter = set_10_TreeViewNode_IsExpanded;
                break;
            case "Microsoft.UI.Xaml.Controls.TreeViewNode.Parent":
                userType = (global::DocGen.DocGen_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
                xamlMember = new global::DocGen.DocGen_XamlTypeInfo.XamlMember(this, "Parent", "Microsoft.UI.Xaml.Controls.TreeViewNode");
                xamlMember.Getter = get_11_TreeViewNode_Parent;
                xamlMember.SetIsReadOnly();
                break;
            }
            return xamlMember;
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal partial class XamlSystemBaseType : global::Microsoft.UI.Xaml.Markup.IXamlType
    {
        string _fullName;
        global::System.Type _underlyingType;

        public XamlSystemBaseType(string fullName, 
            global::System.Type underlyingType)
        {
            _fullName = fullName;
            _underlyingType = underlyingType;
        }

        public string FullName { get { return _fullName; } }

        public global::System.Type UnderlyingType
        {
            get
            {
                return _underlyingType;
            }
        }

        virtual public global::Microsoft.UI.Xaml.Markup.IXamlType BaseType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Microsoft.UI.Xaml.Markup.IXamlMember ContentProperty { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Microsoft.UI.Xaml.Markup.IXamlMember GetMember(string name) { throw new global::System.NotImplementedException(); }
        virtual public bool IsArray { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsCollection { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsConstructible { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsDictionary { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsMarkupExtension { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsBindable { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsReturnTypeStub { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsLocalType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Microsoft.UI.Xaml.Markup.IXamlType ItemType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Microsoft.UI.Xaml.Markup.IXamlType KeyType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Microsoft.UI.Xaml.Markup.IXamlType BoxedType { get { throw new global::System.NotImplementedException(); } }
        virtual public object ActivateInstance() { throw new global::System.NotImplementedException(); }
        virtual public void AddToMap(object instance, object key, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void AddToVector(object instance, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void RunInitializer()   { throw new global::System.NotImplementedException(); }
        virtual public object CreateFromString(string input)   { throw new global::System.NotImplementedException(); }
    }
    
    internal delegate object Activator();
    internal delegate void StaticInitializer();
    internal delegate void AddToCollection(object instance, object item);
    internal delegate void AddToDictionary(object instance, object key, object item);
    internal delegate object CreateFromStringMethod(string args);
    internal delegate object BoxInstanceMethod(object instance);

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal partial class XamlUserType : global::DocGen.DocGen_XamlTypeInfo.XamlSystemBaseType
        , global::Microsoft.UI.Xaml.Markup.IXamlType
    {
        global::DocGen.DocGen_XamlTypeInfo.XamlTypeInfoProvider _provider;
        global::Microsoft.UI.Xaml.Markup.IXamlType _baseType;
        global::Microsoft.UI.Xaml.Markup.IXamlType _boxedType;
        bool _isArray;
        bool _isMarkupExtension;
        bool _isBindable;
        bool _isReturnTypeStub;
        bool _isLocalType;

        string _contentPropertyName;
        string _itemTypeName;
        string _keyTypeName;
        global::System.Collections.Generic.Dictionary<string, string> _memberNames;
        global::System.Collections.Generic.Dictionary<string, object> _enumValues;

        public XamlUserType(global::DocGen.DocGen_XamlTypeInfo.XamlTypeInfoProvider provider, string fullName, 
            global::System.Type fullType, global::Microsoft.UI.Xaml.Markup.IXamlType baseType)
            :base(fullName, fullType)
        {
            _provider = provider;
            _baseType = baseType;
        }

        // --- Interface methods ----

        override public global::Microsoft.UI.Xaml.Markup.IXamlType BaseType { get { return _baseType; } }
        override public bool IsArray { get { return _isArray; } }
        override public bool IsCollection { get { return (CollectionAdd != null); } }
        override public bool IsConstructible { get { return (Activator != null); } }
        override public bool IsDictionary { get { return (DictionaryAdd != null); } }
        override public bool IsMarkupExtension { get { return _isMarkupExtension; } }
        override public bool IsBindable { get { return _isBindable; } }
        override public bool IsReturnTypeStub { get { return _isReturnTypeStub; } }
        override public bool IsLocalType { get { return _isLocalType; } }
        override public global::Microsoft.UI.Xaml.Markup.IXamlType BoxedType { get { return _boxedType; } }

        override public global::Microsoft.UI.Xaml.Markup.IXamlMember ContentProperty
        {
            get { return _provider.GetMemberByLongName(_contentPropertyName); }
        }

        override public global::Microsoft.UI.Xaml.Markup.IXamlType ItemType
        {
            get { return _provider.GetXamlTypeByName(_itemTypeName); }
        }

        override public global::Microsoft.UI.Xaml.Markup.IXamlType KeyType
        {
            get { return _provider.GetXamlTypeByName(_keyTypeName); }
        }

        override public global::Microsoft.UI.Xaml.Markup.IXamlMember GetMember(string name)
        {
            if (_memberNames == null)
            {
                return null;
            }
            string longName;
            if (_memberNames.TryGetValue(name, out longName))
            {
                return _provider.GetMemberByLongName(longName);
            }
            return null;
        }

        override public object ActivateInstance()
        {
            return Activator(); 
        }

        override public void AddToMap(object instance, object key, object item) 
        {
            DictionaryAdd(instance, key, item);
        }

        override public void AddToVector(object instance, object item)
        {
            CollectionAdd(instance, item);
        }

        override public void RunInitializer() 
        {
            StaticInitializer();
        }

        override public object CreateFromString(string input)
        {
            if (BoxedType != null)
            {
                return BoxInstance(BoxedType.CreateFromString(input));
            }

            if (CreateFromStringMethod != null)
            {
                return this.CreateFromStringMethod(input);
            }
            else if (_enumValues != null)
            {
                long value = 0;

                string[] valueParts = input.Split(',');

                foreach (string valuePart in valueParts) 
                {
                    object partValue;
                    long enumFieldValue = 0;
                    try
                    {
                        if (_enumValues.TryGetValue(valuePart.Trim(), out partValue))
                        {
                            enumFieldValue = global::System.Convert.ToInt64(partValue);
                        }
                        else
                        {
                            try
                            {
                                enumFieldValue = global::System.Convert.ToInt64(valuePart.Trim());
                            }
                            catch( global::System.FormatException )
                            {
                                foreach( string key in _enumValues.Keys )
                                {
                                    if( string.Compare(valuePart.Trim(), key, global::System.StringComparison.OrdinalIgnoreCase) == 0 )
                                    {
                                        if( _enumValues.TryGetValue(key.Trim(), out partValue) )
                                        {
                                            enumFieldValue = global::System.Convert.ToInt64(partValue);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        value |= enumFieldValue; 
                    }
                    catch( global::System.FormatException )
                    {
                        throw new global::System.ArgumentException(input, FullName);
                    }
                }

                return global::System.Convert.ChangeType(value, global::System.Enum.GetUnderlyingType(this.UnderlyingType));
            }
            throw new global::System.ArgumentException(input, FullName);
        }

        // --- End of Interface methods

        public Activator Activator { get; set; }
        public StaticInitializer StaticInitializer { get; set; }
        public AddToCollection CollectionAdd { get; set; }
        public AddToDictionary DictionaryAdd { get; set; }
        public CreateFromStringMethod CreateFromStringMethod {get; set; }
        public BoxInstanceMethod BoxInstance {get; set; }

        public void SetContentPropertyName(string contentPropertyName)
        {
            _contentPropertyName = contentPropertyName;
        }

        public void SetIsArray()
        {
            _isArray = true; 
        }

        public void SetIsMarkupExtension()
        {
            _isMarkupExtension = true;
        }

        public void SetIsBindable()
        {
            _isBindable = true;
        }

        public void SetIsReturnTypeStub()
        {
            _isReturnTypeStub = true;
        }

        public void SetIsLocalType()
        {
            _isLocalType = true;
        }

        public void SetItemTypeName(string itemTypeName)
        {
            _itemTypeName = itemTypeName;
        }

        public void SetKeyTypeName(string keyTypeName)
        {
            _keyTypeName = keyTypeName;
        }

        public void SetBoxedType(global::Microsoft.UI.Xaml.Markup.IXamlType boxedType)
        {
            _boxedType = boxedType;
        }

        public object BoxType<T>(object instance) where T: struct
        {
            T unwrapped = (T)instance;
            return new global::System.Nullable<T>(unwrapped);
        }

        public void AddMemberName(string shortName)
        {
            if(_memberNames == null)
            {
                _memberNames =  new global::System.Collections.Generic.Dictionary<string,string>();
            }
            _memberNames.Add(shortName, FullName + "." + shortName);
        }

        public void AddEnumValue(string name, object value)
        {
            if (_enumValues == null)
            {
                _enumValues = new global::System.Collections.Generic.Dictionary<string, object>();
            }
            _enumValues.Add(name, value);
        }
    }

    internal delegate object Getter(object instance);
    internal delegate void Setter(object instance, object value);

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2511")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal partial class XamlMember : global::Microsoft.UI.Xaml.Markup.IXamlMember
    {
        global::DocGen.DocGen_XamlTypeInfo.XamlTypeInfoProvider _provider;
        string _name;
        bool _isAttachable;
        bool _isDependencyProperty;
        bool _isReadOnly;

        string _typeName;
        string _targetTypeName;

        public XamlMember(global::DocGen.DocGen_XamlTypeInfo.XamlTypeInfoProvider provider, string name, string typeName)
        {
            _name = name;
            _typeName = typeName;
            _provider = provider;
        }

        public string Name { get { return _name; } }

        public global::Microsoft.UI.Xaml.Markup.IXamlType Type
        {
            get { return _provider.GetXamlTypeByName(_typeName); }
        }

        public void SetTargetTypeName(string targetTypeName)
        {
            _targetTypeName = targetTypeName;
        }
        public global::Microsoft.UI.Xaml.Markup.IXamlType TargetType
        {
            get { return _provider.GetXamlTypeByName(_targetTypeName); }
        }

        public void SetIsAttachable() { _isAttachable = true; }
        public bool IsAttachable { get { return _isAttachable; } }

        public void SetIsDependencyProperty() { _isDependencyProperty = true; }
        public bool IsDependencyProperty { get { return _isDependencyProperty; } }

        public void SetIsReadOnly() { _isReadOnly = true; }
        public bool IsReadOnly { get { return _isReadOnly; } }

        public Getter Getter { get; set; }
        public object GetValue(object instance)
        {
            if (Getter != null)
                return Getter(instance);
            else
                throw new global::System.InvalidOperationException("GetValue");
        }

        public Setter Setter { get; set; }
        public void SetValue(object instance, object value)
        {
            if (Setter != null)
                Setter(instance, value);
            else
                throw new global::System.InvalidOperationException("SetValue");
        }
    }
}


```

---

## DocGen\Services

<a id='services'></a>

## FILE 17: IWindowService.cs

<a id='iwindowservice'></a>

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

## FILE 18: LanguageService.cs

<a id='languageservice'></a>

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

## FILE 19: LoadAppSettingsService.cs

<a id='loadappsettingsservice'></a>

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

            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFilePath, json);
        }
    }
}

```

---

## FILE 20: ResourceService.cs

<a id='resourceservice'></a>

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

## DocGen\ViewModels

<a id='viewmodels'></a>

## FILE 21: DocGenViewModel.cs

<a id='docgenviewmodel'></a>

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

```

---

