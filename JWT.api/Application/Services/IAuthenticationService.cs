using JWT.api.Request;

namespace JWT.api.Application.Services
{
    public interface IAuthenticationService
    {
        string ValidateCredentials(LoginModel loginModel);
    }
}
