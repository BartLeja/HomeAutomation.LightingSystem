using LightingSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightingSystem.Domain.HomeLightSystem
{
    public class LightPoint : Entity
    {
        public Guid LightPointId { get; set; }

        private List<Bulb> LightBulbs { get; set; }
        private string MqttId { get; set; }
        private string CustomName { get; set; }
        private bool IsAvailable { get; set; } = true;

        public LightPoint(
            string mqttId, 
            string userName, 
            int numberOfBulbs)
        {
            LightPointId = Guid.NewGuid();
            MqttId = mqttId;
            CustomName = userName;
            IsAvailable = true;
        }

        public void Disable()
        {
            IsAvailable = false;
        }

        public void Enable()
        {
            IsAvailable = true;
        }

        public void ChangeBulbStatus(Guid bulbId,bool bulbStatus)
        {
           var bulb = LightBulbs.Where(lb => lb.Id == bulbId).FirstOrDefault();
            bulb.ChangeStatus(bulbStatus);
        }
    }
}
