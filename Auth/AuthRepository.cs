using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using student_management.Data;
using student_management.Model;

namespace student_management.Auth
{
    public class AuthRepository : IAuthRepository
{
        public readonly DataContext _Context;
        public readonly IConfiguration _Configuration;
        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _Configuration = configuration;
            _Context = context;

        }
        public async Task<ServiceResponse<string>> Login(string userName, string password)
        {
            var respone = new ServiceResponse<string>();
            var user = await _Context.Admins.FirstOrDefaultAsync(u => u.UserName.ToLower().Equals(userName.ToLower()));
            if ((user is null) || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                respone.Success = false;
                respone.Message = "Incorrect UserName or Password";

            }
            else
            {
                respone.Data = CreateToken(user);
            }
            return respone;
        }

        public async Task<ServiceResponse<int>> Register(Admin admin, string password)
        {
            var response = new ServiceResponse<int>();
            if (await UserExistes(admin.UserName))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;
            _Context.Admins.Add(admin);
            await _Context.SaveChangesAsync();
            response.Data = admin.Id;
            return response;
        }

        public async Task<bool> UserExistes(string userName)
        {
            if (await _Context.Admins.AnyAsync(u => u.UserName.ToLower() == userName.ToLower()))
            {
                return true;
            }
            return false;
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
        private string CreateToken(Admin admin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
            new Claim(ClaimTypes.Name, admin.UserName)
        };
            var appsettingstoken = _Configuration.GetSection("AppSettings:Token").Value;
            if(appsettingstoken is null)
                throw new Exception("Appsetting token is null");
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appsettingstoken));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}