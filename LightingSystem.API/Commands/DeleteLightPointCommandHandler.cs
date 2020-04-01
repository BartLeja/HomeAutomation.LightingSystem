using LightingSystem.Data.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSystem.API.Commands
{
    public class DeleteLightPointCommandHandler : IRequestHandler<DeleteLightPointCommand, Guid>
    {
        private IHomeLightSystemRepository _homeLightSystemRepository;

        public DeleteLightPointCommandHandler(IHomeLightSystemRepository homeLightSystemRepository)
        {
            _homeLightSystemRepository = homeLightSystemRepository ?? throw new ArgumentNullException(nameof(homeLightSystemRepository));
        }

        public async Task<Guid> Handle(DeleteLightPointCommand request, CancellationToken cancellationToken)
        {
            _homeLightSystemRepository.DeleteLighPoint(request.LightPointId);
            await _homeLightSystemRepository.Save();
            return request.LightPointId;
        }
    }
}
