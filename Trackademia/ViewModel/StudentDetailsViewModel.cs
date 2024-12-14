using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Trackademia.Model;
using Trackademia.Services;

namespace Trackademia.ViewModel
{
    [QueryProperty(nameof(Id), "Id")]
    public class StudentDetailsViewModel : BindableObject
    {
        private readonly UserService _userService;
        private User _student;
        private int _id;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                LoadStudentDetails(_id); // Trigger loading when Id is set
            }
        }

        public User Student
        {
            get => _student;
            set
            {
                _student = value;
                OnPropertyChanged();
            }
        }
        public ICommand ViewStudentInformationCommand { get; }
        public ICommand ViewAttendanceCommand { get; }
        public ICommand ViewAcademicHistoryCommand { get; }

        public StudentDetailsViewModel()
        {
            _userService = new UserService();
            ViewStudentInformationCommand = new Command<int>(GoToStudentInformationPage);
            ViewAttendanceCommand = new Command<int>(GoToAttendancePage);
            ViewAcademicHistoryCommand = new Command<int>(GoToAcademicHistoryPage);
        }

        public async Task LoadStudentDetails(int id)
        {
            try
            {
                var student = await _userService.GetStudentAsync(id);
                Student = student;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching student details: {ex.Message}");
            }
        }
        private async void GoToStudentInformationPage(int id)
        {
            await Shell.Current.GoToAsync("StudentInformationPage", new Dictionary<string, object>
            {
                { "Id", id }
            });
        }
        private async void GoToAttendancePage(int id)
        {
            await Shell.Current.GoToAsync("AttendancePage", new Dictionary<string, object>
            {
                { "Id", id }
            });
        }

        private async void GoToAcademicHistoryPage(int id)
        {
            await Shell.Current.GoToAsync("AcademicHistoryPage", new Dictionary<string, object>
            {
                { "Id", id }
            });
        }

    }
}
