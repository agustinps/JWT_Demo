using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JWT.api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddJWTValidation(this IServiceCollection services, IConfiguration configuration)
        {
            var key = configuration.GetValue<string>("Authentication:JWT:Key");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,  //el emisor es el servidor que creó el token
                    ValidateAudience = false, //el receptor es un destinatario válido
                    //ValidIssuer = "https://localhost:7058",  //emisor del token
                    //ValidAudience = "https://localhost:5001",
                    ValidateIssuerSigningKey = true, //la clave de firma es válida y el servidor confía en ella                                                             
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),  //clave de firma con la que se generó el token
                    ValidateLifetime = true,  //el token no ha caducado
                    RequireExpirationTime = true,  //requiere el tiempo de expiración
                };
            });
        }

    }
}
