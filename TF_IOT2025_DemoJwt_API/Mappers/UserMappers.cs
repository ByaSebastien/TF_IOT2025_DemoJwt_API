using TF_IOT2025_DemoJwt_API.Dtos;
using TF_IOT2025_DemoJwt_API.Entities;

namespace TF_IOT2025_DemoJwt_API.Mappers
{
    public static class UserMappers
    {
        public static UserDTO ToUserDTO(this User u)
        {
            return new UserDTO()
            {
                Id = u.Id,
                role = u.Role.ToString(),
                Username = u.Username,
            };
        }

        public static User ToUser(this RegisterFormDTO form)
        {
            return new User()
            {
                Username = form.Username,
                Password = form.Password,
            };
        }

        public static User ToUser(this LoginFormDTO form)
        {
            return new User()
            {
                Username = form.Username,
                Password = form.Password,
            };
        }
    }
}
