using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LightingSystem.Data.Models
{
    public class LightPoint
    {
        [Key]
        public Guid LightPointId { get; set; }

        public List<Bulb> Bulbs { get; set; }
        public string MqttId { get; set; }
        public string CustomName { get; set; }
        public bool IsAvailable { get; set; } = true;

        public long LocalLightingSystemId { get; set; }
    }
}
