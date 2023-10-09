using student_management.Model;

namespace student_management.Auth
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(Admin admin, string password);
        Task<ServiceResponse<string>> Login(string userName, string password);
        Task<bool> UserExistes(string userName); 
    }
}