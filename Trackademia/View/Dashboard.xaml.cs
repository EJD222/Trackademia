using System;
using Trackademia.ViewModel;

namespace Trackademia.View
{
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();

            // Set the view model as the BindingContext
            BindingContext = new DashboardViewModel();
        }

        private async void OnViewStudentsButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//UserPage");
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
