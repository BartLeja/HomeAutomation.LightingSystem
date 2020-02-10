using LightingSystem.Domain.HomeLightSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightingSystem.Domain.Dtos
{
    public class HomeLightSystemDto
    {
        public List<LightPoint> LightPoints { get; set; }
    }
}
