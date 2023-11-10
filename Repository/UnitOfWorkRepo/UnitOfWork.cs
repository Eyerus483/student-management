using AutoMapper;
using student_management.Data;
using student_management.Repository.AdminRepo;
using student_management.Repository.DepartmentRepo;
using student_management.Repository.StatusRepo;

namespace student_management.Repository.UnitOfWorkRepo
{
    public class UnitOfWork : IUnitOfWork


    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(DataContext context, IMapper mapper)

        {
            _mapper = mapper;
            _context = context;

            Admin = new AdminRepository(_context);

            Status = new StatusRepository(_context);

            Department = new DepartmentRepository(_context, _mapper);
        }
        public IAdminRepository Admin { get; private set; }

        public IStatusRepository Status { get; private set; }
        public IDepartmentRepository Department { get; private set; }

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