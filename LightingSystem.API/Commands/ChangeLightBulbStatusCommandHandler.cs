using LightingSystem.Data.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.API.Commands
{
    public class ChangeLightBulbStatusCommandHandler: IRequestHandler<ChangeLightBulbStatusCommand, Guid>
    {
        private IHomeLightSystemRepository _homeLightSystemRepository;

        public ChangeLightBulbStatusCommandHandler(IHomeLightSystemRepository homeLightSystemRepository)
        {
            _homeLightSystemRepository = homeLightSystemRepository ?? throw new ArgumentNullException(nameof(homeLightSystemRepository));
        }

        public async Task<Guid> Handle(ChangeLightBulbStatusCommand request, CancellationToken cancellationToken)
        {
            var lightBulb = await _homeLightSystemRepository.GetLightBulbByIdAsync(request.LightBulbId);

            lightBulb.ChangeStatus(request.Status);
            
            await _homeLightSystemRepository.Save();

            return request.LightBulbId;
        }
    }
}
