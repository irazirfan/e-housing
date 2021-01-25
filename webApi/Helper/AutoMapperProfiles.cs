using AutoMapper;
using webApi.Dtos;
using webApi.Models;

namespace webApi.Helper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}