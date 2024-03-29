namespace stationeryManagement.Service.SignalRService.Presence;

public interface IPresenceTracker
{
    Task UserConnected(Guid userId, string connectionId);
    Task UserDisconnected(Guid userId, string connectionId);
    Task<Guid[]> GetOnlineAdminAndManagerIds();
    Task<List<string>> GetConnectionsForUser(Guid userId);
}