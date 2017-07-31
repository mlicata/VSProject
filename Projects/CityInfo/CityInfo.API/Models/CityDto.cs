using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public int NumberOfPointsOfInterest {
            get { return PointsOfInterest.Count; }
        }

        public ICollection<POIDto> PointsOfInterest { get; set; } = new List<POIDto>();
    }
}
