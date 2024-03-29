using System.Collections.Concurrent;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using stationeryManagement.Data.Static;
using stationeryManagement.Service.SignalRService.Presence;

namespace stationeryManagement.Service.SignalRService;

public class UserConnection
{
    public string ConnectionId { get; set; }
    public string UserId { get; set; }
    public string Role { get; set; }
}

[Authorize]
public sealed class MessageHub : Hub
{
    private IPresenceTracker _presenceTracker;
    // Lưu trữ thông tin về các kết nối
    private static readonly ConcurrentDictionary<string, UserConnection> Users =
        new ConcurrentDictionary<string, UserConnection>();

    public MessageHub(IPresenceTracker presenceTracker)
    {
        _presenceTracker = presenceTracker;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User.FindFirst(ClaimTypes.Sid).Value;
        var connectionId = Context.ConnectionId;
        await _presenceTracker.UserConnected(Guid.Parse(userId), connectionId);
        var currentUsers = await PresenceTracker.GetOnlineUsers();
        await Clients.All.SendAsync("Connected", currentUsers);
        // // Kiểm tra vai trò của người dùng
        // var userRoles = Context.User.FindFirst(ClaimTypes.Role).Value;
        // bool isAdminOrManager = userRoles != null && (userRoles.Contains(Roles.Admin) || userRoles.Contains(Roles.Manager));
        //
        // // Lưu userId và connectionId vào bộ nhớ
        // Users.TryAdd(userId, new UserConnection { ConnectionId = connectionId, UserId = userId });
        // if (isAdminOrManager)
        // {
        //     // Thêm người dùng vào nhóm Manager nếu họ có vai trò là Admin hoặc Manager
        //     await Groups.AddToGroupAsync(connectionId, "Manager");
        //     await base.OnConnectedAsync();
        //     await Clients.Client(connectionId).SendAsync("ReceiveMessage", "You have been added to the Manager group.");
        // }
        // else
        // {
        //     // Người dùng không có vai trò là Admin hoặc Manager vẫn có thể hoạt động bình thường
        //     await base.OnConnectedAsync();
        //     await Clients.Client(connectionId).SendAsync("ReceiveMessage",
        //         "You don't have permission to join the Manager group, but you can still use the application.");
        // }


    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Guid.Parse(Context.User.FindFirst(ClaimTypes.Sid).Value);
        await _presenceTracker.UserDisconnected(userId, Context.ConnectionId);

        var currentUsers = await PresenceTracker.GetOnlineUsers();
        await Clients.All.SendAsync("Disconnect", currentUsers);


        await base.OnDisconnectedAsync(exception);
    }
}