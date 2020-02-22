using LightingSystem.Data.Dtos;
using MediatR;
using System;

namespace LightingSystem.API.Queries
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
