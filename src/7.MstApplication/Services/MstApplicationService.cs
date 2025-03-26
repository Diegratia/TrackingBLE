using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingBle.src._7MstApplication.Data;
using TrackingBle.src._7MstApplication.Models.Domain;
using TrackingBle.src._7MstApplication.Models.Dto.MstApplicationDtos;
using BCrypt.Net;

namespace TrackingBle.src._7MstApplication.Services
{
    public class MstApplicationService : IMstApplicationService
    {
        private readonly MstApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MstApplicationService(MstApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<MstApplicationDto>> GetAllApplicationsAsync()
        {
            var applications = await _context.MstApplications.ToListAsync();
            if (!applications.Any())
            {
                Console.WriteLine("No MstApplications found in database.");
                return new List<MstApplicationDto>();
            }

            Console.WriteLine($"Found {applications.Count} MstApplications in database.");
            return _mapper.Map<IEnumerable<MstApplicationDto>>(applications);
        }

        public async Task<MstApplicationDto> GetApplicationByIdAsync(Guid id)
        {
            var application = await _context.MstApplications.FirstOrDefaultAsync(a => a.Id == id);
            if (application == null)
            {
                Console.WriteLine($"MstApplication with ID {id} not found in database.");
                return null;
            }

            return _mapper.Map<MstApplicationDto>(application);
        }

        public async Task<MstApplicationDto> CreateApplicationAsync(MstApplicationCreateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            // Buat MstApplication
            var application = _mapper.Map<MstApplication>(dto);
            application.Id = Guid.NewGuid();
            application.ApplicationStatus = 1;

            _context.MstApplications.Add(application);

            // Data default untuk CreatedBy dan UpdatedBy (asumsi sementara)
            string defaultUser = "System"; // Ganti dengan logika autentikasi jika ada
            DateTime now = DateTime.UtcNow;

            // Buat 4 UserGroup dengan LevelPriority = Primary
            var userGroups = new List<UserGroup>
            {
                new UserGroup
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    LevelPriority = LevelPriority.Primary,
                    ApplicationId = application.Id,
                    CreatedBy = defaultUser,
                    CreatedAt = now,
                    UpdatedBy = defaultUser,
                    UpdatedAt = now,
                    Status = 1
                },
                new UserGroup
                {
                    Id = Guid.NewGuid(),
                    Name = "Operator",
                    LevelPriority = LevelPriority.Primary,
                    ApplicationId = application.Id,
                    CreatedBy = defaultUser,
                    CreatedAt = now,
                    UpdatedBy = defaultUser,
                    UpdatedAt = now,
                    Status = 1
                },
                new UserGroup
                {
                    Id = Guid.NewGuid(),
                    Name = "Security",
                    LevelPriority = LevelPriority.Primary,
                    ApplicationId = application.Id,
                    CreatedBy = defaultUser,
                    CreatedAt = now,
                    UpdatedBy = defaultUser,
                    UpdatedAt = now,
                    Status = 1
                },
                new UserGroup
                {
                    Id = Guid.NewGuid(),
                    Name = "Other Primary",
                    LevelPriority = LevelPriority.Primary,
                    ApplicationId = application.Id,
                    CreatedBy = defaultUser,
                    CreatedAt = now,
                    UpdatedBy = defaultUser,
                    UpdatedAt = now,
                    Status = 1
                }
            };

            _context.UserGroups.AddRange(userGroups);

            // Buat 4 User dengan UserGroup yang sesuai
            var users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "TestPrimaryUser1",
                    Password = BCrypt.Net.BCrypt.HashPassword("testprimaryuser123@"),
                    IsCreatedPassword = 1, // Asumsi sudah dibuat
                    Email = "testprimaryuser1@example.com", // Contoh email
                    IsEmailConfirmation = 0, // Belum dikonfirmasi
                    EmailConfirmationCode = Guid.NewGuid().ToString(), // Kode acak
                    EmailConfirmationExpiredAt = now.AddDays(1), // Berlaku 1 hari
                    EmailConfirmationAt = now, // Default
                    LastLoginAt = now, // Default
                    StatusActive = StatusActive.Active,
                    GroupId = userGroups[0].Id // Admin
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "TestPrimaryUser2",
                    Password = BCrypt.Net.BCrypt.HashPassword("testprimaryuser123@"),
                    IsCreatedPassword = 1,
                    Email = "testprimaryuser2@example.com",
                    IsEmailConfirmation = 0,
                    EmailConfirmationCode = Guid.NewGuid().ToString(),
                    EmailConfirmationExpiredAt = now.AddDays(1),
                    EmailConfirmationAt = now,
                    LastLoginAt = now,
                    StatusActive = StatusActive.Active,
                    GroupId = userGroups[1].Id // Operator
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "TestPrimaryUser3",
                    Password = BCrypt.Net.BCrypt.HashPassword("testprimaryuser123@"),
                    IsCreatedPassword = 1,
                    Email = "testprimaryuser3@example.com",
                    IsEmailConfirmation = 0,
                    EmailConfirmationCode = Guid.NewGuid().ToString(),
                    EmailConfirmationExpiredAt = now.AddDays(1),
                    EmailConfirmationAt = now,
                    LastLoginAt = now,
                    StatusActive = StatusActive.Active,
                    GroupId = userGroups[2].Id // Security
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "TestPrimaryUser4",
                    Password = BCrypt.Net.BCrypt.HashPassword("testprimaryuser123@"),
                    IsCreatedPassword = 1,
                    Email = "testprimaryuser4@example.com",
                    IsEmailConfirmation = 0,
                    EmailConfirmationCode = Guid.NewGuid().ToString(),
                    EmailConfirmationExpiredAt = now.AddDays(1),
                    EmailConfirmationAt = now,
                    LastLoginAt = now,
                    StatusActive = StatusActive.Active,
                    GroupId = userGroups[3].Id // Other Primary
                }
            };

