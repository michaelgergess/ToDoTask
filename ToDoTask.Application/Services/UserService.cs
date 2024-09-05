using ToDoTask.Application.DTOs;
using ToDoTask.Application.Interfaces;
using ToDoTask.Domain.Entities;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;

namespace ToDoTask.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<bool> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            var existingUser = (await _unitOfWork.UserRepository.GetAllAsync())
                .FirstOrDefault(u => u.Name == registerUserDto.Name);

            if (existingUser != null)
                return false;

            var user = _mapper.Map<User>(registerUserDto);
            user.RoleId = 1;
            user.Password = HashPassword(registerUserDto.Password);


            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(); 

            return true;
        }

        public async Task<string> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Name == loginUserDto.Name);
            if (user == null || !VerifyPassword(loginUserDto.Password, user.Password))
                return null; 

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(45),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}
