using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackademia.Model;
using Trackademia.Services;
using System.Windows.Input;
using Trackademia.View;

namespace Trackademia.ViewModel
{
    public class UserViewModel : BindableObject
    {
        private readonly UserService _userService;
        public ObservableCollection<User> Users { get; set; }
        private ObservableCollection<AcademicProgram> _programs;
        public ObservableCollection<AcademicProgram> Programs
        {
            get => _programs;
            set
            {
                _programs = value;
                OnPropertyChanged();
            }
        }


        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
                UpdateEntryField();
            }
        }

        private AcademicProgram _selectedProgram;
        public AcademicProgram SelectedProgram
        {
            get => _selectedProgram;
            set
            {
                _selectedProgram = value;
                OnPropertyChanged();
            }
        }

        private string _nameInput;
        public string NameInput
        {
            get => _nameInput;
            set
            {
                _nameInput = value;
                OnPropertyChanged();
            }
        }

        private string _emailInput;
        public string EmailInput
        {
            get => _emailInput;
            set { _emailInput = value; OnPropertyChanged(); }
        }


        private string _studentIdInput;
        public string StudentIdInput
        {
            get => _studentIdInput;
            set { _studentIdInput = value; OnPropertyChanged(); }
        }

        private string _addressInput;
        public string AddressInput
        {
            get => _addressInput;
            set { _addressInput = value; OnPropertyChanged(); }
        }

        private DateTime _birthdateInput;
        public DateTime BirthdateInput
        {
            get => _birthdateInput;
            set { _birthdateInput = value; OnPropertyChanged(); }
        }

        private bool _isStudentModalVisible;
        public bool IsStudentModalVisible { get => _isStudentModalVisible; set { _isStudentModalVisible = value; OnPropertyChanged(); } }

        private void ClearInput()
        {
            NameInput = string.Empty;
            EmailInput = string.Empty;
            StudentIdInput = string.Empty;
            AddressInput = string.Empty;
            BirthdateInput = DateTime.Today;
            SelectedProgram = null;
        }

        private void UpdateEntryField()
        {
            if (SelectedUser != null)
            {
                NameInput = SelectedUser.Name;
                EmailInput = SelectedUser.Email;
                StudentIdInput = SelectedUser.StudentId;
                AddressInput = SelectedUser.Address;
                BirthdateInput = SelectedUser.Birthdate;
                SelectedProgram = Programs.FirstOrDefault(p => p.ID == SelectedUser.Program);
            }
            else
            {
                ClearInput();
            }
        }
        public UserViewModel()
        {
            _userService = new UserService();
            Users = new ObservableCollection<User>();
            Programs = new ObservableCollection<AcademicProgram>();

            LoadUserCommand = new Command(async () => await LoadUsers());
            //AddUserCommand = new Command(async () => await AddUser());
            DeleteUserCommand = new Command(async () => await DeleteUser());
            //UpdateUserCommand = new Command(async () => await UpdateUser());
            ViewDetailsCommand = new Command<int>(ViewDetails);
            OpenAddStudentModalCommand = new Command(OpenAddModal);
            OpenUpdateStudentModalCommand = new Command(OpenUpdateModal);
            CloseStudentModalCommand = new Command(() => IsStudentModalVisible = false);
            SaveStudentCommand = new Command(async () => await SaveStudent());

            Task.Run(async () => await LoadPrograms());

        }

        public ICommand ViewDetailsCommand { get; }
        public ICommand LoadUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand OpenAddStudentModalCommand { get; }
        public ICommand OpenUpdateStudentModalCommand { get; }
        public ICommand CloseStudentModalCommand { get; }
        public ICommand SaveStudentCommand { get; }

        private async void ViewDetails(int id)
        {
            await Shell.Current.GoToAsync($"StudentDetailsPage", new Dictionary<string, object>
            {
                { "Id", id }
            });
        }

        private async Task LoadUsers()
        {
            var users = await _userService.GetUserAsync();
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }

        }
        private async Task LoadPrograms()
        {
            try
            {
                var programs = await _userService.GetProgramsAsync();

                Programs = new ObservableCollection<AcademicProgram>(programs);



                Console.WriteLine($"Programs loaded: {Programs.Count}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading programs: {ex.Message}");
            }
        }

        private void OpenAddModal()
        {
            ClearInput();
            IsStudentModalVisible = true;
        }

        private void OpenUpdateModal()
        {
            if (SelectedUser != null) IsStudentModalVisible = true;
        }

        private async Task SaveStudent()
        {
            if (SelectedUser == null) // Add
            {
                var newUser = new User
                {
                    Name = NameInput,
                    Email = EmailInput,
                    StudentId = StudentIdInput,
                    Address = AddressInput,
                    Birthdate = BirthdateInput,
                    Program = SelectedProgram?.ID ?? 0
                };
                await _userService.AddUsersAsync(newUser);
            }
            else // Update
            {
                SelectedUser.Name = NameInput;
                SelectedUser.Email = EmailInput;
                SelectedUser.StudentId = StudentIdInput;
                SelectedUser.Address = AddressInput;
                SelectedUser.Birthdate = BirthdateInput;
                SelectedUser.Program = SelectedProgram?.ID ?? 0;
                await _userService.UpdateUsersAsync(SelectedUser);
            }

            await LoadUsers();
            IsStudentModalVisible = false;
        }

        //private async Task AddUser()
        //{
        //    if (!string.IsNullOrWhiteSpace(NameInput) && SelectedProgram != null)
        //    {
        //        var newUser = new User
        //        {
        //            Name = NameInput,
        //            Email = EmailInput,
        //            StudentId = StudentIdInput,
        //            Address = AddressInput,
        //            Birthdate = BirthdateInput,
        //            Program = SelectedProgram.ID
        //        };

        //        var result = await _userService.AddUsersAsync(newUser);

        //        if (result.Equals("User added successfully"))
        //        {
        //            await LoadUsers();
        //            ClearInput();
        //        }
        //    }
        //}

        private async Task DeleteUser()
        {
            if (SelectedUser != null)
            {
                // Show confirmation dialog
                bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
                    "Delete Confirmation",
                    $"Are you sure you want to delete {SelectedUser.Name}?",
                    "Yes",
                    "No"
                );

                if (isConfirmed)
                {
                    try
                    {
                        var result = await _userService.DeleteUsersAsync(SelectedUser.ID);
                        await LoadUsers();
                        ClearInput();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting user: {ex.Message}");
                        await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "An error occurred while trying to delete the student.",
                            "OK"
                        );
                    }
                }
            }
            else
            {
                // Show a message if no user is selected
                await Application.Current.MainPage.DisplayAlert(
                    "No Selection",
                    "Please select a student to delete.",
                    "OK"
                );
            }
        }

        //private async Task UpdateUser()
        //{
        //    if (SelectedUser != null && SelectedProgram != null)
        //    {
        //        SelectedUser.Name = NameInput;
        //        SelectedUser.Email = EmailInput;
        //        SelectedUser.StudentId = StudentIdInput;
        //        SelectedUser.Address = AddressInput;
        //        SelectedUser.Birthdate = BirthdateInput;
        //        SelectedUser.Program = SelectedProgram.ID;

        //        var result = await _userService.UpdateUsersAsync(SelectedUser);
        //        if (result.Equals("User updated successfully"))
        //        {
        //            await LoadUsers();
        //        }
        //    }
        //}

        private void UpdateEntryFields()
        {
            if (SelectedUser != null)
            {
                NameInput = SelectedUser.Name;
                EmailInput = SelectedUser.Email;
                StudentIdInput = SelectedUser.StudentId;
                AddressInput = SelectedUser.Address;
                BirthdateInput = SelectedUser.Birthdate;
                SelectedProgram = Programs.FirstOrDefault(p => p.ID == SelectedUser.Program);
            }
            else
            {
                NameInput = string.Empty;
                EmailInput = string.Empty;
                StudentIdInput = string.Empty;
                AddressInput = string.Empty;
                BirthdateInput = DateTime.Today;
                SelectedProgram = null;
            }
        }

    }
}
