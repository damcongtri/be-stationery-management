using Microsoft.AspNetCore.SignalR;
using stationeryManagement.Service.SignalRService.Presence;

namespace stationeryManagement.Service.SignalRService;
public interface IEventHub
{
    Task SendMessageAsync(string method, SignalRMessage message, bool onlyAdmins = true);
    Task SendMessageToAsync(string method, SignalRMessage message, int userId);
}
public class EventHub:IEventHub
{
    private readonly IHubContext<MessageHub> _messageHub;
    private readonly IPresenceTracker _presenceTracker;
    public EventHub(IHubContext<MessageHub> messageHub, IPresenceTracker presenceTracker)
    {
        _messageHub = messageHub;
        _presenceTracker = presenceTracker;

    }
    public async Task SendMessageAsync(string method, SignalRMessage message, bool onlyAdminsAndManager = true)
    {
        var users = _messageHub.Clients.All;
        if (onlyAdminsAndManager)
        {
            var admins = await _presenceTracker.GetOnlineAdminAndManagerIds();
            users = _messageHub.Clients.Users(admins.Select(i => i.ToString()).ToArray());
        }
        await users.SendAsync(method, message);
        await _messageHub.Clients.All.SendAsync("ReceiveMessage", "You have been added to the Manager group.");

    }

    public async Task SendMessageToAsync(string method, SignalRMessage message, int userId)
    {
        await _messageHub.Clients.Users(new List<string>() {userId + string.Empty}).SendAsync(method, message);
    }
}