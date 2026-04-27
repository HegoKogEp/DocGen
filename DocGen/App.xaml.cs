using Microsoft.UI.Xaml;

namespace DocGen
{
    public partial class App : Application
    {
        private Window? _window;

        public App()
        {
            InitializeComponent();
            // Не трогаем язык здесь – он будет установлен позже
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();
            _window.Activate();
        }
    }
}