namespace student_management.Model
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Decription { get; set; } = string.Empty;
        public int CreditHours { get; set; }
        public string CourseCode { get; set; } = string.Empty;
        public string TargetGroup { get; set; } = string.Empty;
        public int Hours { get; set; }
        public string Lecturer { get; set; } = string.Empty;
        public List<Student>? Students { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}