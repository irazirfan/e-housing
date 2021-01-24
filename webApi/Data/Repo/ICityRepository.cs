using System.Collections.Generic;
using System.Threading.Tasks;
using webApi.Models;

namespace webApi.Data.Repo
{
    public interface ICityRepository
    {
         Task<IEnumerable<City>> getCitiesAsync();
         void AddCity(City city);
         void DeleteCity(int CityId);
         Task<bool> SaveAsync();
         
    }
}