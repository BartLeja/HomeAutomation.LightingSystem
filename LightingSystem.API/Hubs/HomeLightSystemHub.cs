using LightingSystem.API.Features.HomeLightingSystem.DisableAllLightPoints;
using LightingSystem.API.Features.HomeLightingSystem.EnableAllLightPoints;
using LightingSystem.API.Features.LightPoint.ChangeLightBulbStatus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace LightingSystem.API.Hubs
{
    //TODO Sending to only one client
    //https://ngohungphuc.wordpress.com/2019/05/01/send-message-to-specific-user-in-signalr/
    [Authorize]
    public class HomeLightSystemHub : Hub
    {
        private readonly IMediator _mediator;
        public HomeLightSystemHub(IMediator mediator) => _mediator = mediator;

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "HomeLightSystemUser");
            //await SendTestMessage(Context.User.Identity.Name, $"User {Context.User.Identity.Name} connected");
            //TODO id from authorization service

            if (Context.User.Identity.Name.Equals("blejaService")){
                //This magic string is from appsettings of HomeAutomation.LightingSystem.LocalServer, this is id of LocalServer
                await _mediator.Send(new EnableAllLightPointsCommand(Guid.Parse("bdcd95ec-2dc6-4a7b-90ca-f132a7784b0f")));
            }
            //TODO set isActive to false on database
            //await _mediator.Send(new EnableAllLightPointsCommand(lightBulbId, status));
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.User.Identity.Name.Equals("blejaService"))
            {
                //This magic string is from appsettings of HomeAutomation.LightingSystem.LocalServer, this is id of LocalServer
                await _mediator.Send(new DisableAllLightPointsCommand(Guid.Parse("bdcd95ec-2dc6-4a7b-90ca-f132a7784b0f")));
            }
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
        
        public async Task SendHardRestOfLightPointMessage(Guid lightPointId)
        {
            await Clients.Others.SendAsync("HardRestOfLightPoint", lightPointId);
        }

        public async Task SendRestOfLightPointMessage(Guid lightPointId)
        {
            await Clients.Others.SendAsync("RestOfLightPoint", lightPointId);
        }
    }
}
