using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentiation
{
    public record AuthenticationResult
    (
        User user,
        string Token
    );
}
