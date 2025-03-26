namespace TrackingBle.src._1Auth.Models.Dto.AuthDtos
{
    public class LoginDto
    {
        public string Username { get; set; } // Ubah dari Email ke Username
        public string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public string Token { get; set; }
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Guid GroupId { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string StatusActive { get; set; }
    }

    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid GroupId { get; set; }
    }

    public class UserGroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LevelPriority { get; set; }
        public Guid ApplicationId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? Status { get; set; }
    }
}