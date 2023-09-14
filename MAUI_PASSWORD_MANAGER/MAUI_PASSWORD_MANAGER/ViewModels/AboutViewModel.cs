using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace MAUI_PASSWORD_MANAGER.ViewModel
{
    internal class AboutViewModel
    {
        public string Title => "MAUI_PASSWORD_MANAGER";
        public string Version => "Version: " + AppInfo.VersionString;
        public string MoreInfoUrl => "https://github.com/Fabio-Mignone/MAUI_PASSWORD_MANAGER";
        public string Message => "This app is written in XAML and C# with .NET MAUI. by Fabio Mignone to learn more visit my github profile";
        public ICommand ShowMoreInfoCommand { get; }

        public AboutViewModel()
        {
            ShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);
        }

        async Task ShowMoreInfo() =>
            await Launcher.Default.OpenAsync(MoreInfoUrl);
    }
}
