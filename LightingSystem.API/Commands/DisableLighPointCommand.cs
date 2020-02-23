using MediatR;
using System;

namespace LightingSystem.API.Commands
{
    public class DisableLighPointCommand : IRequest<Guid>
    {
        public Guid LightPointId { get; set; }

        public DisableLighPointCommand(
            Guid lightPointId
        )
        {
            LightPointId = lightPointId;
        }
    }
}
