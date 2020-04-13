using LightingSystem.Data.Dtos;
using MediatR;
using System.Collections.Generic;

namespace LightingSystem.API.Features.HomeLightingSystem.HomeLightSystemsDataQuery
{
    public class GetLightingSystemsQuery : IRequest<List<HomeLightSystemDto>>
    {
    }
}
