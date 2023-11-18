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

        public async Task<ServiceResponse<string>> DeleteAdmin(string pid)
        {
            var response = new ServiceResponse<string>();
            var admin = await _context.Admins.FirstOrDefaultAsync(d => d.Pid == pid);
            if(admin == null){
                response.Success = false;
                response.Message = "User not found";
                return response;
            }
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            response.Message = "Successfully deleted";
            return response;
        }

        public async Task<ServiceResponse<AdminDashboardResponseDto>> GetAdminDashboard()
        {
            var response = new ServiceResponse<AdminDashboardResponseDto>();
            var totalStudents = await _context.Students.CountAsync();
            var totalDepartments = await _context.Departments.CountAsync();
            var totalTeachers = await _context.Teachers.CountAsync();
            var visitors = await _context.Visitors.FirstOrDefaultAsync();

            response.Data = new AdminDashboardResponseDto { TotalStudents = totalStudents, TotalDepartments = totalDepartments, TotalTeachers = totalTeachers, TotalVisitors = visitors is null ? 0 : visitors.Count };

            return response;
        }
    }
}