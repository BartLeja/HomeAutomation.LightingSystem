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

        private List<LightPoint> LightPoints { get; set; }

        public HomeLightSystem(string username)
        {
            LocalLightingSystemId = Guid.NewGuid();
            UserName = username;
            LightPoints = new List<LightPoint>();
            //TODO context save => TODO unitofwork save => TODO emit event and save ther
        }

        public Guid AddLighPoint(string mqttId, string customName, int numberOfBulbs)
        {
            var lightPoint = new LightPoint(mqttId, customName, numberOfBulbs);
            LightPoints.Add(lightPoint);
            //TODO context save => TODO unitofwork save => TODO emit event and save there
            return lightPoint.LightPointId;
        }

        public void DisableLighPoint(Guid lightPointId)
        {
            var lightPoint = LightPoints.Where(lp => lp.LightPointId == lightPointId).FirstOrDefault();
            lightPoint.Disable();
            //TODO context save => TODO unitofwork save => TODO emit event and save there
        }

        public void EnableLighPoint(Guid lightPointId)
        {
            var lightPoint = LightPoints.Where(lp => lp.LightPointId == lightPointId).FirstOrDefault();
            lightPoint.Enable();
            //TODO context save => TODO unitofwork save => TODO emit event and save there
        }


        public void DisableAllLighPoints()
        {
            LightPoints.ForEach((lp) =>
            {
                lp.Disable();
            });
            //TODO context save => TODO unitofwork save => TODO emit event and save there
        }

        public void EnableAllLighPoints()
        {
            LightPoints.ForEach((lp) =>
            {
                lp.Enable();
            });
            //TODO context save => TODO unitofwork save => TODO emit event and save there
        }

        public void ChangeBulbStatus(Guid lightPointId, Guid bulbId,bool bulbStatus)
        {
            var lightPoint = LightPoints.Where(lp => lp.LightPointId == lightPointId).FirstOrDefault();
            lightPoint.ChangeBulbStatus(bulbId, bulbStatus);
            //TODO context save => TODO unitofwork save => TODO emit event and save there
        }
    }
}
