using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using student_management.Auth;
using student_management.Dto.AdminDto;
using student_management.Dto.CourseDto;
using student_management.Dto.DashboardDto;
using student_management.Dto.DepartmentDto;
using student_management.Model;
using student_management.Repository.UnitOfWorkRepo;

namespace student_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthRepository _authRepository;
        public DepartmentController(IUnitOfWork unitOfWork, IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;

        }
        [HttpPost("login")]
       
        public async Task<ActionResult<ServiceResponse<DepartmentResponseDto>>> Login(UserLoginDto request)
        {
            var response = await _authRepository.DepartmentLogin(request.UserName, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("fetch-all")]
        public async Task<ActionResult<ServiceResponse<DepartmentResponseDto>>> FeatchDepartment()
        {
            var response = await _unitOfWork.Department.FetchAllDepartments();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ServiceResponse<DepartmentResponseDto>>> SearchDepartment([Required] string key)
        {
            var response = await _unitOfWork.Department.SearchForDepartment(key);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Department")]
        [HttpGet("profile")]
        public async Task<ActionResult<ServiceResponse<DepartmentProfileDto>>> GetDepartmentProfile([FromHeader, Required] string pid)
        {
            var response = await _unitOfWork.Department.GetDepartmentProfile(pid);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Department")]
        [HttpPut("update")]
        public async Task<ActionResult<ServiceResponse<DepartmentResponseDto>>> UpdateDepartment(DepartmentUpdateDto request)
        {
            var response = await _unitOfWork.Department.UpdateDepartment(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Department")]
        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteDepartment(int id)
        {
            var response = await _unitOfWork.Department.DeleteDepartment(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}