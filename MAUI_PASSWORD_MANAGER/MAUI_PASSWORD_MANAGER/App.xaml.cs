using MAUI_PASSWORD_MANAGER.Views;

namespace MAUI_PASSWORD_MANAGER
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}