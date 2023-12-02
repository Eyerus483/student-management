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
using student_management.Repository.UnitOfWorkRepo;

namespace student_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminAuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AdminAuthController(IAuthRepository authRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _authRepository = authRepository;

        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<AdminResponseDto>>> Login(UserLoginDto request)
        {
            var response = await _authRepository.AdminLogin(request.UserName, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<string>>> Register(AdminRequestDto request)
        {
            var response = await _authRepository.AdminRegister(new Admin { UserName = request.UserName, FirstName = request.FirstName, LastName = request.LastName }, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(Register), response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]

        public async Task<ActionResult<ServiceResponse<AdminProfileResponseDto>>> AdminUpdate(AdminUpdateDto request)
        {
            var response = await _authRepository.AdminUpdate(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]

        public async Task<ActionResult<ServiceResponse<string>>> AdminDelete([FromHeader, Required] string pid)
        {
            var response = await _unitOfWork.Admin.DeleteAdmin(pid);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return NoContent();
        }
    }
}