
using Coupon_API.Data;
using Coupon_API.Models;
using System.Text;

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
        var item_ids = result.Item1;
        var total = coupon.Amount - result.Item2;

        return new
        {
            item_ids = item_ids,
            total = total
        };
    }

    public Tuple<List<string>, decimal> Combinations(List<FavoriteItems> favorites, Coupon coupon)
    {
        var valMax = coupon.Amount;
        decimal valorSoma = 0;
        List<FavoriteItems> result = new();
        List<string> possibilidades = new();
        var tuple = new List<Tuple<List<string>, decimal>>();

        for(int i = 0; i< favorites.Count; i++)
        {
            if(favorites[i].Amount + valorSoma < valMax)
            {
                valorSoma += favorites[i].Amount;
                possibilidades.Add(favorites[i].IdItem);
            }
            for(int j = 0; j < favorites.Count; j++)
            {
                if (i != j)
                {
                    if(valorSoma + favorites[j].Amount < valMax)
                    {
                        valorSoma += favorites[j].Amount;
                        possibilidades.Add(favorites[j].IdItem);
                    }
                    else
                    {
                        tuple.Add(Tuple.Create(possibilidades, valorSoma));
                        possibilidades = new List<string>();
                        valorSoma = 0;
                    }
                }
            }
        }
        return tuple.OrderByDescending(t => t.Item2).FirstOrDefault();
    }
}