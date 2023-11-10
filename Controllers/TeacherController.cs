using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using student_management.Auth;
using student_management.Dto.AdminDto;
using student_management.Dto.DashboardDto;
using student_management.Dto.DepartmentDto;
using student_management.Dto.TeacherDto;
using student_management.Model;
using student_management.Repository.UnitOfWorkRepo;

namespace student_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthRepository _authRepository;
        public TeacherController(IUnitOfWork unitOfWork, IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;

        }
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<TeacherResponseDto>>> Login(UserLoginDto request)
        {
            var response = await _authRepository.TeacherLogin(request.UserName, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}