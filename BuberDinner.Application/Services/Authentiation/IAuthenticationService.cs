using ErrorOr;

namespace BuberDinner.Application.Services.Authentiation
{
    public interface IAuthenticationService
    {
        ErrorOr<AuthenticationResult> Register(string email, string password, string firstName, string lastName);
        ErrorOr<AuthenticationResult> Login(string email, string password);
    }
}
