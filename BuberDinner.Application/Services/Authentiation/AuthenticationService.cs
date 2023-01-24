using BuberDinner.Application.Common.Interfaces.Authentication;

namespace BuberDinner.Application.Services.Authentiation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public AuthenticationResult Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public AuthenticationResult Register(string email, string password, string firstName, string lastName)
        {
            //check if user already exists
            //else create user

            var userId = Guid.NewGuid();
            var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);

            return new AuthenticationResult { FirstName = firstName, Email = email, Id = Guid.NewGuid(), LastName = lastName, Token = token };
        }
    }
}
