using LightingSystem.Domain.Common;
using LightingSystem.Domain.HomeLightSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightingSystem.Domain.Models
{
    public class LightsGroup: Entity
    {
        public Guid Id { get; set; }

        public string LightGroupName => lightGroupName;
        private string lightGroupName;

        public IEnumerable<LightPoint> LightPoints => lightPoints.ToList();
        private List<LightPoint> lightPoints;

        public LightsGroup() {
            lightPoints = new List<LightPoint>();
        }

        public LightsGroup(Guid id)
        {
            Id = id;
            lightPoints = new List<LightPoint>();
        }

        public LightsGroup(Guid id, string lightGroupName)
        {
            Id = id;
            this.lightGroupName = lightGroupName;
            lightPoints = new List<LightPoint>();
        }
    }
}
