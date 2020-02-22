using System;
using System.Collections.Generic;

namespace LightingSystem.Data.Dtos
{
    public class HomeLightSystemDto
    {
        public Guid LocalLightingSystemId { get; set; }
        public List<LightPointDto> LightPoints { get; set; }
    }
}
