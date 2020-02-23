using MediatR;
using System;

namespace LightingSystem.API.Commands
{
    public class EnableLightPointCommand : IRequest<Guid>
    {
        public Guid LightPointId { get; set; }

        public EnableLightPointCommand(
             Guid lightPointId
            )
        {
            LightPointId = lightPointId;
        }
    }
}
