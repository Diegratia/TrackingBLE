using System;

namespace TrackingBle.Models.Dto.AuthDtos
{
    public class LoginDto
    {
        public string Email { get; set; }
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
}