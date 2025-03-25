using Manager_SIMS.Models;

namespace Manager_SIMS.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUserByEmail(string email);


        User AuthenticateUser(string email, string password);
    }
}
