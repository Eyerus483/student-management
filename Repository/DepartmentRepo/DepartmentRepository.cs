using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using student_management.Data;
using student_management.Dto.CourseDto;
using student_management.Dto.DepartmentDto;
using student_management.Helpers.Common;
using student_management.Model;

namespace student_management.Repository.DepartmentRepo
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DepartmentRepository(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;

        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        public async Task<ServiceResponse<List<DepartmentResponseDto>>> FetchAllDepartments()
        {
            var response = new ServiceResponse<List<DepartmentResponseDto>>();
            if (!await _context.Departments.AnyAsync())
            {
                response.Message = "No Department found";
                return response;
            }
            response.Data = await _context.Departments.Select(d => _mapper.Map<DepartmentResponseDto>(d)).ToListAsync();
            return response;

        }
        public async Task<ServiceResponse<List<DepartmentResponseDto>>> SearchForDepartment(string key)
        {
            var response = new ServiceResponse<List<DepartmentResponseDto>>();
            var department = await _context.Departments.Where(d => d.DepartmentName.ToLower().Contains(key.ToLower())).ToListAsync();
            if (department == null)
            {
                response.Message = "Department not found";
                return response;
            }
            response.Data = department.Select(d => _mapper.Map<DepartmentResponseDto>(d)).ToList();
            return response;
        }
        public async Task<ServiceResponse<DepartmentResponseDto>> UpdateDepartment(DepartmentUpdateDto request)
        {
            var response = new ServiceResponse<DepartmentResponseDto>();
            var department = await _context.Departments.Where(d => d.Id == GetUserId()).FirstOrDefaultAsync();
            if (department == null)
            {
                response.Success = false;
                response.Message = "Department doesn't exist";
                return response;
            }

            var departmentUpdate = _mapper.Map<Department>(request);

            department.UserName = departmentUpdate.UserName;
            department.DepartmentName = departmentUpdate.DepartmentName;
            department.BlockNumber = departmentUpdate.BlockNumber;
            department.YearOfEstablishment = departmentUpdate.YearOfEstablishment;
            department.UpdatedAt = CommonMethods.GetCurrentEasternDateTime();
           

            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<DepartmentResponseDto>(department);
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteDepartment(int id)
        {
            var response = new ServiceResponse<string>();
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (department == null)
            {
                response.Success = false;
                response.Message = "Department not found";
                return response;
            }
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            response.Message = "successfuly deleted";
            return response;
        }

        public async Task<ServiceResponse<DepartmentProfileDto>> GetDepartmentProfile(string pid)
        {
            var response = new ServiceResponse<DepartmentProfileDto>();
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Pid.Equals(pid));
            if (department is null)
            {
                response.Success = false;
                response.Message = "Department not found";
            }
            else
            {
                response.Data = _mapper.Map<DepartmentProfileDto>(department);
            }
            return response;
        }
    }
}


 
