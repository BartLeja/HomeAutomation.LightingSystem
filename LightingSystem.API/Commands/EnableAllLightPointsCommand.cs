using MediatR;
using System;

namespace LightingSystem.API.Commands
{
    public class EnableAllLightPointsCommand : IRequest<Guid>
    {
        public Guid HomeLightSystemId { get; set; }

        public EnableAllLightPointsCommand(Guid homeLightSystemId)
        {
            HomeLightSystemId = homeLightSystemId;
        }
    }
}
