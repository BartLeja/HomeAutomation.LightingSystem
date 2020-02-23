﻿using LightingSystem.Domain.HomeLightSystem;
using MediatR;
using System;
using System.Collections.Generic;

namespace LightingSystem.API.Commands
{
    public class AddLightPointCommand: IRequest<Guid>
    {
        public int LightBulbsCount { get; set; }
        public string MqttId { get; set; }
        public string CustomName { get; set; }
        public Guid HomeLightSystemId { get; set; }
        public List<LightBulb> Bulbs { get; set; }

        public AddLightPointCommand(
            string mqttId, 
            string customName,
            Guid homeLightSystemId,
            List<LightBulb> bulbs)
        {
            MqttId = mqttId;
            CustomName = customName;
            HomeLightSystemId = homeLightSystemId;
            Bulbs = bulbs;
        }
    }
}