using DocGen.Services;
using DocGen.ViewModels;
using Microsoft.UI.Xaml;
using System;
using System.IO;

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
            string iconPath = Path.Combine(AppContext.BaseDirectory, "Assets", "icon.ico");
            appWindow.SetIcon(iconPath);
            appWindow.Resize(new Windows.Graphics.SizeInt32(450, 300));

        }

    }
}
