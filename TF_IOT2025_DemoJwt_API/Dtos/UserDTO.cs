using System.ComponentModel.DataAnnotations;
using TF_IOT2025_DemoJwt_API.Enums;

namespace TF_IOT2025_DemoJwt_API.Dtos
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string role { get; set; } = null!;
    }

    public class UserTokenDTO
    {
        public UserDTO User { get; set; } = null!;
        public string Token { get; set; } = null!;
    }

    public class RegisterFormDTO
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }

    public class LoginFormDTO
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
