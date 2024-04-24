using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Interfaces;

namespace MarketPlaceMinimal.Endpoints
{
    public static class ProductsEndpointsExtension
    {
        public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
        {
            var productsGroup = app.MapGroup("/Products");

            productsGroup.MapGet("/", HandleGetAll)
            .RequireAuthorization()
            .WithTags("Products")
            .WithOpenApi();

            productsGroup.MapGet("/{id:int}",HandleGetById)
            .RequireAuthorization()
            .WithTags("Products")
            .WithOpenApi();

            productsGroup.MapPost("/", HandlePost)
            .RequireAuthorization()
            .WithTags("Products")
            .WithOpenApi();

            productsGroup.MapPut("/{id:int}", HandlePut)
            .RequireAuthorization()
            .WithTags("Products")
            .WithOpenApi();

            productsGroup.MapDelete("/{id:int}", HandleDelete)
            .RequireAuthorization()
            .WithTags("Products")
            .WithOpenApi();
        }
        public static IResult HandleGetAll(IProductService productService) => Results.Ok(productService.GetAll());
        public static IResult HandleGetById(int id, IProductService productService) => Results.Ok(productService.GetById(id));
        public static IResult HandlePost(ProductDto product, IProductService productService)
        {
            try
            {
                productService.Insert(product);
                return Results.Accepted();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }
        public static IResult HandlePut(ProductDto product, IProductService productService)
        {
            try
            {
                productService.Update(product);
                return Results.Accepted();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }
        public static IResult HandleDelete(int id, IProductService productService)
        {
            try
            {
                productService.Delete(id);
                return Results.Accepted();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }
    }
}
