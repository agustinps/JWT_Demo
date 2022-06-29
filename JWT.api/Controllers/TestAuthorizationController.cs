using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWT.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAuthorizationController : ControllerBase
    {
        
        [HttpGet("[action]")]
        public ActionResult AuthenticationFree() => Ok("Método sin autenticación");

        [HttpGet("[action]")]
        [Authorize]
        public ActionResult AuthenticationRequired() => Ok("Método con autenticación requerida correcta");

        [HttpGet("[action]")]
        [Authorize]
        //Podemos obtener los claims de nuestro usuario y recuperar información para realizar alguna operación
        public ActionResult AuthenticationRequiredV2() {
            var userName = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
            var email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
            var role = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);

            //Tambien podemos recuperar esta información del token de la cabecera
            //string jwtToken = Request.Headers["Authorization"].ToString().Substring(7);
            //var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);

            //según la funcionalidad deseada, podemos enviar al cliente a la pagina de logín si el token caduca o podemos generar un nuevo token (RefreshToken) para ampliar la caducidad y enviarlo en al respuesta

            return Ok(new { nombre = userName, correo = email, role = role });
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "administrator")]    
        //Nuestro ejemplo tiene este role por lo que tendrá acceso a este método una vez autenticado
        public ActionResult AuthorizationRoleAdminRequired() => Ok("Método con autenticación requerida y autorización de Role 'administrator' correcta");

        [HttpGet("[action]")]
        [Authorize(Roles = "user")]    
        //Nuestro ejemplo NO tiene este role por lo que NO tendrá acceso a este método aunque esté autenticado
        public ActionResult AuthorizationRoleUserRequired() => Ok("Método con autenticación requerida y autorización de Role 'user' correcta");
    }
}
