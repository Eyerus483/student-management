using student_management.Data;
using student_management.Repository.AdminRepo;

namespace student_management.Repository.UnitOfWorkRepo
{
    public class UnitOfWork : IUnitOfWork
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;

            Admin = new AdminRepository(_context);
        }
        public IAdminRepository Admin { get; private set; }
        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

    }
}