using student_management.Model;

namespace student_management.Dto.CourseDto
{
    public class CourseRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CreditHours { get; set; }
        public string CourseCode { get; set; } = string.Empty;
        public string TargetGroup { get; set; } = string.Empty;
        public int Hours { get; set; }
        
      
    }
}