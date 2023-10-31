using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using student_management.Data;
using student_management.Dto.DashboardDto;
using student_management.Model;

namespace student_management.Repository.AdminRepo
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;
        public AdminRepository(DataContext context)
        {
            _context = context;
            
        }
        public async Task<ServiceResponse<AdminDashboardResponseDto>> GetAdminDashboard()
        {
            var response = new ServiceResponse<AdminDashboardResponseDto>();
            var totalStudents = await _context.Students.CountAsync();
            var totalDepartments = await _context.Departments.CountAsync();
            var totalTeachers = await _context.Teachers.CountAsync();

            response.Data = new AdminDashboardResponseDto { TotalStudents = totalStudents, TotalDepartments = totalDepartments, TotalTeachers = totalTeachers };

            return response;
        }
    }
}