using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackademia.Model;
using Trackademia.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Trackademia.ViewModel
{
    [QueryProperty(nameof(Id), "Id")]
    public class AcademicHistoryViewModel : BindableObject
    {
        private readonly UserService _userService;
        private ObservableCollection<AcademicHistory> _academicHistoryRecords;
        private ObservableCollection<AcademicProgram> _programs;
        private string _studentName;
        private string _studentNumber;
        private int _id;
        private bool _isAddRecordModalVisible;


        public ObservableCollection<AcademicHistory> AcademicHistoryRecords
        {
            get => _academicHistoryRecords;
            set
            {
                _academicHistoryRecords = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<AcademicProgram> Programs
        {
            get => _programs;
            set
            {
                _programs = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Levels { get; set; } = new ObservableCollection<string>
        {
            "1st Year", "2nd Year", "3rd Year", "4th Year"
        };

        public ObservableCollection<string> Semesters { get; set; } = new ObservableCollection<string>
        {
            "1st Sem", "2nd Sem"
        };

        public bool IsAddRecordModalVisible
        {
            get => _isAddRecordModalVisible;
            set
            {
                _isAddRecordModalVisible = value;
                OnPropertyChanged();
            }
        }


        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                LoadAcademicHistoryRecords(_id);
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

        public AcademicProgram SelectedProgram { get; set; }
        public string SelectedLevel { get; set; }
        public string SelectedSemester { get; set; }
        public string SchoolYearInput { get; set; }
        public string GradeInput { get; set; }

        public ICommand OpenAddRecordModalCommand { get; }
        public ICommand CloseAddRecordModalCommand { get; }
        public ICommand AddAcademicRecordCommand { get; }

        public AcademicHistoryViewModel()
        {
            _userService = new UserService();
            AcademicHistoryRecords = new ObservableCollection<AcademicHistory>();
            Programs = new ObservableCollection<AcademicProgram>();

            OpenAddRecordModalCommand = new Command(() => IsAddRecordModalVisible = true);
            CloseAddRecordModalCommand = new Command(() => IsAddRecordModalVisible = false);
            AddAcademicRecordCommand = new Command(async () => await AddAcademicRecord());

            Task.Run(async () => await LoadPrograms());
        }

        public async Task LoadAcademicHistoryRecords(int id)
        {
            try
            {
                var records = await _userService.GetAcademicHistoryByStudentIdAsync(id);
                if (records != null && records.Count > 0)
                {
                    AcademicHistoryRecords = new ObservableCollection<AcademicHistory>(records);

                    // Set StudentName and StudentNumber from the first record
                    StudentName = records[0].StudentName;
                    StudentNumber = records[0].StudentNumber;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching academic history records: {ex.Message}");
            }
        }

        public async Task LoadPrograms()
        {
            try
            {
                var programs = await _userService.GetProgramsAsync();
                Programs = new ObservableCollection<AcademicProgram>(programs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading programs: {ex.Message}");
            }
        }

        private async Task AddAcademicRecord()
        {
            if (SelectedProgram == null || string.IsNullOrWhiteSpace(SelectedLevel) ||
                string.IsNullOrWhiteSpace(SelectedSemester) || string.IsNullOrWhiteSpace(SchoolYearInput) ||
                string.IsNullOrWhiteSpace(GradeInput))
            {
                Console.WriteLine("Please fill out all fields.");
                return;
            }

            try
            {
                var record = new AcademicHistory
                {
                    StudentID = Id,
                    Program = SelectedProgram.ID,
                    Level = SelectedLevel,
                    Semester = SelectedSemester,
                    SchoolYear = SchoolYearInput,
                    Grade = int.Parse(GradeInput)
                };

                var response = await _userService.AddAcademicHistoryAsync(record);

                if (response.Contains("successfully"))
                {
                    IsAddRecordModalVisible = false;
                    await LoadAcademicHistoryRecords(Id); // Refresh the records
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding academic record: {ex.Message}");
            }
        }
    }
}
