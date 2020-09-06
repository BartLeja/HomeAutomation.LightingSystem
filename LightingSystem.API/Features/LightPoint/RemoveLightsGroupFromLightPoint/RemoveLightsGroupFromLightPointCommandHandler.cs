using LightingSystem.Data.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.API.Features.LightPoint.RemoveLightsGroupFromLightPoint
{
    public class RemoveLightsGroupFromLightPointCommandHandler : IRequestHandler<RemoveLightsGroupFromLightPointCommand, Guid>
    {
        private IHomeLightSystemRepository _homeLightSystemRepository;

        public RemoveLightsGroupFromLightPointCommandHandler(IHomeLightSystemRepository homeLightSystemRepository)
            => _homeLightSystemRepository = homeLightSystemRepository
            ?? throw new ArgumentNullException(nameof(homeLightSystemRepository));

        public async Task<Guid> Handle(RemoveLightsGroupFromLightPointCommand request, CancellationToken cancellationToken)
        {
            var lightPoint = await _homeLightSystemRepository.GetLightPointByIdAsync(request.LightPointId);
            //TODO Check if any lightPoint is in selected group and if not remove group
            var lightsGroup = await _homeLightSystemRepository.GetLightsGroupByIdAsync(Guid.Parse("6f69b22d-d70c-4b43-a50f-0e167523c2a6"));
            lightPoint.RemoveLightsGroup(lightsGroup);
            await _homeLightSystemRepository.UpdateLightPoint(lightPoint);
            await _homeLightSystemRepository.Save();

            return request.LightPointId;
        }
    }
}
