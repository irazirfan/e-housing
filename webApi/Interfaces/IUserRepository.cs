using System.Threading.Tasks;
using webApi.Models;

namespace webApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string username, string password);
    }
}