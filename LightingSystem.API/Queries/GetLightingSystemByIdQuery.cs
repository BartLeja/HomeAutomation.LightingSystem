using LightingSystem.Data.Dtos;
using MediatR;
using System;

namespace LightingSystem.API.Queries
{
    //TODO create IComand interferace
    public class GetLightingSystemByIdQuery : IRequest<HomeLightSystemDto>
    {
        public Guid LocalLightingSystemId { get; }

        public GetLightingSystemByIdQuery(Guid localLightingSystemId)
        {
            LocalLightingSystemId = localLightingSystemId;
        }
    }
}
