using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CityDto : CityWithoutPOIsDto
    {
        public int NumberOfPointsOfInterest {
            get { return PointsOfInterest.Count; }
        }

        public ICollection<POIDto> PointsOfInterest { get; set; } = new List<POIDto>();
    }
}
