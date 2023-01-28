namespace BuberDinner.Contracts.Authentication
{
    public record AuthentcationResponse
    (
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Token
    );
}
