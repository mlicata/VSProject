using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/")]
    public class POIController : Controller
    {
        private ILogger<POIController> _logger;

        public POIController(ILogger<POIController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{cityId}/pois")]
        public IActionResult GetPOIs(int cityId)
        {
            try
            {
                var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                return Ok(city.PointsOfInterest);
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
                CityDto city = new CityDto();
                city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                var poi = city.PointsOfInterest.FirstOrDefault(p => p.Id == poiId);
                if (poi == null)
                {
                    _logger.LogInformation($"POI with id {cityId} wasn't found when accessing points of interest.");
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
                if (poi == null)
                {
                    _logger.LogInformation($"Cannot create POI. The POI details provided were invalid.");
                    return BadRequest();
                }

                if (poi.Name == poi.Desc)
                {
                    _logger.LogInformation($"Cannot create new POI for CityId: {cityId}. The Name and Description are identical.");
                    ModelState.AddModelError("Description", "The Name and Description must be different.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Cannot create POI. The Model was invalid");
                    return BadRequest(ModelState);
                }


                CityDto city = new CityDto();
                city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                var maxPOI = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);
                POIDto finalPOI = new POIDto()
                {
                    Id = maxPOI++,
                    Name = poi.Name,
                    Desc = poi.Desc,
                };
                city.PointsOfInterest.Add(finalPOI);

                return NoContent();
                //return CreatedAtRoute("GetPOI", finalPOI);
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
                if (poi == null)
                {
                    _logger.LogInformation($"Cannot update PoiId: {poiId}. The POI details provided were invalid.");
                    return BadRequest();
                }

                if (poi.Name == poi.Desc)
                {
                    _logger.LogInformation($"Cannot update CityId: {cityId} and PoiID: {poiId}. The Name and Description are identical.");
                    ModelState.AddModelError("Description", "The Name and Description must be different.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Cannot update POI. The Model was invalid");
                    return BadRequest(ModelState);
                }


                CityDto city = new CityDto();
                city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                POIDto updatePoi = new POIDto();
                updatePoi = city.PointsOfInterest.FirstOrDefault(p => p.Id == poiId);

                if (updatePoi == null)
                {
                    _logger.LogInformation($"POI with id {poiId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

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
                city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                POIDto storedPoi = new POIDto();
                storedPoi = city.PointsOfInterest.FirstOrDefault(p => p.Id == poiId);

                if (storedPoi == null)
                {
                    _logger.LogInformation($"POI with id {poiId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                POIUpdateDto patchPoi = new POIUpdateDto()
                {
                    Name = storedPoi.Name,
                    Desc = storedPoi.Desc
                };
                patchDoc.ApplyTo(patchPoi, ModelState);

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Cannot partially update POI. The Model was invalid");
                    return BadRequest(ModelState);
                }

                if (patchPoi.Name == patchPoi.Desc)
                {
                    _logger.LogInformation($"Cannot update CityId: {cityId} and PoiID: {poiId}. The Name and Description are identical.");
                    ModelState.AddModelError("Description", "The Name and Description must be different.");
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
                city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                POIDto storedPoi = new POIDto();
                storedPoi = city.PointsOfInterest.FirstOrDefault(p => p.Id == poiId);

                if (storedPoi == null)
                {
                    _logger.LogInformation($"POI with id {poiId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                city.PointsOfInterest.Remove(storedPoi);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while trying to delete PoiId: {poiId} for CityID: {cityId}");
                return StatusCode(500, $"A problem happened while trying to delete PoiId: {poiId} for CityID: {cityId}");
            }
        }
    }
}
