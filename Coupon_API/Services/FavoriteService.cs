
using Coupon_API.Data;
using Coupon_API.Models;

public class FavoriteService
{
    public string DoFavorite(AppDbContext context, string idItem, int idUser)
    {
        ItemService itemService = new ItemService();
        Item item = (Item)itemService.GetItem(idItem);
        List<FavoriteItems> favoriteItems = context.FavoriteItems.Where(x => x.IdUser == idUser).ToList();

        foreach(var fav in favoriteItems)
        {
            if (fav.IdItem == idItem)
                return "opa, esse aqui já existe";
        }
        FavoriteItems newFavorite = new FavoriteItems() { IdItem = idItem, IdUser = idUser, Amount =  item.Price};
        context.FavoriteItems.Add(newFavorite);
        context.SaveChanges();
        return $"Item {idItem} adicionado com sucesso!";
    }

    public bool UndoFavorite()
    {
        return false;
    }
}

