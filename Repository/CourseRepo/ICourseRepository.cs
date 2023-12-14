using student_management.Dto.CourseDto;
using student_management.Model;

namespace student_management.Repository.CourseRepo
{
    public interface ICourseRepository
    {
        public Task<ServiceResponse<CourseResponseDto>> CreateCourse(CourseRequestDto request);
        public Task<ServiceResponse<List<CourseResponseDto>>> FetchAllCourses();
        public Task<ServiceResponse<List<CourseResponseDto>>> FetchCoursesByStudent();
        public Task<ServiceResponse<List<CourseResponseDto>>> SearchForCourses(string key);
        public Task<ServiceResponse<CourseResponseDto>> UpdateCourses(CourseRequestDto request, int id);
        public Task<ServiceResponse<string>> DeleteCourse(int id);
        public Task<ServiceResponse<List<CourseResponseDto>>> FeatchCourseseByDepartment();
        public Task<ServiceResponse<List<CourseResponseDto>>> FeatchCourseseByDepartmentId(int id);

    }
}