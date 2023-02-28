using CafeForGames.Data;
using CafeForGames.Models;
using CafeForGames.Models.DTO;
using CafeForGames.Services.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CafeForGames.Services.Repository
{
    public class UserService : IUserService
    {
        public AppDbContext _Contexts;
        public string secretkey;
        public UserService(AppDbContext Context,IConfiguration _Config)
        {
            _Contexts = Context;
            secretkey = _Config.GetValue<string>("ApiSettings:Secret");
        }


        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _Contexts.Users.FirstOrDefaultAsync(c=>c.UserName == loginRequestDTO.UserName
            && c.Password == loginRequestDTO.Password);
            if (user == null) 
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }
            var TokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = TokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO response = new LoginResponseDTO()
            {
                User = user,
                Token = TokenHandler.WriteToken(token)
            };
            return response;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _Contexts.Users.FirstOrDefault(c=>c.UserName == username);
            if (user == null) { return true; }
            return false;
        }

        public async Task<LocalUser> Register(RegisterRequestDTO registerRequestDTO)
        {
            LocalUser User = new LocalUser()
            {
                UserName = registerRequestDTO.UserName,
                Name = registerRequestDTO.Name,
                Password = registerRequestDTO.Password,
                Role = registerRequestDTO.Role,
            };
            await _Contexts.Users.AddAsync(User);
            await _Contexts.SaveChangesAsync();
            User.Password = "";
            return User;
        }
    }
}
