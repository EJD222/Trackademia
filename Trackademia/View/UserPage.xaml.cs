namespace Trackademia.View;
using Trackademia.ViewModel;
public partial class UserPage : ContentPage
{
	public UserPage()
	{
		InitializeComponent();
        BindingContext = new UserViewModel();
    }
}