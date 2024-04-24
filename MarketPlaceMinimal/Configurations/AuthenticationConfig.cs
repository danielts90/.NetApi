using MarketPlaceBusiness.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MarketPlaceMinimal.Configurations
{
    public static class AuthenticationConfig
    {
        public static void ConfigureAuthentication(this IServiceCollection services, ConfigurationManager config) 
        {
            var appSettingsSection = config.GetSection("AutenticationSettings");
            services.Configure<AutenticationSettings>(appSettingsSection);

            var autenticationSettings = appSettingsSection.Get<AutenticationSettings>();
            var key = Encoding.ASCII.GetBytes(autenticationSettings.Secret);

            services.AddAuthentication(x =>
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
        }
    }
}
