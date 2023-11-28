using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_management.Dto.CourseDto;
using student_management.Model;
using student_management.Repository.UnitOfWorkRepo;

namespace student_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [Authorize(Roles = "Department")]
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<CourseResponseDto>>> RegisterCourse(CourseRequestDto request)
        {
            var response = await _unitOfWork.Course.CreateCourse(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("fetch-all")]
        public async Task<ActionResult<ServiceResponse<List<CourseResponseDto>>>> FetchAllCourses()
        {
            var response = await _unitOfWork.Course.FetchAllCourses();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Student")]
        [HttpGet("fetch-by-student")]
        public async Task<ActionResult<ServiceResponse<List<CourseResponseDto>>>> FetchCoursesByStudent()
        {
            var response = await _unitOfWork.Course.FetchCoursesByStudent();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ServiceResponse<List<CourseResponseDto>>>> SearchCourses([Required] string title)
        {
            var response = await _unitOfWork.Course.SearchForCourses(title);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Department")]
        [HttpPut("update")]

        public async Task<ActionResult<ServiceResponse<CourseResponseDto>>> UpdateCourse([FromHeader,Required] int id, CourseRequestDto request)
        {
            var response = await _unitOfWork.Course.UpdateCourses(id: id, request: request);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        
        }
        [Authorize(Roles ="Department")]
        [HttpDelete("delete")]

        public async Task<ActionResult<ServiceResponse<CourseResponseDto>>> DeleteCourse([Required] int id)
        {
            var response = await _unitOfWork.Course.DeleteCourse(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return NoContent();

        }
        [Authorize(Roles = "Department")]
        [HttpGet("courses-by-department")]

        public async Task<ActionResult<ServiceResponse<List<CourseResponseDto>>>> FeatchCoursesByDepartment()
        {
            var response = await _unitOfWork.Course.FeatchCourseseByDepartment();
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }
    }

}