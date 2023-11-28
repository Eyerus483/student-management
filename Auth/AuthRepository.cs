using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using student_management.Data;
using student_management.Dto.AdminDto;
using student_management.Dto.DepartmentDto;
using student_management.Dto.StudentDto;
using student_management.Dto.TeacherDto;
using student_management.Helpers.Common;
using student_management.Model;

namespace student_management.Auth
{
    public class AuthRepository : IAuthRepository
    {
        public readonly DataContext _context;
        public readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthRepository(DataContext context, IConfiguration configuration, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _configuration = configuration;
            _context = context;

        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        public async Task<ServiceResponse<AdminResponseDto>> AdminLogin(string userName, string password)
        {

            var response = new ServiceResponse<AdminResponseDto>();
            var user = await _context.Admins.FirstOrDefaultAsync(u => u.UserName.ToLower().Equals(userName.ToLower()));
            if ((user is null) || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Incorrect UserName or Password";

            }
            else
            {
                string token = CreateToken(user.Id, user.UserName, "Admin");
                AdminResponseDto getAdmin = _mapper.Map<AdminResponseDto>(user);
                getAdmin.Token = token;
                response.Data = getAdmin;
            }
            return response;
        }

        public async Task<ServiceResponse<string>> AdminRegister(Admin admin, string password)
        {
            var response = new ServiceResponse<string>();
            if (await _context.Admins.AnyAsync(u => u.UserName.ToLower() == admin.UserName.ToLower()))
            {
                response.Success = false;
                response.Message = "Admin already exists.";
                return response;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;
            admin.Pid = GeneratePublicId();
            admin.CreatedAt = CommonMethods.GetCurrentEasternDateTime();
            admin.UpdatedAt = CommonMethods.GetCurrentEasternDateTime();
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            response.Message = "Sucessfuly registered";
            return response;
        }
        public async Task<ServiceResponse<StudentResponseDto>> StudentLogin(string userName, string password)
        {
            var respone = new ServiceResponse<StudentResponseDto>();
            var user = await _context.Students.Include(u => u.Course).FirstOrDefaultAsync(u => u.UserName.ToLower().Equals(userName.ToLower()));
            if ((user is null) || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                respone.Success = false;
                respone.Message = "Incorrect UserName or Password";

            }
            else
            {

                string token = CreateToken(user.Id, user.UserName, "Student");
                StudentResponseDto getStudent = _mapper.Map<StudentResponseDto>(user);
                getStudent.Token = token;
                respone.Data = getStudent;
            }
            return respone;
        }

        public async Task<ServiceResponse<string>> StudentRegister(StudentRequestDto request)
        {
            var response = new ServiceResponse<string>();
            var student = _mapper.Map<Student>(request);
            if (await _context.Students.AnyAsync(u => u.UserName.ToLower() == student.UserName.ToLower()))
            {
                response.Success = false;
                response.Message = "Student already exists.";
                return response;
            }
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            student.PasswordHash = passwordHash;
            student.PasswordSalt = passwordSalt;
            student.Pid = GeneratePublicId();
            student.CreatedAt = CommonMethods.GetCurrentEasternDateTime();
            student.UpdatedAt = CommonMethods.GetCurrentEasternDateTime();
            int maxId = _context.Students.Any()?  _context.Students.Max(s => s.Id) : 0;
            maxId++;
            student.StudentId = $"SM/{maxId:D4}";
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            response.Message = "Sucessfuly registered";
            return response;
        }
        public async Task<ServiceResponse<TeacherResponseDto>> TeacherLogin(string userName, string password)
        {
            var respone = new ServiceResponse<TeacherResponseDto>();
            var user = await _context.Teachers.FirstOrDefaultAsync(u => u.UserName.ToLower().Equals(userName.ToLower()));
            if ((user is null) || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                respone.Success = false;
                respone.Message = "Incorrect UserName or Password";

            }
            else
            {

                string token = CreateToken(user.Id, user.UserName, "Teacher");
                TeacherResponseDto getTeacher = _mapper.Map<TeacherResponseDto>(user);
                getTeacher.Token = token;
                respone.Data = getTeacher;
            }
            return respone;
        }

        public async Task<ServiceResponse<string>> TeacherRegister(TeacherRequestDto request)
        {
            var response = new ServiceResponse<string>();
            var teacher = _mapper.Map<Teacher>(request);
            if (await _context.Teachers.AnyAsync(u => u.UserName.ToLower() == teacher.UserName.ToLower()))
            {
                response.Success = false;
                response.Message = "Teacher already exists.";
                return response;
            }
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            teacher.PasswordHash = passwordHash;
            teacher.PasswordSalt = passwordSalt;
            teacher.Pid = GeneratePublicId();
            teacher.CreatedAt = CommonMethods.GetCurrentEasternDateTime();
            teacher.UpdatedAt = CommonMethods.GetCurrentEasternDateTime();
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            response.Message = "Sucessfuly registered";
            return response;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(int id, string userName, string role)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, role)
        };
            var appsettingstoken = _configuration.GetSection("AppSettings:Token").Value;
            if (appsettingstoken is null)
                throw new Exception("Appsetting token is null");
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appsettingstoken));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GeneratePublicId()
        {
            return Guid.NewGuid().ToString();
        }
        public async Task<ServiceResponse<AdminProfileResponseDto>> GetAdminProfile(string pid)
        {
            var response = new ServiceResponse<AdminProfileResponseDto>();
            var user = await _context.Admins.FirstOrDefaultAsync(u => u.Pid.Equals(pid));
            if (user is null)
            {
                response.Success = false;
                response.Message = "User not found";

            }
            else
            {
                response.Data = _mapper.Map<AdminProfileResponseDto>(user);
            }
            return response;
        }
        public async Task<ServiceResponse<StudentProfileResponseDto>> GetStudentProfile(string pid)
        {
            var response = new ServiceResponse<StudentProfileResponseDto>();
            var user = await _context.Students.FirstOrDefaultAsync(u => u.Pid.Equals(pid));
            if (user is null)
            {
                response.Success = false;
                response.Message = "Student not found";
            }
            else
            {
                response.Data = _mapper.Map<StudentProfileResponseDto>(user);
            }
            return response;
        }
        public async Task<ServiceResponse<DepartmentResponseDto>> DepartmentLogin(string userName, string password)
        {
            var respone = new ServiceResponse<DepartmentResponseDto>();
            var user = await _context.Departments.Include(d => d.Courses).FirstOrDefaultAsync(u => u.UserName.ToLower().Equals(userName.ToLower()));
            if ((user is null) || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                respone.Success = false;
                respone.Message = "Incorrect UserName or Password";

            }
            else
            {

                string token = CreateToken(user.Id, user.UserName, "Department");
                DepartmentResponseDto getDepartment = _mapper.Map<DepartmentResponseDto>(user);
                getDepartment.Token = token;
                respone.Data = getDepartment;
            }
            return respone;
        }
        public async Task<ServiceResponse<string>> DepartmentRegister(DepartmentRequestDto request)
        {
            var response = new ServiceResponse<string>();
            var department = _mapper.Map<Department>(request);
            if (await _context.Departments.AnyAsync(u => u.UserName.ToLower() == department.UserName.ToLower()))
            {
                response.Success = false;
                response.Message = "Department already exists.";
                return response;
            }
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            department.PasswordHash = passwordHash;
            department.PasswordSalt = passwordSalt;
            department.Pid = GeneratePublicId();
            department.CreatedAt = CommonMethods.GetCurrentEasternDateTime();
            department.UpdatedAt = CommonMethods.GetCurrentEasternDateTime();
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            response.Message = "Sucessfuly registered";
            return response;
        }

        public async Task<ServiceResponse<AdminProfileResponseDto>> AdminUpdate(AdminUpdateDto request)
        {
            var response = new ServiceResponse<AdminProfileResponseDto>();
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Id == GetUserId());
            if(admin == null){
                response.Success = false;
                response.Message = "User doesn't exist";
                return response;
            }
            if (admin.UserName != request.UserName && await _context.Admins.FirstOrDefaultAsync(a => a.UserName.ToLower().Equals(request.UserName.ToLower())) != null) 
            {
                response.Success = false;
                response.Message = "User Name already exists";
                return response;
            }
            var adminUpdate = _mapper.Map<Admin>(request);

            admin.FirstName = adminUpdate.FirstName;
            admin.LastName = adminUpdate.LastName;
            admin.UserName = adminUpdate.UserName;
            admin.UpdatedAt = CommonMethods.GetCurrentEasternDateTime();

            // _mapper.Map(adminUpdate, admin);

            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<AdminProfileResponseDto>(admin);
            return response;
        }
    }
}
