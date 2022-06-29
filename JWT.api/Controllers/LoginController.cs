using JWT.api.Application.Services;
using JWT.api.Request;
using Microsoft.AspNetCore.Mvc;

namespace JWT.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        public ActionResult DoLogin(LoginModel loginModel) => Ok(authenticationService.ValidateCredentials(loginModel));

    }
}
