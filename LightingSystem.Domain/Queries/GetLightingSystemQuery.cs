using LightingSystem.Domain.Dtos;
using MediatR;
using System;

namespace LightingSystem.Domain.Queries
{
    //TODO create IComand interferace
    public class GetLightingSystemQuery : IRequest<HomeLightSystemDto>
    {
        public Guid LocalLightingSystemId { get; }

        public GetLightingSystemQuery(Guid localLightingSystemId)
        {
            LocalLightingSystemId = localLightingSystemId;
        }
    }
}
