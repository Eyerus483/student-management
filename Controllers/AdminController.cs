using System;
using System.Collections.Generic;
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
    public class AdminController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AdminController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            
        }
      
       [Authorize(Roles = "Admin")]
       [HttpPost("student/register")]
       public async Task<ActionResult<ServiceResponse<int>>> RegisterStudent(StudentRequestDto request){
            var response = await _authRepository.StudentRegister(request);
            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response); 
       }
       
       [Authorize(Roles = "Admin")]
       [HttpGet("profile")]
       public async Task<ActionResult<ServiceResponse<AdminProfileResponseDto>>> GetProfile(int id){
            var response = await _authRepository.GetAdminProfile(id);
            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response); 
       }
       
    }
}