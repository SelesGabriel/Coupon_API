using Coupon_API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class FavoriteItems
{
    [JsonIgnore]
    public int Id { get; }
    public string? IdItem { get; set; }
    [JsonIgnore]
    public int IdUser { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; }
    public decimal Amount { get; set; }
}
