using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Dto.UserDto;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Service.Interface;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsers();
    Task<User?> GetUser(Guid id);
    Task<User?> CreateUser(UserDto user);
    Task<bool> DeleteUser(Guid id);
    Task<bool> UpdateUser(UserDto user, Guid id);
    Task<User?> Login(UserLoginDto userLoginDto);
    Task<bool> IsAdminOrManager(Guid userId);
}