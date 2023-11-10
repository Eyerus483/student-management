using student_management.Model;

namespace student_management.Dto.DepartmentDto
{
    public class DepartmentResponseDto
    {
        public int Id { get; set; }
        public string Pid { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Department;
        public string DepartmentName { get; set; } = string.Empty;
        public string BlockNumber { get; set; } = string.Empty;
        public int YearOfEstablishment { get; set; }
        public List<Student>? Students { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}