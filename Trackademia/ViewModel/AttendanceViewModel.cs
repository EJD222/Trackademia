using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trackademia.Model;
using Trackademia.Services;

namespace Trackademia.ViewModel
{
    [QueryProperty(nameof(Id), "Id")]
    public class AttendanceViewModel : BindableObject
    {
        private readonly UserService _userService;
        private ObservableCollection<Attendance> _attendanceRecords;
        private string _studentName;
        private string _studentNumber;
        private int _id;
        private DateTime _selectedDate;

        public ObservableCollection<Attendance> AttendanceRecords
        {
            get => _attendanceRecords;
            set
            {
                _attendanceRecords = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                LoadAttendanceRecords(_id); // Automatically load records when ID is set
            }
        }

        public string StudentName
        {
            get => _studentName;
            set
            {
                _studentName = value;
                OnPropertyChanged();
            }
        }

        public string StudentNumber
        {
            get => _studentNumber;
            set
            {
                _studentNumber = value;
                OnPropertyChanged();
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        private Attendance _selectedAttendance;
        public Attendance SelectedAttendance
        {
            get => _selectedAttendance;
            set
            {
                _selectedAttendance = value;
                OnPropertyChanged();
            }
        }

        public ICommand MarkPresentCommand { get; }
        public ICommand MarkAbsentCommand { get; }
        private async Task MarkAttendance(string status)
        {
            try
            {
                var attendance = new Attendance
                {
                    Date = SelectedDate, // SelectedDate is already DateTime
                    Status = status,
                    StudentID = Id // Use the ID of the current student
                };

                var result = await _userService.AddAttendanceAsync(attendance);

                if (result.Contains("Attendance record added successfully"))
                {
                    // Refresh the attendance list after adding a new record
                    await LoadAttendanceRecords(Id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding attendance: {ex.Message}");
            }
        }

        public AttendanceViewModel()
        {
            _userService = new UserService();
            AttendanceRecords = new ObservableCollection<Attendance>();

            MarkPresentCommand = new Command(async () => await MarkAttendance("Present"));
            MarkAbsentCommand = new Command(async () => await MarkAttendance("Absent"));

            SelectedDate = DateTime.Today; // Default to today
        }

        //Loading Attendance for Student
        public async Task LoadAttendanceRecords(int id)
        {
            try
            {
                var records = await _userService.GetAttendanceByStudentIdAsync(id);
                if (records != null && records.Count > 0)
                {
                    AttendanceRecords = new ObservableCollection<Attendance>(records);

                    // Set StudentName and StudentNumber from the first record
                    StudentName = records[0].StudentName;
                    StudentNumber = records[0].StudentNumber;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching attendance records: {ex.Message}");
            }
        }

        //Add Attendance for Student
        private async Task AddAttendanceRecord(string status)
        {
            try
            {
                var attendance = new Attendance
                {
                    Date = SelectedDate,
                    Status = status,
                    StudentID = Id
                };

                var response = await _userService.AddAttendanceAsync(attendance);
                if (response.Contains("successfully"))
                {
                    // Reload attendance records to reflect the new entry
                    await LoadAttendanceRecords(Id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding attendance record: {ex.Message}");
            }
        }

    }
}
