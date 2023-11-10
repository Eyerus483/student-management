using student_management.Dto.AdminDto;
using student_management.Dto.DepartmentDto;
using student_management.Dto.StudentDto;
using student_management.Dto.TeacherDto;
using student_management.Model;

namespace student_management.Auth
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string>> AdminRegister(Admin admin, string password);
        Task<ServiceResponse<AdminResponseDto>> AdminLogin(string userName, string password);
        Task<ServiceResponse<string>> StudentRegister(StudentRequestDto request);
        Task<ServiceResponse<string>> TeacherRegister(TeacherRequestDto request);
        Task<ServiceResponse<StudentResponseDto>> StudentLogin(string userName, string password);
        Task<ServiceResponse<AdminProfileResponseDto>> GetAdminProfile(string pid);
        Task<ServiceResponse<StudentProfileResponseDto>> GetStudentProfile(string pid);
        Task<ServiceResponse<string>> DepartmentRegister(DepartmentRequestDto request);
         Task<ServiceResponse<DepartmentResponseDto>> DepartmentLogin(string userName, string password);
         Task<ServiceResponse<TeacherResponseDto>> TeacherLogin(string userName, string password);

    }
}