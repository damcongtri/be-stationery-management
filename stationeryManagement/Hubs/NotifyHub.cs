using Microsoft.AspNetCore.SignalR;

namespace stationeryManagement.Hubs;

public class UserConnection
{
    public string ConnectionId { get; set; }
    public string UserId { get; set; }
    public string Role { get; set; }
}

public sealed class NotifyHub : Hub
{
    // Lưu trữ thông tin về các kết nối
    // private static readonly ConcurrentDictionary<string, UserConnection> Users =
    //     new ConcurrentDictionary<string, UserConnection>();

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("aaa");
        // var userId = Context.GetHttpContext().Request.Query["userId"];
        // var connectionId = Context.ConnectionId;
        //
        // // Lưu userId và connectionId vào bộ nhớ
        // Users.TryAdd(userId, new UserConnection { ConnectionId = connectionId, UserId = userId });
        // await base.OnConnectedAsync();
        await Clients.All.SendAsync("ReceiveMessage", Context.ConnectionId);
    }
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    // public override async Task OnDisconnectedAsync(Exception exception)
    // {
    //     UserConnection user;
    //     Users.TryRemove(Context.ConnectionId, out user);
    //
    //     await base.OnDisconnectedAsync(exception);
    // }
    // public async Task NewMessage(long username, string message) =>
    //     await Clients.All.SendAsync("messageReceived", username, message);
    //
    // public async Task SendMessageToUser(string userId, string message)
    // {
    //     UserConnection user;
    //     if (Users.TryGetValue(userId, out user))
    //     {
    //         await Clients.Client(user.ConnectionId).SendAsync("ReceiveMessage", "Server", "hello");
    //     }
    //         await Clients.All.SendAsync("ReceiveMessage", "Server", "hello");
    // } 
    // public async Task SendMessageToManager(string role, string message)
    // {
    //     UserConnection user;
    //     if (Users.TryGetValue(Roles.Manager, out user))
    //     {
    //         await Clients.Client(user.ConnectionId).SendAsync("ReceiveMessage", "Server", message);
    //     }
    //     if (Users.TryGetValue(Roles.Admin, out user))
    //     {
    //         await Clients.Client(user.ConnectionId).SendAsync("ReceiveMessage", "Server", message);
    //     }
    // }
}