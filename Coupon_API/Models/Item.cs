using Newtonsoft.Json;

namespace Coupon_API.Models
{
    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
    }
}
