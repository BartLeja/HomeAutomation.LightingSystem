using LightingSystem.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.API.Features.HomeLightingSystem.ChangeLightPointsGroupStatus
{
    public class ChangeLightPointsGroupStatusCommandHandler : IRequestHandler<ChangeLightPointsGroupStatusCommand, IEnumerable<Guid>>
    {
        private IHomeLightSystemRepository _homeLightSystemRepository;

        public ChangeLightPointsGroupStatusCommandHandler(IHomeLightSystemRepository homeLightSystemRepository)
          => _homeLightSystemRepository = homeLightSystemRepository ?? throw new ArgumentNullException(nameof(homeLightSystemRepository));

        public async Task<IEnumerable<Guid>> Handle(ChangeLightPointsGroupStatusCommand request, CancellationToken cancellationToken)
        {
            var homeLightSystem = await _homeLightSystemRepository.GetByIdAsync(request.HomeLightSystemId);

            var lightBulbIds = homeLightSystem.ChangeLightPointsGroupStatus(request.LightPointsGroupId, request.Status);
            await _homeLightSystemRepository.Save();
            return lightBulbIds;
        }
    }
}
