using Coupon_API.Data;
using Coupon_API.Models;


public class UserService
{
    public List<User>? GetUser(AppDbContext context)
    {

        return context.Users.ToList();
    }
    public User GetUser(AppDbContext context, int id)
    {
        return context.Users.FirstOrDefault(x => x.IdUser == id);
    }
    public User PostUser(AppDbContext context, User user)
    {
        List<User> listUser = GetUser(context);
        user.RegistrationDate = DateTime.UtcNow;
        //foreach (var newUser in listUser)
        //{
        //    if (user.Email == newUser.Email)
        //        throw new Exception("Error: Email already exists");
        //}
        context.Add(user);
        context.SaveChanges();
        return user;
    }
}
