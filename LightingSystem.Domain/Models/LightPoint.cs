using LightingSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightingSystem.Domain.HomeLightSystem
{
    public class LightPoint : Entity
    {
        public Guid Id { get; set; }

        public IEnumerable<LightBulb> LightBulbs => lightBulbs.ToList();
        private List<LightBulb> lightBulbs;
        private string mqttId;
        private string customName;
        private bool isAvailable;

        private LightPoint()
        {
            isAvailable = true;
            lightBulbs = new List<LightBulb>();
        }

        public LightPoint(
            string mqttId, 
            string customName, 
            int numberOfBulbs
            )
        {
            Id = Guid.NewGuid();
            this.mqttId = mqttId;
            this.customName = customName;
            isAvailable = true;
            lightBulbs = new List<LightBulb>();
        }

        public void AddLightBulb(LightBulb lightBulb)
        {
            lightBulbs.Add(lightBulb);
        }

        public void Disable()
        {
            isAvailable = false;
        }

        public void Enable()
        {
            isAvailable = true;
        }

        public void ChangeBulbStatus(Guid bulbId,bool bulbStatus)
        {
           var lightBulb = lightBulbs.Where(lb => lb.Id == bulbId).FirstOrDefault();
            lightBulb.ChangeStatus(bulbStatus);
        }
    }
}
