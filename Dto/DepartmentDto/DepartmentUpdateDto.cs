namespace student_management.Dto.DepartmentDto
{
    public class DepartmentUpdateDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string BlockNumber { get; set; } = string.Empty;
        public int YearOfEstablishment { get; set; }
    }
}