using System.Threading.Tasks;

namespace webApi.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }
         Task<bool> SaveAsync();
    }
}