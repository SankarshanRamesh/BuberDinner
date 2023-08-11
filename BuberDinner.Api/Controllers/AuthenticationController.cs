using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var command = _mapper.Map<RegisterCommand>(registerRequest);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match(authResult => Ok(_mapper.Map<AuthentcationResponse>(authResult)),
                errors => Problem(errors));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var query = _mapper.Map<LoginQuery>(loginRequest);
            var authResult = await _mediator.Send(query);

            if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized,
                    title: authResult.FirstError.Description);
            }

            return authResult.Match(authResult => Ok(_mapper.Map<AuthentcationResponse>(authResult)),
                errors => Problem(errors));
        }
    }
}
