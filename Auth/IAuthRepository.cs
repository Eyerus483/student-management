using student_management.Model;

namespace student_management.Auth
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> AdminRegister(Admin admin, string password);
        Task<ServiceResponse<string>> AdminLogin(string userName, string password);
        Task<ServiceResponse<int>> StudentRegister(Student student, string password);
        Task<ServiceResponse<string>> StudentLogin(string userName, string password);
      
    }
}