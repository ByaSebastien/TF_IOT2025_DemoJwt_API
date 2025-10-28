namespace TF_IOT2025_DemoJwt_API.Entities
{
    public class House
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<User> Users { get; set; } = null!;
    }
}
