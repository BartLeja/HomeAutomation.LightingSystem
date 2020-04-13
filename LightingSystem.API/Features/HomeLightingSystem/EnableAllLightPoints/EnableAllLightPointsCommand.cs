using MediatR;
using System;

namespace LightingSystem.API.Features.HomeLightingSystem.EnableAllLightPoints
{
    public class EnableAllLightPointsCommand : IRequest<Guid>
    {
        public Guid HomeLightSystemId { get; set; }

        public EnableAllLightPointsCommand(Guid homeLightSystemId) => HomeLightSystemId = homeLightSystemId;
    }
}
