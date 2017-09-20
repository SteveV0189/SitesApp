using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SitesApp.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Sensor> Sensors { get; set; }
    }
    public class Sensor
    {
        [Key]
        public string SensorName { get; set; }
        public int SensorTypeId { get; set; }
        public int SiteId { get; set; }
        public ICollection<Observation> Observations { get; set; }
    }
    public class Observation
    {
        public int Id { get; set; }
        public string SensorName { get; set; }
        public float ObsValue { get; set; }
        public DateTime ObsDateTime { get; set; }
    }
}
