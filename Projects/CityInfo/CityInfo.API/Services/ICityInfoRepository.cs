﻿using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();
        City GetCity(int cityId, bool inclPOIs);

        IEnumerable<POI> GetPOIs(int cityId);
        POI GetPOI(int cityId, int poiId);
    }
}
