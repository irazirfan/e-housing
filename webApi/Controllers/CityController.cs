using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;
using webApi.Data.Repo;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository repo;
        public CityController(ICityRepository repo)
        {
            this.repo = repo;
        }

        // GET api/city
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var cities = await repo.getCitiesAsync();
            return Ok(cities);
        }

        // POST api/city/post
        [HttpPost("post")]
        public async Task<IActionResult> AddCity(City city)
        {
            repo.AddCity(city);
            await repo.SaveAsync();
            return StatusCode(201);
        }

        // POST api/city/delete
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            repo.DeleteCity(id);
            await repo.SaveAsync();
            return Ok(id);
        }
    }
}