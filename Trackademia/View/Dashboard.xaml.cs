namespace Trackademia.View;

public partial class Dashboard : ContentPage
{
	public Dashboard()
	{
		InitializeComponent();
	}
    private async void OnViewUserButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//UserPage");
    }

    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }
}