using Microsoft.EntityFrameworkCore.Diagnostics;
using student_management.Repository.AdminRepo;

namespace student_management.Repository.UnitOfWorkRepo
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminRepository Admin { get; }

        int Save();
    }
     
}

