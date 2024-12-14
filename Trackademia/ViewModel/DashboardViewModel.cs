using System.Windows.Input;
using Trackademia.Services;
public class DashboardViewModel : BindableObject
{
    private readonly UserService _userService;

    private string _currentDate;
    public string CurrentDate
    {
        get => _currentDate;
        set
        {
            _currentDate = value;
            OnPropertyChanged();
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
        }
    }

    public ICommand LoadDashboardDataCommand { get; }

    public DashboardViewModel()
    {
        _userService = new UserService(); // Ensure you create the UserService instance here
        CurrentDate = $"Today is {DateTime.Now:MMMM dd, yyyy}";
        LoadDashboardDataCommand = new Command(async () => await LoadDashboardData());

        Task.Run(async () => await LoadDashboardData());

        // Subscribe to the UsersChanged event
        _userService.UsersChanged += async (sender, e) =>
        {
            await LoadDashboardData();
        };
    }

    private async Task LoadDashboardData()
    {
        try
        {
            StudentCount = await _userService.GetStudentCountAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading dashboard data: {ex.Message}");
        }
    }
}

