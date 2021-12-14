using System.Threading.Tasks;
using webApi.Models;

namespace webApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);

        void Register(string userName, string password);

        Task<bool> USerAlreadyExists(string userName);
    }
}