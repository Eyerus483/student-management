using student_management.Dto.CourseDto;
using student_management.Dto.DepartmentDto;
using student_management.Model;

namespace student_management.Dto.StudentDto
{
    public class StudentResponseDto
    {
        public int Id { get; set; }
        public string Pid { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Teacher;
        public string Photo { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SubCity { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Section { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public EnrollmentClass EnrollmentType { get; set; }
        public List<CourseResponseDto>? Course { get; set; }
        public DepartmentProfileDto? Department { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        
    }
}