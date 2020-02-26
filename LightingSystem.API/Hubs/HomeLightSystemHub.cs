using LightingSystem.API.Commands;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace LightingSystem.API.Hubs
{
    public class HomeLightSystemHub : Hub
    {
        private readonly IMediator _mediator;
        public HomeLightSystemHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "HomeLightSystemUser");
            await SendTestMessage(Context.User.Identity.Name, $"User {Context.User.Identity.Name} connected");

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "HomeLightSystemUser");
            await base.OnDisconnectedAsync(exception);
        }

        // TODO Remove after tests
        public Task SendTestMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendLightPointStatus(Guid lightBulbId, bool status)
        {
            await _mediator.Send(new ChangeLightBulbStatusCommand(lightBulbId, status));
            await Clients.Others.SendAsync("ReceiveLightPointStatus", lightBulbId, status);
        }
    }
}
