using Coupon_API.Models;
using Newtonsoft.Json;

public class ItemService
{
    public object GetItem(string idItem)
    {
        HttpClient client = new HttpClient();
        string uri = $"https://api.mercadolibre.com/items/{idItem}";

        try
        {
            var response = client.GetStringAsync(uri);

            Item item = JsonConvert.DeserializeObject<Item>(response.Result);
            return item;
        }
        catch (Exception)
        {
            return new
            {
                message = $"Item with id {idItem} not found",
                error = "not_found",
                status = 404,
                cause = ""
            };
        }
    }


}
