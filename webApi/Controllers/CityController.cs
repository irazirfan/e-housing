using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;
using webApi.Interfaces;
using webApi.Dtos;
using System;
using AutoMapper;
using System.Collections.Generic;

namespace webApi.Controllers
{
    public class CityController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET api/city
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            //throw new UnauthorizedAccessException();
            var cities = await uow.CityRepository.getCitiesAsync();
            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(citiesDto);
        }

        // POST api/city/post
        [HttpPost("post")]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = mapper.Map<City>(cityDto);
            city.LastUpdatedBy = 1;
            city.LastUpdatedOn = DateTime.Now;
            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        // PUT api/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
        {
            if(id != cityDto.Id)
                return BadRequest("Update not allowed");

            var cityFromDB = await uow.CityRepository.FindCity(id);
            
            if(cityFromDB == null)
                return BadRequest("Update not allowed");

            cityFromDB.LastUpdatedBy = 1;
            cityFromDB.LastUpdatedOn = DateTime.Now;
            mapper.Map(cityDto, cityFromDB);

            //throw new Exception("Some unknow error occured");
            await uow.SaveAsync();
            return StatusCode(200);
        }

        // PUT api/updateCityName/{id}
        [HttpPut("updateCityName/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityUpdateDto cityDto)
        {
            var cityFromDB = await uow.CityRepository.FindCity(id);
            cityFromDB.LastUpdatedBy = 1;
            cityFromDB.LastUpdatedOn = DateTime.Now;
            mapper.Map(cityDto, cityFromDB);
            await uow.SaveAsync();
            return StatusCode(200);
        }

        // PATCH api/update/{id}
        // [HttpPatch("update/{id}")]
        // public async Task<IActionResult> UpdateCityPatch(int id, JsonPatchDocument<City> cityToPatch)
        // {
        //     var cityFromDB = await uow.CityRepository.FindCity(id);
        //     cityFromDB.LastUpdatedBy = 1;
        //     cityFromDB.LastUpdatedOn = DateTime.Now;
        //     cityToPatch.ApplyTo(cityFromDB, ModelState);
        //     await uow.SaveAsync();
        //     return StatusCode(200);
        // }

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