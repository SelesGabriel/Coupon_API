using Coupon_API.Data;
using Coupon_API.IServices;
using Coupon_API.Models;
using Coupon_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "CatalogoApi", Version = "v1" });
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = @"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space]. Example: \'Bearer 12345abcdef'\",
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddDbContext<AppDbContext>();

//add token authentication
builder.Services.AddSingleton<ITokenService>(new TokenService());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();


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
}).WithTags("Item").RequireAuthorization();
#endregion

#region User
app.MapGet("user", (AppDbContext context) =>
{
    return Results.Ok(userService.GetUser(context));
}).WithTags("User");

app.MapGet("user/{id}", (AppDbContext context, int id) =>
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

#region Login
app.MapPost("login", [AllowAnonymous] (UserLogin userLogin, ITokenService tokenService) =>
 {
     if (userLogin == null)
         return Results.BadRequest("Invalid Login");
     if (userLogin.UserName == "admin" && userLogin.Password == "admin")
     {
         var tokenString = tokenService.GerarToken(app.Configuration["Jwt:Key"],
             app.Configuration["Jwt:Issuer"],
             app.Configuration["Jwt:Audience"], userLogin);
         return Results.Ok(new { token = tokenString });
     }
     return Results.BadRequest("Invalid Login");
 }).WithTags("Authentication");
#endregion

app.UseAuthentication();
app.UseAuthorization();
app.Run();
