using Trackademia.ViewModel;

namespace Trackademia.View
{
    public partial class UserPage : ContentPage
    {
        private UserViewModel _viewModel;

        public UserPage()
        {
            InitializeComponent();
            _viewModel = new UserViewModel();
            BindingContext = _viewModel;
        }

        private async void OnFilterProgramChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            // Determine the program value based on selected index
            int programValue = selectedIndex switch
            {
                1 => 1, // Bachelor of Science in Information Technology
                2 => 2, // Bachelor of Science in Computer Science
                3 => 3, // Bachelor of Multimedia Arts
                _ => 0  // All Programs
            };

            if (programValue == 0)
            {
                // Load all students if "All Programs" is selected
                await _viewModel.LoadUsers();
            }
            else
            {
                // Filter students by the selected program value
                await _viewModel.FilterUsersByProgramValue(programValue);
            }
        }

        private async void OnDashboardButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Dashboard");
        }
    }
}
