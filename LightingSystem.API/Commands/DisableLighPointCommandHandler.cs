using LightingSystem.Data.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.API.Commands
{
    public class DisableLighPointCommandHandler : IRequestHandler<DisableLighPointCommand, Guid>
    {
        private IHomeLightSystemRepository _homeLightSystemRepository;

        public DisableLighPointCommandHandler(IHomeLightSystemRepository homeLightSystemRepository)
        {
            _homeLightSystemRepository = homeLightSystemRepository ?? throw new ArgumentNullException(nameof(homeLightSystemRepository));
        }

        public async Task<Guid> Handle(DisableLighPointCommand request, CancellationToken cancellationToken)
        {
            var lighPoint = await _homeLightSystemRepository.GetLightPointByIdAsync(request.LightPointId);

            lighPoint.Disable();

            await _homeLightSystemRepository.Save();

            return request.LightPointId;
        }
    }
}
