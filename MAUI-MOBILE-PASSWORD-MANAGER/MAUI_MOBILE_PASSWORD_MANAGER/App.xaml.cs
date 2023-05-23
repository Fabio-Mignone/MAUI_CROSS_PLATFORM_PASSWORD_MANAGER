namespace MAUI_MOBILE_PASSWORD_MANAGER;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
