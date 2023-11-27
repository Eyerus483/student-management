using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using student_management.Data;
using student_management.Dto.CourseDto;
using student_management.Helpers.Common;
using student_management.Model;

namespace student_management.Repository.CourseRepo
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CourseRepository(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
            
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
         public async Task<ServiceResponse<CourseResponseDto>> CreateCourse(CourseRequestDto request)
        {
            var response = new ServiceResponse<CourseResponseDto>();
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Title.ToLower().Equals(request.Title.ToLower()));
            var courseRequest = _mapper.Map<Course>(request);

            if(course != null){
                response.Success = false;
                response.Message = "Course already exists";
                return response;
            }
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == GetUserId());
            if(department == null){
                response.Success = false;
                response.Message = "Department not found";
                return response;
            }
            courseRequest.DepartmentId = GetUserId();
            department.CreatedAt = CommonMethods.GetCurrentEasternDateTime();
            department.UpdatedAt = CommonMethods.GetCurrentEasternDateTime();
            _context.Courses.Add(courseRequest);
            await _context.SaveChangesAsync();

            //response.Data = await _context.Courses.FirstOrDefaultAsync();
            response.Message = "Succesfully registered";
            return response;

        }

        public async Task<ServiceResponse<List<CourseResponseDto>>> FetchAllCourses()
        {
            var response = new ServiceResponse<List<CourseResponseDto>>();
            if (!await _context.Courses.AnyAsync()){
                response.Message = "No courses found";
                return response;
            }
            response.Data = await _context.Courses.Select(c => _mapper.Map<CourseResponseDto>(c)).ToListAsync();
            return response;

        }

        public async Task<ServiceResponse<List<CourseResponseDto>>> FetchCoursesByStudent()
        {
            var response = new ServiceResponse<List<CourseResponseDto>>();
            var student = await _context.Students.Include(s=> s.Course).FirstOrDefaultAsync(s => s.Id == GetUserId());
            if(student == null){
                response.Success = false;
                response.Message = "Student not found";
                return response;
            }
            if(student.Course == null){
                response.Message = "No course registered";
                return response;
            }
            response.Data = student.Course.Select(c => _mapper.Map<CourseResponseDto>(c)).ToList();
            return response;
        }
        public async Task<ServiceResponse<List<CourseResponseDto>>> FeatchCourseseByDepartment()
        {
            var response = new ServiceResponse<List<CourseResponseDto>>();
            var department = await _context.Departments.Include(d => d.Courses).FirstOrDefaultAsync(s => s.Id == GetUserId());
            if(department == null){
                response.Success = false;
                response.Message = "Department not found";
                return response;
            }
            if(department.Courses == null){
                response.Message = "No Courses found";
                return response;
            }
            response.Data = department.Courses.Select(d => _mapper.Map<CourseResponseDto>(d)).ToList();
            return response;
        }


        public async Task<ServiceResponse<List<CourseResponseDto>>> SearchForCourses(string key)
        {
            var response = new ServiceResponse<List<CourseResponseDto>>();
            var courses = await _context.Courses.Where(c => c.Title.ToLower().Contains(key.ToLower())).ToListAsync();
            if(courses == null){
                response.Message = "Course not found";
                return response;
            }
            response.Data = courses.Select(c => _mapper.Map<CourseResponseDto>(c)).ToList();
            return response;

        }

        public async Task<ServiceResponse<CourseResponseDto>> UpdateCourses(CourseResponseDto request)
        {
            var response = new ServiceResponse<CourseResponseDto>();
            var course = await _context.Courses.Where(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (course == null)
            {
                response.Success = false;
                response.Message = "Course doesn't exist";
                return response;
            }
         
            var courseUpdate = _mapper.Map<Course>(request);

            course.Title = courseUpdate.Title;
            course.CourseCode = courseUpdate.CourseCode;
            course.CreditHours = courseUpdate.CreditHours;
            course.Description = courseUpdate.Description;
            course.Hours = courseUpdate.Hours;
            course.TargetGroup = courseUpdate.TargetGroup;

            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<CourseResponseDto>(course);
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteCourse(int id)
        {
            var response = new ServiceResponse<string>();
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if(course == null){
                response.Success = false;
                response.Message = "Course not found";
                return response;
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            response.Message = "successfuly deleted";
            return response;
        }
    }
}