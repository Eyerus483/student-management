using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_management.Auth;
using student_management.Dto.AdminDto;
using student_management.Dto.DashboardDto;
using student_management.Dto.StudentDto;
using student_management.Model;
using student_management.Repository.UnitOfWorkRepo;

namespace student_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AdminController(IAuthRepository authRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
          
            _authRepository = authRepository;

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("student/register")]
        public async Task<ActionResult<ServiceResponse<int>>> RegisterStudent(StudentRequestDto request)
        {
            var response = await _authRepository.StudentRegister(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("profile")]
        public async Task<ActionResult<ServiceResponse<AdminProfileResponseDto>>> GetProfile(string pid)
        {
            var response = await _authRepository.GetAdminProfile(pid);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("dashboard")]

        public async Task<ActionResult<ServiceResponse<AdminDashboardResponseDto>>> GetAdminDashboard()
        {

            var response = await _unitOfWork.Admin.GetAdminDashboard();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }
    }
}