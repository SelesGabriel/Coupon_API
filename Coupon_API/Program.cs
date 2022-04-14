using Coupon_API.Data;
using Coupon_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();


builder.Services.AddCors(options => options
.AddPolicy("CorsPolicy", a => a.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));

var app = builder.Build();
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Variables
UserService userService = new();
ItemService itemService = new();
FavoriteService favoriteService = new();
CouponService couponService = new();

#endregion

#region Item
app.MapGet("item", (string idItem) =>
{
    return Results.Ok(itemService.GetItem(idItem));
}).WithTags("Item");
#endregion

#region User
app.MapGet("user", (AppDbContext context) =>
{
    return Results.Ok(userService.GetUser(context));
}).WithTags("User");

app.MapGet("user/{id}", (AppDbContext context,int id) =>
{
    return Results.Ok(userService.GetUser(context, id));
}).WithTags("User");

app.MapPost("user", (AppDbContext context, User user) =>
{
    return Results.Ok(userService.PostUser(context, user));
}).WithTags("User"); ;
#endregion

#region Favorite


app.MapPut("doFavorite", (AppDbContext context, string idItem, int idUser) =>
{
    return Results.Ok(favoriteService.DoFavorite(context, idItem, idUser));
}).WithTags("Favorite");

app.MapPut("undoFavorite", (AppDbContext context, string idItem, int idUser) =>
 {
     return Results.Ok(favoriteService.UndoFavorite(context, idItem, idUser));
 }).WithTags("Favorite");

app.MapPost("coupon/stats", (AppDbContext context) =>
{
    return Results.Ok(favoriteService.GetTopFavorites(context));
}).WithTags("Favorite");
#endregion

#region Coupon
app.MapPost("coupon", (AppDbContext context, Coupon coupon, int idUser) =>
{
    return Results.Ok(couponService.PostCoupon(context, coupon, idUser));
}).WithTags("Coupon");
#endregion

app.Run();
