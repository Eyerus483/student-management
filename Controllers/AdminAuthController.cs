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

       

       [HttpPost("Login")]
    public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request){
            var response = await _authRepository.AdminLogin(request.UserName, request.Password);
              if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
    }
[HttpPost("Register")]
       public async Task<ActionResult<ServiceResponse<int>>> Register(AddAdminDto request){
            var response = await _authRepository.AdminRegister(new Admin { UserName = request.UserName, FirstName = request.FirstName, LastName = request.LastName }, request.Password);
            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response); 
       }
[Authorize(Roles = "Admin")]
       [HttpPost("Register/Student")]
       public async Task<ActionResult<ServiceResponse<int>>> RegisterStudent(AddStudentDto request){
            var response = await _authRepository.StudentRegister(request);
            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response); 
       }
       
    }
}