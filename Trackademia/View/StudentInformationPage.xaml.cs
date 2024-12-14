using Trackademia.ViewModel;

namespace Trackademia.View;

[QueryProperty(nameof(Id), "id")]
public partial class StudentInformationPage : ContentPage
{
	public StudentInformationPage()
	{
		InitializeComponent();
        BindingContext = new StudentDetailsViewModel();
    }
}