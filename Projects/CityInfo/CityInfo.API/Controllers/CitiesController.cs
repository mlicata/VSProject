using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CityInfo.API.Services;
using CityInfo.API.Models;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet()]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();
            var results = new List<CityWithoutPOIsDto>();
            foreach (var cityEntity in cityEntities)
            {
                results.Add(new CityWithoutPOIsDto
                {
                    Id = cityEntity.Id,
                    Desc = cityEntity.Desc,
                    Name = cityEntity.Name
                });
            }
            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {   
            var cityEntities = _cityInfoRepository.GetCities();
            var returnCity = new CityWithoutPOIsDto();
            foreach (var cityEntity in cityEntities)
            {
                if (cityEntity.Id == id)
                {
                    returnCity = new CityWithoutPOIsDto
                    {
                        Id = cityEntity.Id,
                        Desc = cityEntity.Desc,
                        Name = cityEntity.Name
                    };
                    break;
                }
            }

            if (returnCity == null)
            {
                return NotFound();
            }
            return Ok(returnCity);
        }
    }
}
 