using System;
using System.Collections.Generic;

namespace LightingSystem.Data.Dtos
{
    public class LightPointDto
    {
        public Guid LightPointId { get; set; }
        public int LightBulbsCount { get; set; }
        public string MqttId { get; set; }
        public string CustomName { get; set; }
        public bool IsAvailable { get; set; }
        public List<BulbDto> LightBulbs { get; set;}
    }
}
