namespace Trackademia.View;
using Trackademia.ViewModel;

[QueryProperty(nameof(Id), "id")]
public partial class AttendancePage : ContentPage
{
    public int Id { get; set; }

    public AttendancePage()
	{
		InitializeComponent();
        BindingContext = new AttendanceViewModel();
    }
}