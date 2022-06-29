using JWT.api.Domain.Entities;
using JWT.api.Request;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.api.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string ValidateCredentials(LoginModel loginModel)
        {
            var user = GetSimulatedFindInDatabase(loginModel);
            if (user is null)
                throw new Exception("Datos de usuario incorrectos");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.EMail),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = configuration.GetValue<string>("Authentication:JWT:Key");

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(configuration.GetValue<int>("Authentication:JWT:ExpirationHours")),
                SigningCredentials = signingCredentials,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(securityTokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        private UserData GetSimulatedFindInDatabase(LoginModel loginModel) => new UserData { Id = 1, UserName = "usuario", Password = "contraseña", FirstName = "Pruebas", EMail = "usuario@dominio.com", Role = "administrator" };
    }
}
