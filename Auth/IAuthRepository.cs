using student_management.Dto.AdminDto;
using student_management.Dto.StudentDto;
using student_management.Model;

namespace student_management.Auth
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> AdminRegister(Admin admin, string password);
        Task<ServiceResponse<AdminResponseDto>> AdminLogin(string userName, string password);
        Task<ServiceResponse<int>> StudentRegister(StudentRequestDto request);
        Task<ServiceResponse<StudentResponseDto>> StudentLogin(string userName, string password);
        Task<ServiceResponse<AdminProfileResponseDto>> GetAdminProfile(int id);
        Task<ServiceResponse<StudentProfileResponseDto>> GetStudentProfile(int id);

    }
}