using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The city 'name' is too long")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "The city 'description' is too long")]
        public string Desc { get; set; }

        public ICollection<POI> PointsOfInterest { get; set; } = new List<POI>();
    }
}
