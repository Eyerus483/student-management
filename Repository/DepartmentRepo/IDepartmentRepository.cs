using student_management.Dto.CourseDto;
using student_management.Dto.DepartmentDto;
using student_management.Model;

namespace student_management.Repository.DepartmentRepo
{
    public interface IDepartmentRepository
    {
        public Task<ServiceResponse<List<DepartmentResponseDto>>> FetchAllDepartments();
        public Task<ServiceResponse<List<DepartmentResponseDto>>> SearchForDepartment(string key);
        Task<ServiceResponse<DepartmentResponseDto>> UpdateDepartment(DepartmentUpdateDto request);
        Task<ServiceResponse<string>> DeleteDepartment(int id);
    
    }
}