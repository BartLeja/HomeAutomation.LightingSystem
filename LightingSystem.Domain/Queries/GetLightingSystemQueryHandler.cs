using LightingSystem.Data.Repositories;
using LightingSystem.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.Domain.Queries
{
    public class GetLightingSystemQueryHandler : IRequestHandler<GetLightingSystemQuery, HomeLightSystemDto>
    {
        private readonly IHomeLightSystemRepository _homeLightSystemRepository;

        public GetLightingSystemQueryHandler(IHomeLightSystemRepository homeLightSystemRepository)
        {
            _homeLightSystemRepository = homeLightSystemRepository;
        }
        public async Task<HomeLightSystemDto> Handle(GetLightingSystemQuery request, CancellationToken cancellationToken)
        {
            var homeLightSystem =  await _homeLightSystemRepository.GetByIdAsync(request.LocalLightingSystemId);

            //TODO proper validation with Fluent Validation
            if (homeLightSystem == null)
            {
                Console.WriteLine("home is not a home");
            }



            return new HomeLightSystemDto() { LightPoints = new List<HomeLightSystem.LightPoint>() };
        }
    }
}
