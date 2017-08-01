using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/")]
    public class POIController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;

        private ILogger<POIController> _logger;

        public POIController(ILogger<POIController> logger, ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("{cityId}/pois")]
        public IActionResult GetPOIs(int cityId)
        {
            try
            {
                var pois = _cityInfoRepository.GetPOIs(cityId);
                if (pois == null)
                {
                    return NotFound();
                }
                return Ok(pois);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while trying to get POIs for CityID: {cityId}");
                return StatusCode(500, $"A problem happened while fetching POIs for CityID: {cityId}");
            }

        }

        [HttpGet("{cityId}/pois/{PoiID}", Name = "GetPOI")]
        public IActionResult GetPOI(int cityId, int poiId)
        {
            try
            {
                var poi = _cityInfoRepository.GetPOI(cityId, poiId);
                if (poi == null)
                {
                    return NotFound();
                }
                return Ok(poi);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while trying to get PoiId: {poiId} for CityID: {cityId}");
                return StatusCode(500, $"A problem happened while fetching PoiId: {poiId} for CityID: {cityId}");
            }
        }

        [HttpPost("{cityId}/pois/")]
        public IActionResult CreatePOI(int cityId, [FromBody] POICreationDto poi)
        {
            try{
                if (!poiCheck(poi))
                    BadRequest();

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Cannot create POI. The Model was invalid");
                    return BadRequest(ModelState);
                }


                CityDto city = new CityDto();
                city = getCityDto(cityId);

                if (city == null)
                    return NotFound();

                var maxPOI = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);
                POIDto finalPOI = new POIDto()
                {
                    Id = maxPOI++,
                    Name = poi.Name,
                    Desc = poi.Desc,
                };
                city.PointsOfInterest.Add(finalPOI);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while trying to create a POI for CityID: {cityId}");
                return StatusCode(500, $"A problem happened while creating a POI for CityID: {cityId}");
            }
        }

        [HttpPut("{cityId}/pois/{poiId}")]
        public IActionResult UpdatePOI(int cityId, int poiId, [FromBody] POIUpdateDto poi)
        {
            try
            {
                if (!poiCheck(poi))
                    BadRequest();

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Cannot update POI. The Model was invalid");
                    return BadRequest(ModelState);
                }

                CityDto city = new CityDto();
                city = getCityDto(cityId);

                if (city == null)
                    return NotFound();

                POIDto updatePoi = new POIDto();
                updatePoi = getPOIDto(city, poiId);

                if (poi == null)
                    return NotFound();

                updatePoi.Name = poi.Name;
                updatePoi.Desc = poi.Desc;

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while trying to update PoiId: {poiId} for CityID: {cityId}");
                return StatusCode(500, $"A problem happened while trying to update PoiId: {poiId} for CityID: {cityId}");
            }
        }

        [HttpPatch("{cityId}/pois/{poiId}")]
        public IActionResult PartiallyUpdatePOI(int cityId, int poiId, [FromBody] JsonPatchDocument<POIUpdateDto> patchDoc)
        {
            try
            {
                if (patchDoc == null)
                {
                    _logger.LogInformation($"Cannot update PoiId: {poiId}. The POI details provided were invalid.");
                    return BadRequest();
                }

                CityDto city = new CityDto();
                city = getCityDto(cityId);

                if (city == null)
                    return NotFound();

                POIDto storedPoi = new POIDto();
                storedPoi = getPOIDto(city, poiId);

                if (storedPoi == null)
                    return NotFound();

                POIUpdateDto patchPoi = new POIUpdateDto()
                {
                    Name = storedPoi.Name,
                    Desc = storedPoi.Desc
                };
                patchDoc.ApplyTo(patchPoi, ModelState);

                if (!poiCheck(patchPoi))
                    BadRequest();

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Cannot partially update POI. The Model was invalid");
                    return BadRequest(ModelState);
                }

                TryValidateModel(patchPoi);
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Cannot partially create POI (v2). The Model was invalid");
                    return BadRequest(ModelState);
                }

                storedPoi.Name = patchPoi.Name;
                storedPoi.Desc = patchPoi.Desc;

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while trying to update PoiId: {poiId} for CityID: {cityId}");
                return StatusCode(500, $"A problem happened while trying to update PoiId: {poiId} for CityID: {cityId}");
            }
        }

        [HttpDelete("{cityId}/pois/{poiId}")]
        public IActionResult DeletePOI(int cityId, int poiId)
        {
            try
            {
                CityDto city = new CityDto();
                city = getCityDto(cityId);

                if (city == null)
                    return NotFound();

                POIDto storedPoi = new POIDto();
                storedPoi = getPOIDto(city, poiId);

                if (storedPoi == null)
                    return NotFound();

                city.PointsOfInterest.Remove(storedPoi);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while trying to delete PoiId: {poiId} for CityID: {cityId}");
                return StatusCode(500, $"A problem happened while trying to delete PoiId: {poiId} for CityID: {cityId}");
            }
        }

        private CityDto getCityDto(int cityId)
        {
            CityDto tempDto = new CityDto();
            tempDto = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (tempDto == null)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
            }
            return tempDto;
        }

        private POIDto getPOIDto(CityDto city, int poiId)
        {
            POIDto tempDto = new POIDto();
            tempDto = city.PointsOfInterest.FirstOrDefault(p => p.Id == poiId);
            if (tempDto == null)
            {
                _logger.LogInformation($"POI with id {poiId} wasn't found when accessing points of interest.");
            }
            return tempDto;
        }

        private bool poiCheck(POIDto poi)
        {
            if (poi == null)
            {
                _logger.LogInformation($"Cannot create temp POI. The POI details provided were invalid.");
                return false;
            }

            if (poi.Name != null && poi.Desc != null && poi.Name == poi.Desc)
            {
                _logger.LogInformation($"Cannot create POI. The Name and Description are identical.");
                ModelState.AddModelError("Description", "The Name and Description must be different.");
                return false;
            }
            return true;
        }
    }
}
