using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Coupon_API.Models
{
    public class Coupon
    {
        public int Id { get; }
        [JsonIgnore]
        public string Name { get; set; }
        [NotMapped,Required]
        public List<string>? ItemIds { get; set; }
        [NotMapped, Required]
        public decimal Amount { get; set; }
    }
}
