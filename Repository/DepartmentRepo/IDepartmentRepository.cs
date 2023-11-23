using student_management.Dto.CourseDto;
using student_management.Dto.DepartmentDto;
using student_management.Model;

namespace student_management.Repository.DepartmentRepo
{
    public interface IDepartmentRepository
    {
        Task<ServiceResponse<DepartmentUpdateDto>> UpdateDepartment(int id);
        Task<ServiceResponse<string>> DeleteDepartment(int id);
    
    }
}