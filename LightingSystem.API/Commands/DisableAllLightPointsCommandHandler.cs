using LightingSystem.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.API.Commands
{
    public class DisableAllLightPointsCommandHandler : IRequestHandler<DisableAllLightPointsCommand, Guid>
    {
        private IHomeLightSystemRepository _homeLightSystemRepository;

        public DisableAllLightPointsCommandHandler(IHomeLightSystemRepository homeLightSystemRepository)
        {
            _homeLightSystemRepository = homeLightSystemRepository ?? throw new ArgumentNullException(nameof(homeLightSystemRepository));
        }

        public async Task<Guid> Handle(DisableAllLightPointsCommand request, CancellationToken cancellationToken)
        {
            var homeLightSystem = await _homeLightSystemRepository.GetByIdAsync(request.HomeLightSystemId);

            homeLightSystem.DisableAllLighPoints();

            _homeLightSystemRepository.ChangeLighPointAvailability(homeLightSystem);
            await _homeLightSystemRepository.Save();

            return request.HomeLightSystemId;
        }
    }
}
