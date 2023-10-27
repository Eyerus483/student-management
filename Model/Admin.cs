namespace student_management.Model
{
    public class Admin
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Admin;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}