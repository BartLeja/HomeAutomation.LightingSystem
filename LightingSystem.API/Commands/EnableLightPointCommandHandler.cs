using LightingSystem.Data.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.API.Commands
{
    public class EnableLightPointCommandHandler : IRequestHandler<EnableLightPointCommand, Guid>
    {
        private IHomeLightSystemRepository _homeLightSystemRepository;

        public EnableLightPointCommandHandler(IHomeLightSystemRepository homeLightSystemRepository)
        {
            _homeLightSystemRepository = homeLightSystemRepository ?? throw new ArgumentNullException(nameof(homeLightSystemRepository));
        }

        public async Task<Guid> Handle(EnableLightPointCommand request, CancellationToken cancellationToken)
        {
            var lighPoint = await _homeLightSystemRepository.GetLightPointByIdAsync(request.LightPointId);

            lighPoint.Enable();
            
            await _homeLightSystemRepository.Save();

            return request.LightPointId;
        }
    }
}
