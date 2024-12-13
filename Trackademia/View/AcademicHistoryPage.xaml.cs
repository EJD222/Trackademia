namespace Trackademia.View;
using Trackademia.ViewModel;

[QueryProperty(nameof(Id), "id")]
public partial class AcademicHistoryPage : ContentPage
{
    public int Id { get; set; }

    public AcademicHistoryPage()
	{
		InitializeComponent();
        BindingContext = new AcademicHistoryViewModel();
    }
}