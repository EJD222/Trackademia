namespace Trackademia.View;
using Trackademia.ViewModel;

[QueryProperty(nameof(Id), "id")]
public partial class StudentDetailsPage : ContentPage
{
	public StudentDetailsPage()
	{
		InitializeComponent();
        BindingContext = new StudentDetailsViewModel();
    }
}