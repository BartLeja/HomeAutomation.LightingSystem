using LightingSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightingSystem.Domain.HomeLightSystem
{
    public class HomeLightSystem : Entity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public IEnumerable<LightPoint> LightPoints => lightPoints.ToList();
        private List<LightPoint> lightPoints;

        private HomeLightSystem()
        {
            lightPoints = new List<LightPoint>();
        }

        public HomeLightSystem(string username)
        {
            Id = Guid.NewGuid();
            UserName = username;
            lightPoints = new List<LightPoint>();
            //TODO context save => TODO unitofwork save => TODO emit event and save ther

        }

        public Guid AddLighPoint(Guid id, string customName, List<LightBulb> lightBulbs)
        {
            var lightPoint = new LightPoint(id,customName, lightBulbs.Count());
            foreach(var lightBulb in lightBulbs ?? Enumerable.Empty<LightBulb>())
            {
                lightPoint.AddLightBulb(lightBulb);
            }

            lightPoints.Add(lightPoint);
            //TODO context save => TODO unitofwork save => TODO emit event and save there
            return lightPoint.Id;
        }

        public void DisableAllLighPoints()
        {
            lightPoints.ForEach((lp) =>
            {
                lp.Disable();
            });
            //TODO context save => TODO unitofwork save => TODO emit event and save there
        }

        public void EnableAllLighPoints()
        {
            lightPoints.ForEach((lp) =>
            {
                lp.Enable();
            });
            //TODO context save => TODO unitofwork save => TODO emit event and save there
        }
    }
}
