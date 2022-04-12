namespace Coupon_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public string Email { get; set; }
        public List<Item> Favorites { get; }
    }
}
