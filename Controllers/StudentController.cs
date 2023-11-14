using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_management.Auth;
using student_management.Dto.AdminDto;
using student_management.Dto.StudentDto;
using student_management.Model;

namespace student_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public StudentController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;

        }
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<StudentResponseDto>>> Login(UserLoginDto request)
        {
            var response = await _authRepository.StudentLogin(request.UserName, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Student")]
        [HttpGet("profile")]
        public async Task<ActionResult<ServiceResponse<StudentProfileResponseDto>>> GetStudentProfile([FromHeader, Required] string pid)
        {
            var response = await _authRepository.GetStudentProfile(pid);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }

}