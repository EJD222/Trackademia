using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackademia.Model;
using System.Net.Http.Json;

namespace Trackademia.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost/trackademia/";

        public UserService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<User>> GetUserAsync()
        {
            var response =
                await _httpClient.GetFromJsonAsync<List<User>>($"{BaseUrl}get_user.php");
            return response ?? new List<User>();
        }

        //Add user
        public async Task<string> AddUsersAsync(User user)
        {
            var response =
                await _httpClient.PostAsJsonAsync($"{BaseUrl}add_user.php", user);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        //Update User
        public async Task<string> UpdateUsersAsync(User user)
        {
            var response =
                await _httpClient.PostAsJsonAsync($"{BaseUrl}update_user.php", user);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        //Delete User
        public async Task<string> DeleteUsersAsync(int userId)
        {
            var response =
                await _httpClient.PostAsJsonAsync($"{BaseUrl}delete_user.php", new { id = userId });
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        //List Program Name
        public async Task<List<AcademicProgram>> GetProgramsAsync()
        {
            var response =
                await _httpClient.GetFromJsonAsync<List<AcademicProgram>>($"{BaseUrl}get_programs.php");
            return response ?? new List<AcademicProgram>();
        }

        //Get Selected Student Details
        public async Task<User> GetStudentAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<User>($"{BaseUrl}get_student.php?id={id}");
            return response ?? new User();
        }

        //Get Selected Student Attendace
        public async Task<List<Attendance>> GetAttendanceByStudentIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<List<Attendance>>($"{BaseUrl}get_attendance_by_id.php?id={id}");
            return response ?? new List<Attendance>();
        }

        //Add Attendance for Selected Student
        public async Task<string> AddAttendanceAsync(Attendance attendance)
        {
            // Convert DateTime to the required string format
            var requestData = new
            {
                date = attendance.Date.ToString("yyyy-MM-dd"), // Format DateTime to string
                studentId = attendance.StudentID,
                status = attendance.Status
            };

            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}add_attendance.php", requestData);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        //Get Selected Student Academic History
        public async Task<List<AcademicHistory>> GetAcademicHistoryByStudentIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<List<AcademicHistory>>($"{BaseUrl}get_academic_history_by_id.php?id={id}");
            return response ?? new List<AcademicHistory>();
        }

        //Add Academic Record for Selected Student
        public async Task<string> AddAcademicHistoryAsync(AcademicHistory record)
        {
            var requestData = new
            {
                studentId = record.StudentID,
                programId = record.Program,
                level = record.Level,
                schoolYear = record.SchoolYear,
                semester = record.Semester,
                grade = record.Grade
            };

            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}add_academic_history.php", requestData);
            return await response.Content.ReadAsStringAsync();
        }


    }
}
