using student_management.Dto.CourseDto;
using student_management.Dto.DepartmentDto;
using student_management.Model;

namespace student_management.Dto.TeacherDto
{
    public class TeacherRequestDto
    {
      
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string SubCity { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public List<CourseRequestDto>? Course { get; set; }
        public DepartmentRequestDto? Department { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Password { get; set; } = string.Empty;

    }
}