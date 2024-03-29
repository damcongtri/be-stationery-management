using Microsoft.EntityFrameworkCore.Metadata.Internal;
using stationeryManagement.Data;
using stationeryManagement.Service.Interface;

namespace stationeryManagement.Service.SignalRService.Presence;
internal class ConnectionDetail
{
    public string UserName { get; set; }
    public List<string> ConnectionIds { get; set; } = new List<string>();
    public bool IsAdminOrManager { get; set; }
}
public class PresenceTracker:IPresenceTracker
{
    private static readonly Dictionary<Guid, ConnectionDetail> OnlineUsers = new Dictionary<Guid, ConnectionDetail>();

    private readonly IUserService _userService;

    public PresenceTracker(IUserService userService)
    {
        _userService = userService;
    }

    public async Task UserConnected(Guid userId, string connectionId)
    {
        var user = await _userService.GetUser(userId);
        if (user == null) return;
        var isAdminOrManager = await _userService.IsAdminOrManager(userId);
        lock (OnlineUsers)
        {
            if (OnlineUsers.TryGetValue(userId, out var detail))
            {
                detail.ConnectionIds.Add(connectionId);
            }
            else
            {
                OnlineUsers.Add(userId, new ConnectionDetail()
                {
                    UserName = user.Name,
                    ConnectionIds = new List<string>() {connectionId},
                    IsAdminOrManager = isAdminOrManager
                });
            }
        }
    }


    public Task UserDisconnected(Guid userId, string connectionId)
    {
        lock (OnlineUsers)
        {
            if (!OnlineUsers.ContainsKey(userId)) return Task.CompletedTask;

            OnlineUsers[userId].ConnectionIds.Remove(connectionId);

            if (OnlineUsers[userId].ConnectionIds.Count == 0)
            {
                OnlineUsers.Remove(userId);
            }
        }
        return Task.CompletedTask;
    }

    public Task<Guid[]> GetOnlineAdminAndManagerIds()
    {
        Guid[] onlineUsers;
        lock (OnlineUsers)
        {
            onlineUsers = OnlineUsers.Where(pair => pair.Value.IsAdminOrManager)
                .Select(k => k.Key)
                .OrderBy(key=>key)
                .ToArray();
        }
        return Task.FromResult(onlineUsers);
    }

    public static Task<string[]> GetOnlineUsers()
    {
        string[] onlineUsers;
        lock (OnlineUsers)
        {
            onlineUsers = OnlineUsers
                .Select(k => k.Value.UserName)
                .OrderBy(k=>k)
                .ToArray();
        }

        return Task.FromResult(onlineUsers);
    }
    public Task<List<string>> GetConnectionsForUser(Guid userId)
    {
        List<string>? connectionIds;
        lock (OnlineUsers)
        {
            connectionIds = OnlineUsers.GetValueOrDefault(userId)?.ConnectionIds;
        }

        return Task.FromResult(connectionIds ?? new List<string>());
    }
}