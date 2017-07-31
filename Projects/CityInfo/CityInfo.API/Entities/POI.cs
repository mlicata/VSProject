using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class POI
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("cityId")]
        public City City { get; set; }
        public int cityId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The POI 'name' is too long")]
        public string Name { get; set; }
    }
}
