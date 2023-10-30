using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student_management.Dto.DashboardDto
{
    public class AdminDashboardResponseDto
    {
        public int TotalUsers { get; set; }
        public int TotalStudents { get; set; }
        public int TotalTeachers { get; set; }
        public int TotalDepartments { get; set; }
        public int StudentsRegisteredThisYear { get; set; }
        public int StudentsRegisteredLastYear { get; set; }
        public int StudentsRegisteredIncreaseThisYear { get; set; }
        public int StudentsRegisteredIncreseLastYear { get; set; }
        public int TotalVisitors { get; set; }
    }
}