using student_management.Model;

namespace student_management.Dto.DepartmentDto
{
    public class GetDepartmentDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string BlockNumber { get; set; } = string.Empty;
        public DateTime YearOfEstablishment { get; set; }
        public List<Student>? Students { get; set; }
    }
}