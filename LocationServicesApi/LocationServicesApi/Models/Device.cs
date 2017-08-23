using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LocationServicesApi.Models
{
    public class Device
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required, MinLength(3)]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        public float? Longitude { get; set; }

        public float? Latitude { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }
    }
}
