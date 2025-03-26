using System;
using System.Linq;
using TrackingBle.src._1Auth.Data;
using TrackingBle.src._1Auth.Models.Domain;
using BCrypt.Net;

namespace TrackingBle.src._1Auth.Seeding
{
    public static class DatabaseSeeder
    {
        public static void Seed(AuthDbContext context)
        {
            // UserGroup (3 role: System, Primary, UserCreated)
            if (!context.UserGroups.Any(ug => ug.Status != 0))
            {
                var applicationId = Guid.Parse("D9AF1B06-44B7-4688-9621-572EF0434B36");

                var groups = new[]
                {
                    new UserGroup
                    {
                        Id = Guid.NewGuid(),
                        Name = "System Initial",
                        LevelPriority = LevelPriority.System, // Disimpan sebagai int (0)
                        ApplicationId = applicationId,
                        CreatedBy = "System",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedBy = "System",
                        UpdatedAt = DateTime.UtcNow,
                        Status = 1
                    },
                    new UserGroup
                    {
                        Id = Guid.NewGuid(),
                        Name = "Primary Initial",
                        LevelPriority = LevelPriority.Primary, // Disimpan sebagai int (1)
                        ApplicationId = applicationId,
                        CreatedBy = "System",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedBy = "System",
                        UpdatedAt = DateTime.UtcNow,
                        Status = 1
                    },
                    new UserGroup
                    {
                        Id = Guid.NewGuid(),
                        Name = "UserCreated Initial",
                        LevelPriority = LevelPriority.UserCreated, // Disimpan sebagai int (2)
                        ApplicationId = applicationId,
                        CreatedBy = "System",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedBy = "System",
                        UpdatedAt = DateTime.UtcNow,
                        Status = 1
                    }
                };

                context.UserGroups.AddRange(groups);
                context.SaveChanges();
            }

            // User (hanya systemadmin)
            if (!context.Users.Any(u => u.Username == "systemadmin"))
            {
                var superadminGroup = context.UserGroups
                    .FirstOrDefault(ug => ug.LevelPriority == LevelPriority.System && ug.Status != 0);

                if (superadminGroup != null)
                {
                    var systemAdmin = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "systemadmin",
                        Password = BCrypt.Net.BCrypt.HashPassword("System123@"),
                        IsCreatedPassword = 1,
                        Email = "systemadmin@test.com",
                        IsEmailConfirmation = 1,
                        EmailConfirmationCode = "ABC123",
                        EmailConfirmationExpiredAt = DateTime.UtcNow.AddDays(1),
                        EmailConfirmationAt = DateTime.UtcNow,
                        LastLoginAt = DateTime.MinValue,
                        StatusActive = StatusActive.Active,
                        GroupId = superadminGroup.Id
                    };

                    context.Users.Add(systemAdmin);
                    context.SaveChanges();
                }
            }
        }
    }
}