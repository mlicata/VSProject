using CityInfo.API.Entities;
using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public static class CityInfoContextExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            if(context.Cities.Any())
            { return; }

            List<City> newCities = new List<City>()
            {
                new City()
                {
                    Name = "New York City",
                    Desc = "The one with the big park.",
                    PointsOfInterest = new List<POI>()
                    {
                        new POI()
                        {
                            Name = "Central Park",
                            Desc = "The big park in the middle of the city"
                        },
                        new POI()
                        {
                            Name = "Times Square",
                            Desc = "The place with all the bright billboards"
                        }
                    }
                },
                new City()
                {
                    Name = "London",
                    Desc = "The one with the Queen.",
                    PointsOfInterest = new List<POI>()
                    {
                        new POI()
                        {
                            Name = "Hyde Park",
                            Desc = "The place with the pretty gas lamps at night"
                        },
                        new POI()
                        {
                            Name = "The Grenadier",
                            Desc = "The pub that is hidden except for those who know where to look"
                        }
                    }
                },
                new City()
                {
                    Name = "Seattle",
                    Desc = "The one with the needle.",
                    PointsOfInterest = new List<POI>()
                    {
                        new POI()
                        {
                            Name = "Space Needle",
                            Desc = "The big needle from the World's Fair"
                        },
                        new POI()
                        {
                            Name = "Benaroya Hall",
                            Desc = "The place to hear pretty music"
                        }
                    }
                }
            };

            context.Cities.AddRange(newCities);
            context.SaveChanges();
        }
    }
}
