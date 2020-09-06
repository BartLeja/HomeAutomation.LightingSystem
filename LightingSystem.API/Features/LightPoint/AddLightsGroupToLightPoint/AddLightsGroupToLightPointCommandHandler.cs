using LightingSystem.Data.Repositories;
using LightingSystem.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.API.Features.LightPoint.AddLightsGroupToLightPoint
{
    public class AddLightsGroupToLightPointCommandHandler : IRequestHandler<AddLightsGroupToLightPointCommand, Guid>
    {
        private IHomeLightSystemRepository _homeLightSystemRepository;

        public AddLightsGroupToLightPointCommandHandler(IHomeLightSystemRepository homeLightSystemRepository) 
            => _homeLightSystemRepository = homeLightSystemRepository 
            ?? throw new ArgumentNullException(nameof(homeLightSystemRepository));

        public async Task<Guid> Handle(AddLightsGroupToLightPointCommand request, CancellationToken cancellationToken)
        {
            var lightsGroup = await _homeLightSystemRepository.CheckIfLightsGroupExist(request.LightGroupName);
            var lightPoint = await _homeLightSystemRepository.GetLightPointByIdAsync(request.LightPointId);

            if (lightsGroup == null)
            {
                lightsGroup = new LightsGroup(request.Id, request.LightGroupName);
                await _homeLightSystemRepository.CreateLightsGroup(lightsGroup);
            }

            lightPoint.AddLightsGroup(lightsGroup);
            await _homeLightSystemRepository.UpdateLightPoint(lightPoint);
            await _homeLightSystemRepository.Save();

            return lightsGroup.Id;
        }
    }
}
