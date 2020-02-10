using LightingSystem.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.Domain.Commands
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
            var homeLightSystem = new HomeLightSystem.HomeLightSystem(command.UserName);
            
            await _homeLightSystemRepository.AddAsync(new Infrastructure.Database.Models.HomeLightSystem() { UserName = "test"});
            await _homeLightSystemRepository.Save();
            return homeLightSystem.LocalLightingSystemId;
        }
    }
}
