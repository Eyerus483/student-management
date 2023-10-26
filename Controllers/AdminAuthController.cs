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
    public class AdminAuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
       public AdminAuthController(IAuthRepository authRepository)
       {
            _authRepository = authRepository;
        
       } 

       

       [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<AdminResponseDto>>> Login(UserLoginDto request){
            var response = await _authRepository.AdminLogin(request.UserName, request.Password);
              if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
    }
[HttpPost("register")]
       public async Task<ActionResult<ServiceResponse<int>>> Register(AdminRequestDto request){
            var response = await _authRepository.AdminRegister(new Admin { UserName = request.UserName, FirstName = request.FirstName, LastName = request.LastName }, request.Password);
            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response); 
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
       public async Task<ActionResult<ServiceResponse<AdminResponseDto>>> GetProfile(int id){
            var response = await _authRepository.GetAdminProfile(id);
            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response); 
       }
       
    }
}