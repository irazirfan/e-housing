using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;
using webApi.Interfaces;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        public CityController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        // GET api/city
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var cities = await uow.CityRepository.getCitiesAsync();
            return Ok(cities);
        }

        // POST api/city/post
        [HttpPost("post")]
        public async Task<IActionResult> AddCity(City city)
        {
            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        // POST api/city/delete
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            uow.CityRepository.DeleteCity(id);
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}