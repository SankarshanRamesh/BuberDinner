using BuberDinner.Application.Common.Persistance;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrasructure.Persistance
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new();
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public User GetUserByEmail(string email)
        {
            return _users.SingleOrDefault(user => user.Email == email);
        }
    }
}
