using MarketPlaceBusiness.Autentication;
using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Helpers;
using MarketPlaceBusiness.Interfaces;
using MarketPlaceBusiness.Services;
using MarketPlaceData.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT desta maneira: Bearer {seu token}",
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
             new string[] {}
         }
     });
});


builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IAutenticationService, AutenticationService>();

var appSettingsSection = builder.Configuration.GetSection("AutenticationSettings");
builder.Services.Configure<AutenticationSettings>(appSettingsSection);

var autenticationSettings = appSettingsSection.Get<AutenticationSettings>();
var key = Encoding.ASCII.GetBytes(autenticationSettings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = autenticationSettings.Audience,
        ValidIssuer = autenticationSettings.Issuer
    };
});
builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

var usersGroup = app.MapGroup("/Users");
var productsGroup = app.MapGroup("/Products");

#region UsersGroup
usersGroup.MapGet("/{id:int}", (int id, IUserService userService) => Results.Ok(userService.GetById(id)))
.RequireAuthorization()
.WithTags("Users")
.WithOpenApi();

usersGroup.MapGet("/", (IUserService userService) => Results.Ok(userService.GetAll()))
.RequireAuthorization()
.WithTags("Users")
.WithOpenApi();

usersGroup.MapPost("/Autenticate", (UserDto user, IUserService userService) =>
{
    try
    {
        var token = userService.AutenticateUser(user);
        return Results.Ok(token);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
});

usersGroup.MapPost("/CreateRootUser", (IUserService userService) =>
{
    userService.Insert(new UserDto
    {
        Name = "root",
        Email = "root@teste.com",
        Password = "teste@123",
        ConfirmPassword = "teste@123"
    });
    return userService.GetById(1);
});

usersGroup.MapPost("/", (UserDto user, IUserService userService) =>
{
    return userService.Insert(user);
})
.RequireAuthorization()
.WithTags("Users")
.WithOpenApi();

usersGroup.MapPut("/{id:int}", (int id, UserDto user, IUserService userService) =>
{
    userService.Update(user);
})
.RequireAuthorization()
.WithTags("Users")
.WithOpenApi();

usersGroup.MapDelete("/{id:int}", (int id, IUserService userService) =>
{
    userService.Delete(id);
})
.RequireAuthorization()
.WithTags("Users")
.WithOpenApi();

#endregion
#region ProductsGroup
productsGroup.MapGet("/", (IProductService productService) =>
{
    return productService.GetAll();
})
.RequireAuthorization()
.WithTags("Products")
.WithOpenApi();

productsGroup.MapGet("/{id:int}", (int id, IProductService productService) =>
{
    return productService.GetById(id);
})
.RequireAuthorization()
.WithTags("Products")
.WithOpenApi();

productsGroup.MapPost("/", (ProductDto product, IProductService productService) =>
{
    productService.Insert(product);
})
.RequireAuthorization()
.WithTags("Products")
.WithOpenApi();

productsGroup.MapPut("/{id:int}", (int id, ProductDto product,  IProductService productService) =>
{
    productService.Update(product);
})
.RequireAuthorization()
.WithTags("Products")
.WithOpenApi();

productsGroup.MapDelete("/{id:int}", (int id, IProductService productService) =>
{
    productService.Delete(id);
})
.RequireAuthorization()
.WithTags("Products")
.WithOpenApi();
#endregion

app.Run();
