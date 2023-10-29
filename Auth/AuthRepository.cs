using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using student_management.Data;
using student_management.Dto.AdminDto;
using student_management.Dto.StudentDto;
using student_management.Model;

namespace student_management.Auth
{
    public class AuthRepository : IAuthRepository
    {
        public readonly DataContext _context;
        public readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthRepository(DataContext context, IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
            _context = context;

        }

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

        public async Task<ServiceResponse<int>> AdminRegister(Admin admin, string password)
        {
            var response = new ServiceResponse<int>();
            if (await _context.Admins.AnyAsync(u => u.UserName.ToLower() == admin.UserName.ToLower()))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;
            admin.Pid = GeneratePublicId();
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            response.Message = "Sucessfuly registered";
            return response;
        }
        public async Task<ServiceResponse<StudentResponseDto>> StudentLogin(string userName, string password)
        {
            var respone = new ServiceResponse<StudentResponseDto>();
            var user = await _context.Students.FirstOrDefaultAsync(u => u.UserName.ToLower().Equals(userName.ToLower()));
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

        public async Task<ServiceResponse<int>> StudentRegister(StudentRequestDto request)
        {
            var response = new ServiceResponse<int>();
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
            _context.Students.Add(student);
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
        public async Task<ServiceResponse<StudentProfileResponseDto>> GetStudentProfile(int id)
        {
            var response = new ServiceResponse<StudentProfileResponseDto>();
            var user = await _context.Students.FirstOrDefaultAsync(u => u.Id.Equals(id));
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
    }
}