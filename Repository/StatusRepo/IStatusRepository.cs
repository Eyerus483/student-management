using student_management.Model;

namespace student_management.Repository.StatusRepo
{
    public interface IStatusRepository
    {
        Task<ServiceResponse<string?>> UpdateVisitorsCount(); 
    }
}