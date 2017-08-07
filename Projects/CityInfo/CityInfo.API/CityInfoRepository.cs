using CityInfo.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityInfoContext _context;
        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(int cityId, bool inclPOIs)
        {
            if(inclPOIs)
            {
                return _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == cityId).FirstOrDefault();
            }
            return _context.Cities.Where(c => c.Id == cityId).FirstOrDefault();
        }

        public POI GetPOI(int cityId, int poiId)
        {
            return _context.POIs
                    .Where(p => p.cityId == cityId && p.Id == poiId).FirstOrDefault();
        }

        public IEnumerable<POI> GetPOIs(int cityId)
        {
            return _context.POIs
                    .Where(p => p.cityId == cityId).ToList();
        }

        public IEnumerable<POI> UpdatePOI(int cityId, int poiId, POI updatePoi)
        {
            POI basePoi = new POI();
            basePoi = GetPOI(cityId, poiId);
            _context.POIs.Update(basePoi);
            _context.SaveChanges();
            return basePoi;
        }
    }
}
