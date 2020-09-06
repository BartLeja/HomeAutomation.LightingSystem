using MediatR;
using System;

namespace LightingSystem.API.Features.LightPoint.AddLightsGroupToLightPoint
{
    public class AddLightsGroupToLightPointCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string LightGroupName { get; set; }
        public Guid LightPointId { get; set; }
       
        public AddLightsGroupToLightPointCommand(
            Guid id,
            string lightGroupName,
            Guid lightPointId
            )
        {
            Id = id;
            LightGroupName = lightGroupName;
            LightPointId = lightPointId;
        }
    }
}
