using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student_management.Dto.AdminDto
{
    public class AdminProfileResponseDto
    {
       public int Id { get; set; }
       public string Pid { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
    }
}