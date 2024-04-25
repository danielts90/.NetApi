using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Interfaces;

namespace MarketPlaceMinimal.Endpoints
{
    public static class ProductsEndpoints
    {
        public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGroup("/Products")
               .MapEndpoints()
               .RequireAuthorization()
               .WithTags("Products")
               .WithOpenApi();
        }
        public static IResult HandleGetAll(IProductService productService) => TypedResults.Ok(productService.GetAll());
        public static IResult HandleGetById(int id, IProductService productService) => TypedResults.Ok(productService.GetById(id));
        public static IResult HandlePost(ProductDto product, IProductService productService)
        {
            try
            {
                var id = productService.Insert(product);
                return TypedResults.Accepted($"{id}");
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex);
            }
        }
        public static IResult HandlePut(ProductDto product, IProductService productService)
        {
            try
            {
                productService.Update(product);
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex);
            }
        }
        public static IResult HandleDelete(int id, IProductService productService)
        {
            try
            {
                productService.Delete(id);
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex);
            }
        }
        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", HandleGetAll);
            group.MapGet("/{id}", HandleGetById);
            group.MapPost("/", HandlePost);
            group.MapPut("/{id}", HandlePut);
            group.MapDelete("/{id}", HandleDelete);

            return group;
        }
    }
}
