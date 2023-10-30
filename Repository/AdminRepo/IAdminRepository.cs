using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using student_management.Dto.DashboardDto;
using student_management.Model;

namespace student_management.Repository.AdminRepo
{
    public interface IAdminRepository
    {
        public Task<ServiceResponse<AdminDashboardResponseDto>> GetAdminDashboard(); 
    }
}