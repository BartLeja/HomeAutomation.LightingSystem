using LightingSystem.Data.Dtos;
using MediatR;
using System;

namespace LightingSystem.API.Features.LightPoint.LightPointDataQuery
{
    public class GetLightPointQuery : IRequest<LightPointDto>
    {
        public Guid Id { get; }

        public GetLightPointQuery(Guid id) => Id = id;
    }
}