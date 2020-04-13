using MediatR;
using System;

namespace LightingSystem.API.Features.LightPoint.Disable
{
    public class DisableLighPointCommand : IRequest<Guid>
    {
        public Guid LightPointId { get; set; }

        public DisableLighPointCommand(
            Guid lightPointId
        ) => LightPointId = lightPointId;
    }
}
