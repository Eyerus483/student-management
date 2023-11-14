using AutoMapper;
using Microsoft.EntityFrameworkCore;
using student_management.Data;
using student_management.Dto.CourseDto;
using student_management.Model;

namespace student_management.Repository.DepartmentRepo
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public DepartmentRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }
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
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == request.DepartmentId);
            if(department == null){
                response.Success = false;
                response.Message = "Department not found";
                return response;
            }
            _context.Courses.Add(courseRequest);
            await _context.SaveChangesAsync();

            //response.Data = await _context.Courses.FirstOrDefaultAsync();
            response.Message = "Succesfully registered";
            return response;

        }

    }
}