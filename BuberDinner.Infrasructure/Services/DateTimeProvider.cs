using BuberDinner.Application.Common.Interfaces.Services;

namespace BuberDinner.Infrasructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Utcnow => DateTime.UtcNow;
    }
}
