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

            response.Data = new AdminDashboardResponseDto { TotalStudents = await _context.Students.CountAsync()};

            return response;
        }
    }
}