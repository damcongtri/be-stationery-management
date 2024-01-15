using stationeryManagement.Data.Model;

namespace stationeryManagement.Service.Interface;

public interface IAuthService
{
    string GenerateToken(User user);
}