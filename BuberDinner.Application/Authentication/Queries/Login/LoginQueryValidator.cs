using FluentValidation;

namespace BuberDinner.Application.Authentication.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(obj => obj.Email).NotEmpty();
            RuleFor(obj => obj.Password).NotEmpty();
        }
    }
}
