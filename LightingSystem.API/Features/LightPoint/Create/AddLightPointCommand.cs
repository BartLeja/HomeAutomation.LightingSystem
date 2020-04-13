using LightingSystem.Domain.HomeLightSystem;
using MediatR;
using System;
using System.Collections.Generic;

namespace LightingSystem.API.Features.LightPoint.Create
{
    public class AddLightPointCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string CustomName { get; set; }
        public Guid HomeLightSystemId { get; set; }
        public List<LightBulb> Bulbs { get; set; }

        public AddLightPointCommand(
             Guid id,
            string customName,
            Guid homeLightSystemId,
            List<LightBulb> bulbs)
        {
            Id = id;
            CustomName = customName;
            HomeLightSystemId = homeLightSystemId;
            Bulbs = bulbs;
        }
    }
}
