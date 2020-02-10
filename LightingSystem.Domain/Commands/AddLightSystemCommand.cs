using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightingSystem.Domain.Commands
{
    public class AddLightSystemCommand: IRequest<Guid>
    {
        public string UserName { get; set; }

        public AddLightSystemCommand(string userName)
        {
            UserName = userName;
        }
    }
}
