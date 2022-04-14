
using Coupon_API.Data;
using Coupon_API.Models;

public class FavoriteService
{
    public string DoFavorite(AppDbContext context, string idItem, int idUser)
    {
        ItemService itemService = new ItemService();
        Item item = (Item)itemService.GetItem(idItem);
        List<FavoriteItems> favoriteItems = context.FavoriteItems.Where(x => x.IdUser == idUser).ToList();

        foreach (var fav in favoriteItems)
        {
            if (fav.IdItem == idItem)
                return $"The item {idItem} already exists";
        }
        FavoriteItems newFavorite = new FavoriteItems() { IdItem = idItem, IdUser = idUser, Amount = item.Price };
        context.FavoriteItems.Add(newFavorite);
        context.SaveChanges();
        return $"Item {idItem} was added in your favorites";
    }

    public string UndoFavorite(AppDbContext context, string idItem, int idUser)
    {
        ItemService itemService = new ItemService();
        List<FavoriteItems> favoriteItems = context.FavoriteItems.Where(x => x.IdUser == idUser).ToList();
        FavoriteItems favoriteItem = new();
        foreach (var fav in favoriteItems)
        {
            context.FavoriteItems.Remove(fav);
            context.SaveChanges();
            return $"Item {idItem} was removed.";
        }
        return $"The item {idItem} does not exists.";
    }

    public List<object> GetTopFavorites(AppDbContext context)
    {
        var list = new List<object>();
        foreach (var item in context.FavoriteItems.GroupBy(x => x.IdItem).Select(select => new
        { idItem = select.First().IdItem, Qtd = select.Count() }).OrderByDescending(order => order.Qtd).Take(5))
        {
            list.Add(item);
        }
        return list;
    }
}

