namespace student_management.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string Pid { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Department;
        public string UserName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string BlockNumber { get; set; } = string.Empty;
        public int YearOfEstablishment { get; set; }
        public List<Student>? Students { get; set; }
        public List<Course>? Courses { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}