            _context.Users.AddRange(users);

            // Simpan semua perubahan dalam satu transaksi
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<MstApplicationDto>(application);
            Console.WriteLine($"Created MstApplication with ID {application.Id}");
            Console.WriteLine($"Created 4 UserGroups: {string.Join(", ", userGroups.Select(ug => ug.Name))}");
            Console.WriteLine($"Created 4 Users: {string.Join(", ", users.Select(u => u.Username))}");
            return resultDto;
        }

        public async Task UpdateApplicationAsync(Guid id, MstApplicationUpdateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var application = await _context.MstApplications.FindAsync(id);
            if (application == null)
                throw new KeyNotFoundException($"MstApplication with ID {id} not found.");

            _mapper.Map(dto, application);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Updated MstApplication with ID {id}");
        }

        public async Task DeleteApplicationAsync(Guid id)
        {
            var application = await _context.MstApplications.FindAsync(id);
            if (application == null)
                throw new KeyNotFoundException($"MstApplication with ID {id} not found.");

            application.ApplicationStatus = 0;
            await _context.SaveChangesAsync();

            Console.WriteLine($"Soft-deleted MstApplication with ID {id}");
        }
    }

}


     // public async Task<MstApplicationDto> CreateApplicationAsync(MstApplicationCreateDto dto)
        // {
        //     if (dto == null) throw new ArgumentNullException(nameof(dto));

        //     var application = _mapper.Map<MstApplication>(dto);
        //     application.Id = Guid.NewGuid();
        //     application.ApplicationStatus = 1;

        //     _context.MstApplications.Add(application);
        //     await _context.SaveChangesAsync();

        //     var resultDto = _mapper.Map<MstApplicationDto>(application);
        //     Console.WriteLine($"Created MstApplication with ID {application.Id}");
        //     return resultDto;
        // }