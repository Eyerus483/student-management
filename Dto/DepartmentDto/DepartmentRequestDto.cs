using student_management.Dto.StudentDto;
using student_management.Model;

namespace student_management.Dto.DepartmentDto
{
    public class DepartmentRequestDto
    {
        public string UserName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string BlockNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int YearOfEstablishment { get; set; }
      
    }
}