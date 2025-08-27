using TF_IOT2025_DemoJwt_API.Enums;

namespace TF_IOT2025_DemoJwt_API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
