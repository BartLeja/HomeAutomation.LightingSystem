using LightingSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightingSystem.Domain.HomeLightSystem
{
    public class LightPoint : Entity
    {
        public Guid LightPointId { get; set; }

        public IEnumerable<Bulb> LightBulbs => lightBulbs.ToList();
        private List<Bulb> lightBulbs;
        private string mqttId;
        private string customName;
        private bool isAvailable;

        private LightPoint()
        {
            isAvailable = true;
            lightBulbs = new List<Bulb>();
        }

        public LightPoint(
            string mqttId, 
            string customName, 
            int numberOfBulbs
            )
        {
            LightPointId = Guid.NewGuid();
            this.mqttId = mqttId;
            this.customName = customName;
            isAvailable = true;
            lightBulbs = new List<Bulb>();
        }

        public void AddBulb(Bulb bulb)
        {
            lightBulbs.Add(bulb);
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
           var bulb = lightBulbs.Where(lb => lb.Id == bulbId).FirstOrDefault();
            bulb.ChangeStatus(bulbStatus);
        }
    }
}
