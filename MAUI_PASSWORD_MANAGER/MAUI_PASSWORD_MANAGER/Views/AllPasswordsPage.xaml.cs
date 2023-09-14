namespace MAUI_PASSWORD_MANAGER.Views;
public partial class AllPasswordsPage : ContentPage
{
    public AllPasswordsPage()
    {
        InitializeComponent();
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }
}