namespace MAUI_PASSWORD_MANAGER
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.PasswordPage), typeof(Views.PasswordPage));
        }
    }
}