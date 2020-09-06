using MediatR;
using System;
using System.Collections.Generic;

namespace LightingSystem.API.Features.HomeLightingSystem.ChangeLightPointsGroupStatus
{
    public class ChangeLightPointsGroupStatusCommand : IRequest<IEnumerable<Guid>>
    {
        public Guid HomeLightSystemId { get; set; }
        public Guid LightPointsGroupId { get; set; }
        public bool Status { get; set; }

        public ChangeLightPointsGroupStatusCommand(
            Guid homeLightSystemId,
            Guid lightPointsGroupId,
            bool status)
        {
            HomeLightSystemId = homeLightSystemId;
            LightPointsGroupId = lightPointsGroupId;
            Status = status;
        }
    }
}
