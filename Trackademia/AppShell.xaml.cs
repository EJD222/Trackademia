using Trackademia.View;
namespace Trackademia
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("StudentDetailsPage", typeof(StudentDetailsPage));
            Routing.RegisterRoute("StudentInformationPage", typeof(StudentInformationPage));
            Routing.RegisterRoute("AttendancePage", typeof(AttendancePage));
            Routing.RegisterRoute("AcademicHistoryPage", typeof(AcademicHistoryPage));

        }
    }
}
