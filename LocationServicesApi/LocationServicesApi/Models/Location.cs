using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocationServicesApi.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        public float Longitude { get; set; }

        [Required]
        public float Latitude { get; set; }

        public DateTime Date { get; set; }

        public string Address { get; set; }

        [Required]
        public int DeviceId { get; set; }

        public virtual Device Device { get; set; }
    }
}
