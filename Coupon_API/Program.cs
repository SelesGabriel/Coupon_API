using Coupon_API.Data;
using Coupon_API.Models;
using Coupon_API.Services;

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

#endregion
app.MapGet("user", (AppDbContext context) => 
{ 
    return Results.Ok(userService.GetUser(context)); 
});
app.MapPost("user",(AppDbContext context, User user) => { return Results.Ok(userService.PostUser(context,user)); });
//app.MapPut("doFavorite", (AppDbContext context, User user, int id, int idItem) =>
// {
//     return Results.Ok(userService.DoFavorite(context, user, id,idItem));  
// });
app.MapGet("item", (string idItem) => { return Results.Ok(itemService.GetItem(idItem)); });
app.MapPost("coupon", () => { return Results.Ok(); });
app.MapGet("favorites", () => { return Results.Ok(); });
app.MapPost("addfavorite", () => { return Results.Ok(); });

app.Run();
