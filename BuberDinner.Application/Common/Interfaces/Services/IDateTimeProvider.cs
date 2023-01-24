namespace BuberDinner.Application.Common.Interfaces.Services
{
    public interface IDateTimeProvider
    {
        DateTime Utcnow { get; }
    }
}
