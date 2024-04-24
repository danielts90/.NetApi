using MarketPlaceBusiness.Autentication;
using MarketPlaceBusiness.Interfaces;
using MarketPlaceBusiness.Services;
using MarketPlaceData.Repository;

namespace MarketPlaceMinimal.Configurations
{
    public static class DepencyInjectionConfig
    {
        public static void ConfigureDI(this IServiceCollection services)
        {
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IAutenticationService, AutenticationService>();

        }
    }
}
