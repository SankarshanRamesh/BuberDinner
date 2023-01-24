using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Persistance;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentiation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            //validate if user exists
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User with given email does not exists!");
            }

            //validate password
            if (user.Password != password)
            {
                throw new Exception("Incorrect password.");
            }

            //create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }

        public AuthenticationResult Register(string email, string password, string firstName, string lastName)
        {
            //check if user already exists
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                throw new Exception("User with given email already exists!");
            }
            //else create user
            var user = new User()
            {
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName
            };
            _userRepository.AddUser(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
