using Coupon_API.Data;
using Coupon_API.Models;

namespace Coupon_API.Services
{
    public class UserService
    {
        public List<User>? GetUser(AppDbContext context)
        {
            return context.Users.ToList();
        }
        public User GetUser(AppDbContext context, int id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }
        public User PostUser(AppDbContext context, User user)
        {
            List<User> listUser = GetUser(context);
            foreach (var newUser in listUser)
            {
                if (user.Email == newUser.Email)
                    throw new Exception("Error: Email already exists");
            }
            context.Add(user);
            context.SaveChanges();
            return user;
        }
        //public object DoFavorite(AppDbContext context, User user, int idUser, int idItem)
        //{
        //    //user.Favorites.Add(item)

        //    //return user;
        //}

        public bool UndoFavorite()
        {
            return false;
        }
    }
}
