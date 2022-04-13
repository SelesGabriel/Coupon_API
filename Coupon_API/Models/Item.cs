using Newtonsoft.Json;

namespace Coupon_API.Models
{
    public class Item
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
