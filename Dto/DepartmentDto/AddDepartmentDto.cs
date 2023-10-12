using student_management.Model;

namespace student_management.Dto.DepartmentDto
{
    public class AddDepartmentDto
    {
        public string UserName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string BlockNumber { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
        public int YearOfEstablishment { get; set; }
        public List<Student>? Students { get; set; }
    }
}