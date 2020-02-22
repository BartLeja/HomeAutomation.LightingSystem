using LightingSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightingSystem.Domain.HomeLightSystem
{
    public class HomeLightSystem : Entity
    {
        public Guid LocalLightingSystemId { get; set; }
        public string UserName { get; set; }

        public IEnumerable<LightPoint> LightPoints => lightPoints.ToList();
        private List<LightPoint> lightPoints;

        private HomeLightSystem()
        {
            lightPoints = new List<LightPoint>();
        }

        public HomeLightSystem(string username)
        {
            LocalLightingSystemId = Guid.NewGuid();
            UserName = username;
            lightPoints = new List<LightPoint>();
            //TODO context save => TODO unitofwork save => TODO emit event and save ther
        }

        public Guid AddLighPoint(string mqttId, string customName, int numberOfBulbs, List<Bulb> bulbs)
        {
            var lightPoint = new LightPoint(mqttId, customName, numberOfBulbs);
            foreach(var bulb in bulbs ?? Enumerable.Empty<Bulb>())
            {
                lightPoint.AddBulb(bulb);
            }

            lightPoints.Add(lightPoint);
            //TODO context save => TODO unitofwork save => TODO emit event and save there
            return lightPoint.LightPointId;
        }

        public void DisableLighPoint(Guid lightPointId)
        {
            var lightPoint = lightPoints.Where(lp => lp.LightPointId == lightPointId).FirstOrDefault();
            lightPoint.Disable();
            //TODO context save => TODO unitofwork save => TODO emit event and save there
        }

        public void EnableLighPoint(Guid lightPointId)
        {
            var lightPoint = lightPoints.Where(lp => lp.LightPointId == lightPointId).FirstOrDefault();
            lightPoint.Enable();
            //TODO context save => TODO unitofwork save => TODO emit event and save there
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

        public void ChangeBulbStatus(Guid lightPointId, Guid bulbId,bool bulbStatus)
        {
            var lightPoint = lightPoints.Where(lp => lp.LightPointId == lightPointId).FirstOrDefault();
            lightPoint.ChangeBulbStatus(bulbId, bulbStatus);
            //TODO context save => TODO unitofwork save => TODO emit event and save there
        }
    }
}
