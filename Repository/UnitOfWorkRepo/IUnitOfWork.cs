using Microsoft.EntityFrameworkCore.Diagnostics;
using student_management.Repository.AdminRepo;
using student_management.Repository.DepartmentRepo;
using student_management.Repository.StatusRepo;

namespace student_management.Repository.UnitOfWorkRepo
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminRepository Admin { get; }

        IStatusRepository Status { get; }
        IDepartmentRepository Department { get; }

        int Save();
    }
     
}

