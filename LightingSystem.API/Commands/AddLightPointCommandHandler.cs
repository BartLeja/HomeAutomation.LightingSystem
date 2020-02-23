using LightingSystem.Data.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.API.Commands
{
    public class AddLightPointCommandHandler : IRequestHandler<AddLightPointCommand, Guid>
    {
        private IHomeLightSystemRepository _homeLightSystemRepository;

        public AddLightPointCommandHandler(IHomeLightSystemRepository homeLightSystemRepository)
        {
            _homeLightSystemRepository = homeLightSystemRepository ?? throw new ArgumentNullException(nameof(homeLightSystemRepository));
        }

        public async Task<Guid> Handle(AddLightPointCommand request, CancellationToken cancellationToken)
        {
            var homeLightSystem = await _homeLightSystemRepository.GetByIdAsync(request.HomeLightSystemId);

            var lightPointId = homeLightSystem.AddLighPoint(
                request.MqttId,
                request.CustomName,
                request.LightBulbsCount,
                request.Bulbs);
          
           _homeLightSystemRepository.AddLightPoint(homeLightSystem, lightPointId);
            await _homeLightSystemRepository.Save();
            return lightPointId;
        
        }
    }
}
