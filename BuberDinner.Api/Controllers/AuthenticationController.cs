using BuberDinner.Application.Services.Authentiation;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            ErrorOr<AuthenticationResult> authResult = _authenticationService.Register(registerRequest.Email, registerRequest.Password, registerRequest.FirstName, registerRequest.LastName);

            return authResult.Match(authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var authResult = _authenticationService.Login(loginRequest.Email, loginRequest.Password);

            if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized,
                    title: authResult.FirstError.Description);
            }

            return authResult.Match(authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }

        private static AuthentcationResponse MapAuthResult(AuthenticationResult authResult)
        {
            return new AuthentcationResponse
                        (
                            authResult.user.Id,
                            authResult.user.FirstName,
                            authResult.user.LastName,
                            authResult.user.Email,
                            authResult.Token
                        );
        }
    }
}
