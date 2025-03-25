using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrackingBle.src._1Auth.Data;
using TrackingBle.src._1Auth.Models.Dto.AuthDtos;
using TrackingBle.src._1Auth.Models.Domain;
using BCrypt.Net;

namespace TrackingBle.src._1Auth.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    }

    public class AuthService : IAuthService
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(
            AuthDbContext context,
            IMapper mapper,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .Include(u => u.Group)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Verify(dto.Password, user.Password))
                throw new Exception("Invalid email or password.");
            if (user.StatusActive != StatusActive.Active)
                throw new Exception("Account is not active.");
            if (user.IsEmailConfirmation == 0)
                throw new Exception("Email not confirmed.");

            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);
            return new AuthResponseDto
            {
                Token = token,
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                GroupId = user.GroupId,
                IsEmailConfirmed = user.IsEmailConfirmation == 1,
                StatusActive = user.StatusActive.ToString()
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("Email is already registered.");

            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(currentUserId))
                throw new UnauthorizedAccessException("User not authenticated.");

            var currentUser = await _context.Users
                .Include(u => u.Group)
                .FirstOrDefaultAsync(u => u.Id == Guid.Parse(currentUserId));
            if (currentUser == null)
                throw new UnauthorizedAccessException("Current user not found.");

            var currentUserRole = currentUser.Group?.LevelPriority;
            if (currentUserRole == LevelPriority.Primary)
            {
                var targetGroup = await _context.UserGroups
                    .FirstOrDefaultAsync(g => g.Id == dto.GroupId);
                if (targetGroup == null || targetGroup.LevelPriority != LevelPriority.UserCreated)
                    throw new UnauthorizedAccessException("Users with Primary role can only create accounts with UserCreated role.");
            }

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                Email = dto.Email,
                Password = BCrypt.HashPassword(dto.Password),
                IsCreatedPassword = 1,
                IsEmailConfirmation = 0,
                EmailConfirmationCode = Guid.NewGuid().ToString(),
                EmailConfirmationExpiredAt = DateTime.UtcNow.AddDays(7),
                EmailConfirmationAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow,
                StatusActive = StatusActive.Active,
                GroupId = dto.GroupId
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            newUser.Group = await _context.UserGroups.FirstOrDefaultAsync(g => g.Id == newUser.GroupId);
            if (newUser.Group == null)
                throw new Exception("Assigned group not found.");

            var token = GenerateJwtToken(newUser);
            return new AuthResponseDto
            {
                Token = token,
                Id = newUser.Id,
                Username = newUser.Username,
                Email = newUser.Email,
                GroupId = newUser.GroupId,
                IsEmailConfirmed = newUser.IsEmailConfirmation == 1,
                StatusActive = newUser.StatusActive.ToString()
            };
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("groupId", user.GroupId.ToString()),
                new Claim(ClaimTypes.Role, user.Group.LevelPriority.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}