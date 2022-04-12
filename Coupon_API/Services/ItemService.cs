using Coupon_API.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Coupon_API.Services
{
    public class ItemService
    {
        public object GetItem(string idItem)
        {


            using (var client = new HttpClient())
            {
                string uri = $"https://api.mercadolibre.com/items/{idItem}";
                client.DefaultRequestHeaders.Clear();
                var response = client.GetStringAsync(uri);
                if (response.Status == TaskStatus.Faulted)
                {
                    return new
                    {
                        message = $"Item with id {idItem} not found",
                        error = "not_found",
                        status = 404,
                        cause = ""
                    };
                }
                Item item = JsonConvert.DeserializeObject<Item>(response.Result);
                return item;
            }
        }


    }
}
