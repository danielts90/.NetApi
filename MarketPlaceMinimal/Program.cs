using MarketPlaceMinimal.Configurations;
using MarketPlaceMinimal.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureSwagger();
builder.Services.ConfigureDI();
builder.Services.ConfigureAuthentication(builder.Configuration);
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

app.MapUserEndpoints();
app.MapProductsEndpoints();

app.Run();
