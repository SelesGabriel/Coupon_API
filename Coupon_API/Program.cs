using Coupon_API.Data;
using Coupon_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();

//builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql("Server=uyu7j8yohcwo35j3.cbetxkdyhwsb.us-east-1.rds.amazonaws.com;DataBase=qjmyeifpkiy1rj4d;Uid=h90qchpk35uy8xe4;Pwd=pii53w106qhyh35w",
//    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql")));

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

app.MapGet("item", (string idItem) =>
{
    try
    {
        return Results.Ok(itemService.GetItem(idItem));
    }
    catch (global::System.Exception e) 
    {
        return Results.Problem(e.Message);
    }
    
});

app.MapGet("user", (AppDbContext context) =>
{
    return Results.Ok(userService.GetUser(context));
});

app.MapPost("user", (AppDbContext context, User user) =>
{
    return Results.Ok(userService.PostUser(context, user));
});

app.MapPut("doFavorite", (AppDbContext context, string idItem, int idUser) =>
{
    return Results.Ok(favoriteService.DoFavorite(context, idItem, idUser));
});

app.MapPost("coupon", (AppDbContext context, Coupon coupon, int idUser) =>
{
    return Results.Ok(couponService.PostCoupon(context, coupon,idUser));
});

app.MapPost("coupon/stats", () =>
{
    return Results.Ok();
});

app.Run();
