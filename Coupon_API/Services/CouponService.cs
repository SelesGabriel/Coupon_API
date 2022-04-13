
using Coupon_API.Data;
using Coupon_API.Models;

public class CouponService
{
    public object PostCoupon(AppDbContext context, Coupon coupon, int idUser)
    {
        List<FavoriteItems> favoriteList = context.FavoriteItems.Where(f => f.IdUser == idUser).ToList();
        List<FavoriteItems> newFavoriteList = new();
        foreach (var favorite in coupon.ItemIds)
        {
            if (favoriteList.Any(x => x.IdItem == favorite))
                newFavoriteList.Add(favoriteList.Where(x=> x.IdItem == favorite).First());
        }


        var result = Combinations(newFavoriteList, coupon);


        return new
        {
            item_ids = newFavoriteList,
            total = 20
        };
    }

    public List<FavoriteItems> Combinations(List<FavoriteItems> favorites, Coupon coupon)
    {
        var valMax = coupon.Amount;
        decimal valorSoma = 0;
        string descricaoSoma = "";
        FavoriteItems favoriteitems = new();
        List<FavoriteItems> result = new();
        List<string> possibilidades = new();

        //List<decimal> favorites = new() { 10, 20, 30, 40, 50, 60 };

        for (int i = 0; i < favorites.Count; i++)
        {
            if (valorSoma + favorites[i].Amount <= valMax)
            {
                
                descricaoSoma = $"{descricaoSoma} ({favorites[i].IdItem} {favorites[i].Amount}) + {valorSoma} value {favorites[i].Amount + valorSoma}";
                valorSoma = valorSoma + favorites[i].Amount;
            }
            for (int j = 0; j < favorites.Count; j++)
            {
                if (i != j)
                {
                    if (valorSoma + favorites[j].Amount <= valMax)
                    {
                        descricaoSoma = $"{descricaoSoma} (({favorites[i].IdItem} {favorites[j].Amount}) + {valorSoma} value {favorites[j].Amount + valorSoma}";
                        valorSoma = valorSoma + favorites[j].Amount;

                    }

                    else
                    {
                        possibilidades.Add(descricaoSoma);
                        descricaoSoma = "";
                        valorSoma = 0;
                    }
                }


            }
        }
        List<string> teste = new();
        


        return new List<FavoriteItems>();
    }

}

