using MarketPlaceBusiness.Autentication;
using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Interfaces;
using MarketPlaceBusiness.Services;
using MarketPlaceData.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IAutenticationService, AutenticationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var usersGroup = app.MapGroup("/Users");
var productsGroup = app.MapGroup("/Products");

#region UsersGroup
usersGroup.MapGet("/{id:int}", (int id, IUserService userService) =>
{
    return (userService.GetById(id));
})
.WithTags("Users")
.WithOpenApi();

usersGroup.MapGet("/", (IUserService userService) =>
{
    return (userService.GetAll());
})
.WithTags("Users")
.WithOpenApi();


usersGroup.MapPost("/", (UserDto user, IUserService userService) =>
{
    return userService.Insert(user);
})
.WithTags("Users")
.WithOpenApi();

usersGroup.MapPut("/{id:int}", (int id, UserDto user, IUserService userService) =>
{
    userService.Update(user);
})
.WithTags("Users")
.WithOpenApi();

usersGroup.MapDelete("/{id:int}", (int id, IUserService userService) =>
{
    userService.Delete(id);
})
.WithTags("Users")
.WithOpenApi();

#endregion
#region ProductsGroup
productsGroup.MapGet("/", (IProductService productService) =>
{
    return productService.GetAll();
})
.WithTags("Products")
.WithOpenApi();

productsGroup.MapGet("/{id:int}", (int id, IProductService productService) =>
{
    return productService.GetById(id);
})
.WithTags("Products")
.WithOpenApi();

productsGroup.MapPost("/", (ProductDto product, IProductService productService) =>
{
    productService.Insert(product);
})
.WithTags("Products")
.WithOpenApi();

productsGroup.MapPut("/{id:int}", (int id, ProductDto product,  IProductService productService) =>
{
    productService.Update(product);
})
.WithTags("Products")
.WithOpenApi();

productsGroup.MapDelete("/{id:int}", (int id, IProductService productService) =>
{
    productService.Delete(id);
})
.WithTags("Products")
.WithOpenApi();
#endregion

app.Run();
