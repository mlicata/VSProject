using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            //init Dummy Data
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Desc = "The one with the big park.",
                    PointsOfInterest = new List<POIDto>()
                    {
                        new POIDto()
                        {
                            Id = 1,
                            Name = "POI 1",
                            Desc = "desc of poi 1"
                        },
                        new POIDto()
                        {
                            Id = 2,
                            Name = "POI 2",
                            Desc = "desc of poi 2"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "London",
                    Desc = "The one with the Queen.",
                    PointsOfInterest = new List<POIDto>()
                    {
                        new POIDto()
                        {
                            Id = 1,
                            Name = "POI 1",
                            Desc = "desc of poi 1"
                        },
                        new POIDto()
                        {
                            Id = 2,
                            Name = "POI 2",
                            Desc = "desc of poi 2"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Seattle",
                    Desc = "The one with the needle.",
                    PointsOfInterest = new List<POIDto>()
                    {
                        new POIDto()
                        {
                            Id = 1,
                            Name = "POI 1",
                            Desc = "desc of poi 1"
                        },
                        new POIDto()
                        {
                            Id = 2,
                            Name = "POI 2",
                            Desc = "desc of poi 2"
                        }
                    }
                }
            };
        }
    }
}
