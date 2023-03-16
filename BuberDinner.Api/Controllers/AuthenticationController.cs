using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;

        public AuthenticationController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var command = new RegisterCommand(registerRequest.FirstName,
                registerRequest.LastName, registerRequest.Email, registerRequest.Password);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match(authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var query = new LoginQuery(loginRequest.Email, loginRequest.Password);
            var authResult = await _mediator.Send(query);

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
