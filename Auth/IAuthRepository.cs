using student_management.Dto.AdminDto;
using student_management.Dto.StudentDto;
using student_management.Model;

namespace student_management.Auth
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> AdminRegister(Admin admin, string password);
        Task<ServiceResponse<GetAdminDto>> AdminLogin(string userName, string password);
        Task<ServiceResponse<int>> StudentRegister(AddStudentDto request);
        Task<ServiceResponse<GetStudentDto>> StudentLogin(string userName, string password);
        Task<ServiceResponse<GetAdminDto>> GetProfile(int id);
      
    }
}