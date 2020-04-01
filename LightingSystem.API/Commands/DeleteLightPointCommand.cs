using MediatR;
using System;

namespace LightingSystem.API.Commands 
{
    public class DeleteLightPointCommand : IRequest<Guid>
    {
        public Guid LightPointId { get; set; }

        public DeleteLightPointCommand(
              Guid lightPointId
            )
        {
            LightPointId = lightPointId;
        }
    }
}
