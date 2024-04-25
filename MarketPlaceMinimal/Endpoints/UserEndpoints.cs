using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Interfaces;

namespace MarketPlaceMinimal.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
           app.MapGroup("/Users")
              .MapEndpoints()
              .RequireAuthorization()
              .WithTags("Users")
              .WithOpenApi();
        }

        public static IResult HandleGetAll(IUserService userService) => TypedResults.Ok(userService.GetAll());
        public static IResult HandleGetById(int id, IUserService userService) => TypedResults.Ok(userService.GetById(id));
        public static IResult HandlePost(UserDto user, IUserService userService)
        {
            try
            {
                userService.Insert(user);
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex);
            }
        }
        public static IResult HandlePut(UserDto user, IUserService userService)
        {
            try
            {
                userService.Update(user);
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex);
            }
        }
        public static IResult HandleDelete(int id, IUserService userService)
        {
            try
            {
                userService.Delete(id);
                return TypedResults.NoContent();
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex);
            }
        }
        public static IResult HandleAutenticate(UserDto user, IUserService userService)
        {
            try
            {
                var token = userService.AutenticateUser(user);
                return TypedResults.Ok(token);
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex);
            }
        }
        public static IResult HandleCreateRootUser(IUserService userService)
        {
            var userId = userService.Insert(new UserDto
            {
                Name = "root",
                Email = "root@teste.com",
                Password = "teste@123",
                ConfirmPassword = "teste@123"
            });
            return TypedResults.Ok(userService.GetById(userId));
        }
        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", HandleGetAll);
            group.MapGet("/{id}", HandleGetById);
            group.MapPost("/", HandlePost);
            group.MapPut("/{id}", HandlePut);
            group.MapDelete("/{id}", HandleDelete);
            group.MapPost("/Autenticate", HandleAutenticate).AllowAnonymous();
            group.MapPost("/CreateRootUser", HandleCreateRootUser).AllowAnonymous();

            return group;
        }

    }
}
