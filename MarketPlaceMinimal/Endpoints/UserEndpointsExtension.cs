using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Interfaces;

namespace MarketPlaceMinimal.Endpoints
{
    public static class UserEndpointsExtension
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var usersGroup = app.MapGroup("/Users");

            usersGroup.MapGet("/{id:int}", HandleGetById)
                .RequireAuthorization()
                .WithTags("Users")
                .WithOpenApi();

            usersGroup.MapGet("/", HandleGetAll)
                .RequireAuthorization()
                .WithTags("Users")
                .WithOpenApi();

            usersGroup.MapPost("/Autenticate", HandleAutenticate)
                .WithTags("Users")
                .WithOpenApi();

            usersGroup.MapPost("/CreateRootUser", HandleCreateRootUser)
                .WithTags("Users")
                .WithOpenApi();

            usersGroup.MapPost("/", HandlePost)
                .RequireAuthorization()
                .WithTags("Users")
                .WithOpenApi();

            usersGroup.MapPut("/{id:int}", HandlePut)
                .RequireAuthorization()
                .WithTags("Users")
                .WithOpenApi();

            usersGroup.MapDelete("/{id:int}", HandleDelete)
                .RequireAuthorization()
                .WithTags("Users")
                .WithOpenApi();
        }

        public static IResult HandleGetAll(IUserService userService) => Results.Ok(userService.GetAll());
        public static IResult HandleGetById(int id, IUserService userService) => Results.Ok(userService.GetById(id));
        public static IResult HandlePost(UserDto user, IUserService userService)
        {
            try
            {
                userService.Insert(user);
                return Results.Accepted();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }
        public static IResult HandlePut(UserDto user, IUserService userService)
        {
            try
            {
                userService.Update(user);
                return Results.Accepted();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }
        public static IResult HandleDelete(int id, IUserService userService)
        {
            try
            {
                userService.Delete(id);
                return Results.Accepted();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }
        public static IResult HandleAutenticate(UserDto user, IUserService userService)
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
            return Results.Ok(userService.GetById(userId));
        }

    }
}
