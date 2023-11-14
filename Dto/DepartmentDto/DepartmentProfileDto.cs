using student_management.Dto.CourseDto;
using student_management.Dto.StudentDto;
using student_management.Model;

namespace student_management.Dto.DepartmentDto
{
    public class DepartmentProfileDto
    {
        public int Id { get; set; }
        public string Pid { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string BlockNumber { get; set; } = string.Empty;
        public int YearOfEstablishment { get; set; }
        public List<StudentProfileResponseDto>? Students { get; set; }
        public List<CourseResponseDto>? Courses { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}