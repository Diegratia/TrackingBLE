

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using TrackingBle.src._1Auth.Data;
    using TrackingBle.src._1Auth.Models.Dto.AuthDtos;
    using TrackingBle.src._1Auth.Models.Domain;
    using BCrypt.Net;

    namespace TrackingBle.src_1.Auth.Services
    {
        public interface IAuthService
        {
            Task<AuthResponseDto> LoginAsync(LoginDto dto);
        }

        public class AuthService : IAuthService
        {
            private readonly AuthDbContext _context;
            private readonly IMapper _mapper;
            private readonly IConfiguration _configuration;

            public AuthService(AuthDbContext context, IMapper mapper, IConfiguration configuration)
            {
                _context = context;
                _mapper = mapper;
                _configuration = configuration;
            }

            public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
            {
                var user = await _context.Users
                    .Include(u => u.Group)
                    .FirstOrDefaultAsync(u => u.Email == dto.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
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

            private string GenerateJwtToken(User user)
            {
    
                Console.WriteLine($"AuthService Jwt:Issuer = {_configuration["Jwt:Issuer"]}");
                Console.WriteLine($"AuthService Jwt:Audience = {_configuration["Jwt:Audience"]}");
                Console.WriteLine($"AuthService Jwt:Key = {_configuration["Jwt:Key"]}");

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("groupId", user.GroupId.ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds);

                    

                 var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    Console.WriteLine($"Generated token: {tokenString}");
                    Console.WriteLine($"Token length: {tokenString.Length}");
                    Console.WriteLine($"Token parts: {tokenString.Split('.').Length}");
                    
                    return tokenString;
            }
        }
    }
