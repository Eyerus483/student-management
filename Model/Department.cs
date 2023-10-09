namespace student_management.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string BlockNumber { get; set; } = string.Empty;
        public DateTime YearOfEstablishment { get; set; }
        public List<Student>? Students { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}