using Manager_SIMS.Models;

namespace Manager_SIMS.Facades
{
    public interface IUserFacade
    {
        string RegisterUser(string fullName, string email, string password, int roleId, string address, string phoneNumber);

        User LoginUser(string email, string password);
    }

}