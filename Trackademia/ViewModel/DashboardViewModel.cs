using System.Collections.ObjectModel;
using System.Windows.Input;
using Trackademia.Services;

public class DashboardViewModel : BindableObject
{
    private readonly UserService _userService;
    private bool _isLoadingData = false;
    private string _currentDate;
    private int _studentCount;
    private string _chartData;
    private bool _isLoading;
    private bool _hasError;
    private string _errorMessage;
    private bool _isRefreshing;
    private ObservableCollection<KeyValuePair<string, int>> _studentCountByProgram;

    // Properties for loading and error states
    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public bool HasError
    {
        get => _hasError;
        set
        {
            _hasError = value;
            OnPropertyChanged();
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set
        {
            _isRefreshing = value;
            OnPropertyChanged();
        }
    }

    // Chart data property
    public string ChartData
    {
        get => _chartData;
        set
        {
            _chartData = value;
            OnPropertyChanged();
        }
    }

    public string CurrentDate
    {
        get => _currentDate;
        set
        {
            _currentDate = value;
            OnPropertyChanged();
        }
    }

    public int StudentCount
    {
        get => _studentCount;
        set
        {
            _studentCount = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<KeyValuePair<string, int>> StudentCountByProgram
    {
        get => _studentCountByProgram;
        set
        {
            _studentCountByProgram = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoadDashboardDataCommand { get; private set; }
    public ICommand RefreshCommand { get; private set; }

    public DashboardViewModel()
    {
        _userService = new UserService();
        CurrentDate = $"Today is {DateTime.Now:MMMM dd, yyyy}";
        StudentCountByProgram = new ObservableCollection<KeyValuePair<string, int>>();

        // Subscribe to the UsersChanged event
        _userService.UsersChanged += async (s, e) => await RefreshData();

        // Initialize commands
        LoadDashboardDataCommand = new Command(async () => await LoadDashboardData());
        RefreshCommand = new Command(async () => await RefreshData());

        // Initial load
        LoadDashboardDataCommand.Execute(null);
    }

    // Make sure to unsubscribe when the ViewModel is disposed
    ~DashboardViewModel()
    {
        if (_userService != null)
        {
            _userService.UsersChanged -= async (s, e) => await RefreshData();
        }
    }

    private async Task RefreshData()
    {
        IsRefreshing = true;
        HasError = false;
        await LoadDashboardData();
        IsRefreshing = false;
    }

    private async Task LoadDashboardData()
    {
        if (_isLoadingData) return;

        _isLoadingData = true;
        IsLoading = true;
        HasError = false;
        ErrorMessage = string.Empty;

        try
        {
            // Add slight delay for loading animation
            await Task.Delay(300);

            // Load student count
            StudentCount = await _userService.GetStudentCountAsync();

            // Load program distribution data
            var studentCountByProgramData = await _userService.GetStudentCountByProgramAsync();
            StudentCountByProgram.Clear();

            foreach (var item in studentCountByProgramData)
            {
                StudentCountByProgram.Add(item);
            }

            // Prepare chart data
            var chartDataObj = StudentCountByProgram.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value
            );
            ChartData = System.Text.Json.JsonSerializer.Serialize(chartDataObj);
        }
        catch (Exception ex)
        {
            HasError = true;
            ErrorMessage = "Unable to load dashboard data. Please try again.";
            Console.WriteLine($"Error loading dashboard data: {ex.Message}");
        }
        finally
        {
            _isLoadingData = false;
            IsLoading = false;
            IsRefreshing = false;
        }
    }
}