using System.Collections.ObjectModel;
using System.Windows.Input;
using Trackademia.Services;

public class DashboardViewModel : BindableObject
{
    private readonly UserService _userService;
    private bool _isLoadingData = false;

    private string _currentDate;
    public string CurrentDate
    {
        get => _currentDate;
        set
        {
            _currentDate = value;
            OnPropertyChanged();
            Console.WriteLine($"CurrentDate updated: {CurrentDate}");
        }
    }

    private int _studentCount;
    public int StudentCount
    {
        get => _studentCount;
        set
        {
            _studentCount = value;
            OnPropertyChanged();
            Console.WriteLine($"StudentCount updated: {StudentCount}");
        }
    }

    private ObservableCollection<KeyValuePair<string, int>> _studentCountByProgram;
    public ObservableCollection<KeyValuePair<string, int>> StudentCountByProgram
    {
        get => _studentCountByProgram;
        set
        {
            _studentCountByProgram = value;
            OnPropertyChanged();
            Console.WriteLine("StudentCountByProgram updated.");
        }
    }

    public ICommand LoadDashboardDataCommand { get; }

    public DashboardViewModel()
    {
        Console.WriteLine($"DashboardViewModel instantiated at {DateTime.Now}");
        _userService = new UserService();
        CurrentDate = $"Today is {DateTime.Now:MMMM dd, yyyy}";

        StudentCountByProgram = new ObservableCollection<KeyValuePair<string, int>>();

        LoadDashboardDataCommand = new Command(async () => await LoadDashboardData());

        Console.WriteLine("Initial data load started.");
        LoadDashboardDataCommand.Execute(null); // Trigger the initial load
    }

    private async Task LoadDashboardData()
    {
        if (_isLoadingData) return; // Prevent redundant calls
        _isLoadingData = true;

        try
        {
            Console.WriteLine("Loading dashboard data...");
            StudentCount = await _userService.GetStudentCountAsync();

            var studentCountByProgramData = await _userService.GetStudentCountByProgramAsync();

            Console.WriteLine("Clearing StudentCountByProgram...");
            StudentCountByProgram.Clear();
            foreach (var item in studentCountByProgramData)
            {
                // Check if item already exists in the list to avoid duplication
                if (!StudentCountByProgram.Contains(item))
                {
                    StudentCountByProgram.Add(item);
                }
            }

            Console.WriteLine("Dashboard data loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading dashboard data: {ex.Message}");
        }
        finally
        {
            _isLoadingData = false;
        }
    }
}