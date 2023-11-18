using AutoMapper;
using Microsoft.EntityFrameworkCore;
using student_management.Data;
using student_management.Dto.CourseDto;
using student_management.Model;

namespace student_management.Repository.DepartmentRepo
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public DepartmentRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }
       
    }
}