using MediatR;
using System;

namespace LightingSystem.API.Features.HomeLightingSystem.Create
{
    public class AddLightSystemCommand: IRequest<Guid>
    {
        public string UserName { get; set; }

        public AddLightSystemCommand(string userName) => UserName = userName;
    }
}
