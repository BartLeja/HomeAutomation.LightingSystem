using LightingSystem.Data.Dtos;
using MediatR;
using System.Collections.Generic;

namespace LightingSystem.API.Queries
{
    public class GetLightingSystemsQuery : IRequest<List<HomeLightSystemDto>>
    {
    }
}
