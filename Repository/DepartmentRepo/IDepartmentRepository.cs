using student_management.Dto.CourseDto;
using student_management.Model;

namespace student_management.Repository.DepartmentRepo
{
    public interface IDepartmentRepository
    {
        public Task<ServiceResponse<CourseResponseDto>> CreateCourse(CourseRequestDto request);
    }
}