using BuberDinner.Application.Services.Authentiation;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Route("register")]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            var authResult = _authenticationService.Register(registerRequest.Email, registerRequest.Password, registerRequest.FirstName, registerRequest.LastName);
            var response = new AuthentcationResponse() { Email = authResult.Email, FirstName = authResult.FirstName, LastName = authResult.LastName, Token = authResult.Token, Id = authResult.Id };
            return Ok(response);
        }

        [Route("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var authResult = _authenticationService.Login(loginRequest.Email, loginRequest.Password);
            var response = new AuthentcationResponse() { Email = authResult.Email, FirstName = authResult.FirstName, LastName = authResult.LastName, Token = authResult.Token, Id = authResult.Id };
            return Ok(response);
        }
    }
}
