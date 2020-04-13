using LightingSystem.Data.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.API.Features.HomeLightingSystem.EnableAllLightPoints
{
    public class EnableAllLightPointsCommandHandler : IRequestHandler<EnableAllLightPointsCommand, Guid>
    {
        private IHomeLightSystemRepository _homeLightSystemRepository;

        public EnableAllLightPointsCommandHandler(IHomeLightSystemRepository homeLightSystemRepository) => _homeLightSystemRepository = homeLightSystemRepository ?? throw new ArgumentNullException(nameof(homeLightSystemRepository));

        public async Task<Guid> Handle(EnableAllLightPointsCommand request, CancellationToken cancellationToken)
        {
            var homeLightSystem = await _homeLightSystemRepository.GetByIdAsync(request.HomeLightSystemId);

            homeLightSystem.EnableAllLighPoints();

            _homeLightSystemRepository.ChangeLighPointAvailability(homeLightSystem);
            await _homeLightSystemRepository.Save();

            return request.HomeLightSystemId;
        }
    }
}
