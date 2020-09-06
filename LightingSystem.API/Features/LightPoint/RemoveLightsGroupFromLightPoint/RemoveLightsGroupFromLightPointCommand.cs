using MediatR;
using System;

namespace LightingSystem.API.Features.LightPoint.RemoveLightsGroupFromLightPoint
{
    public class RemoveLightsGroupFromLightPointCommand : IRequest<Guid>
    {
        public Guid LightPointId { get; set; }

        public RemoveLightsGroupFromLightPointCommand(Guid lightPointId) 
        {
            LightPointId = lightPointId;
        }
    }
}
