

using Coupon_API.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Coupon_API.Models
{
    public class User
    {

        public int IdUser { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? Email { get; set; }
        public ICollection<FavoriteItems>? Favorites
        {
            get
            {
                AppDbContext context = new();

                return context.FavoriteItems.Where(f => f.IdUser == IdUser).ToList();
            }
        }
    }
}