using System.ComponentModel.DataAnnotations;

namespace TF_IOT2025_DemoJwt_API.Dtos
{
    public class HouseAddDto
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
