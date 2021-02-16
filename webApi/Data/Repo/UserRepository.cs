using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webApi.Interfaces;
using webApi.Models;

namespace webApi.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dc;
        public UserRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            return await dc.Users.FirstOrDefaultAsync(u=> u.Username == username && u.Password == password);
        }
    }
}