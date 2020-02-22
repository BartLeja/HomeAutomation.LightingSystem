using LightingSystem.Data.Repositories;
using LightingSystem.Domain.HomeLightSystem;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.API.Commands
{
    public class AddLightSystemCommandHandler : IRequestHandler<AddLightSystemCommand,Guid>
    {
        private IHomeLightSystemRepository _homeLightSystemRepository;

        public AddLightSystemCommandHandler(IHomeLightSystemRepository homeLightSystemRepository)
        {
            _homeLightSystemRepository = homeLightSystemRepository ?? throw new ArgumentNullException(nameof(homeLightSystemRepository)); 
        }

        public async Task<Guid> Handle(AddLightSystemCommand command, CancellationToken cancellationToken)
        {
            var homeLightSystem = new HomeLightSystem(command.UserName);
            
            await _homeLightSystemRepository.AddAsync(homeLightSystem);
            await _homeLightSystemRepository.Save();
            return homeLightSystem.Id;
        }
    }
}
