using LightingSystem.Data.Dtos;
using MediatR;
using System;

namespace LightingSystem.API.Queries
{
    public class GetLightPointQuery : IRequest<LightPointDto>
    {
        public Guid Id { get; }

        public GetLightPointQuery(Guid id)
        {
            Id = id;
        }
    }
}