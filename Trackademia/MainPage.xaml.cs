using Trackademia.View;

namespace Trackademia
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }
        private async void ClickedViewUser(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//UserPage");
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }

}