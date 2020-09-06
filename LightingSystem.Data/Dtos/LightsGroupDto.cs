using System;

namespace LightingSystem.Data.Dtos
{
    public class LightsGroupDto
    {
        public Guid Id { get; set; }
        public string LightGroupName { get; set; }
        public Guid LightPointId { get; set; }
    }
}
