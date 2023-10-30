namespace student_management.Model
{
    public class Student
    {
        public int Id { get; set; }
        public Role Role { get; set; } = Role.Student;
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        
        public string SubCity { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Section { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public EnrollmentClass EnrollmentType { get; set; }
        public List<Course>? Course { get; set; }
        public Department? Department { get; set; }

        public DateTime RegistrationDate { get; set; }

        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        


    }
}