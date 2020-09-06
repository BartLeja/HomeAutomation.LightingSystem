using LightingSystem.Domain.Common;
using LightingSystem.Domain.Models;
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
        public LightsGroup LightsGroup => lightsGroup;
        private LightsGroup lightsGroup;

        public string CustomName => customName;
        private string customName;
        public bool IsAvailable => isAvailable;
        private bool isAvailable;

        private LightPoint()
        {
            isAvailable = true;
            lightBulbs = new List<LightBulb>();
            lightsGroup = new LightsGroup();
        }

        public LightPoint(
            Guid id,
            string customName, 
            int numberOfBulbs
            )
        {
            Id = id;
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

        public void ChangeLightPointStatus(bool bulbStatus)
        {
            lightBulbs.ForEach(lb => lb.ChangeStatus(bulbStatus));
        }

        public void AddLightsGroup(LightsGroup lightsGroup)
        {
            this.lightsGroup = lightsGroup;
        }

        public void RemoveLightsGroup(LightsGroup lightsGroup)
        {
            //TODO move to.net core and null the LightsGroup in LihtPoint
            this.lightsGroup = lightsGroup;
        }
    }
}
