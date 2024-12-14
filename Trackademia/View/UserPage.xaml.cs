namespace Trackademia.View;
using Trackademia.ViewModel;
public partial class UserPage : ContentPage
{
	public UserPage()
	{
		InitializeComponent();
        BindingContext = new UserViewModel();
    }
    private async void OnDashboardButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Dashboard");
    }
}